using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using WebFotokopi.Application.Abstraction.Services;
using WebFotokopi.Application.DTOs.FileDTOs;
using WebFotokopi.Application.DTOs.PackageDTOs;
using WebFotokopi.Application.DTOs.PaperSizeDTOs;
using WebFotokopi.Application.DTOs.PaperTypeDTOs;
using WebFotokopi.Application.DTOs.SheetsPerPageDTOs;
using WebFotokopi.Application.Repositories.FileRepositories;
using WebFotokopi.Application.Repositories.ProductFeatureRepositories;
using WebFotokopi.Application.ViewModels.File;
using WebFotokopi.Application.ViewModels.Package;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Domain.Entities.Identity;
using WebFotokopi.Persistence.Repositories.ProductFeatureRepositories;

namespace WebFotokopi.Persistence.Services
{
    public class FileService : IFileService
    {
        readonly IFileReadRepository _fileReadRepository;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        readonly UserManager<AppSeller> _sellerUserManager;
        readonly UserManager<AppCustomer> _customerUserManager;

        public FileService(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, UserManager<AppCustomer> customerUserManager, UserManager<AppSeller> sellerUserManager, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _sellerUserManager = sellerUserManager;
            _customerUserManager = customerUserManager;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<AppSeller?> FindSeller()
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                AppSeller? appSeller = await _sellerUserManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                return appSeller;
            }
            return null;
        }
        public async Task<AppCustomer?> FindCustomer()
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name?.ToString();
            if (!string.IsNullOrEmpty(username))
            {
                AppCustomer? appCustomer = await _customerUserManager.Users.Include(x=>x.CustomerAddress).FirstOrDefaultAsync(x => x.UserName == username);
                return appCustomer;
            }
            return null;
        }

        public async Task<CreateFileResponseDTO> CreateFileAsync(VM_Create_File vmCreateFile)
        {
            AppSeller? appSeller = FindSeller().Result;
            if (vmCreateFile.FileContent == null || vmCreateFile.FileContent.Length == 0)
                return new() { Message = "Dosya Bulunamadı",Succeeded=false };
            using (var stream = new MemoryStream())
            {
                if (appSeller != null)
                {
                    await vmCreateFile.FileContent.CopyToAsync(stream);
                    stream.Position = 0; // MemoryStream'in pozisyonunu sıfırla
                    byte[] fileBytes = stream.ToArray();
                    string base64String = Convert.ToBase64String(fileBytes);
                    int NumberOfPages;
                    using (PdfReader reader = new PdfReader(stream))
                    {
                        NumberOfPages = reader.NumberOfPages;
                    }

                    WebFotokopi.Domain.Entities.File file = new()
                    {
                        Title = vmCreateFile.FileTitle,
                        Note = vmCreateFile.FileNote,
                        FileContent = base64String,
                        FileName = vmCreateFile.FileContent.FileName,
                        ContentType = vmCreateFile.FileContent.ContentType,
                        AppSellerID = appSeller.Id,
                        SellerOwner = true,
                        NumberOfPage = NumberOfPages,
                    };
                    bool succeeded = await _fileWriteRepository.AddAsync(file);
                    await _fileWriteRepository.SaveAsync();
                    if (succeeded)
                        return new() { Succeeded = succeeded, Message = "Paket ekleme işlemi başarı ile tamamlandı" };
                    return new() { Succeeded = succeeded, Message = "Paket ekleme işlemi tamamlanamadı!" };
                }
            }
            return new() { Succeeded = false, Message = "Ürün ekleme işlemi sırasında kullanıcı doğrulanamadı" };
        }
        public async Task<IEnumerable<GetFileDTO>> GetFileAsync(VM_FilterGet_File vmFilterGetFile)
        {
            AppSeller? appSeller = FindSeller().Result;

            if (appSeller != null)
            {
                var files = await _fileReadRepository
                .Table
                .Include(x => x.AppSeller)
                .Where(x => x.AppSellerID == appSeller.Id)
                .Where(x=>x.SellerOwner==true)
                .Where(x => string.IsNullOrEmpty(vmFilterGetFile.FileTitle) || x.Title.Contains(vmFilterGetFile.FileTitle))
                .Where(x => string.IsNullOrEmpty(vmFilterGetFile.FileNote) || x.Note.Contains(vmFilterGetFile.FileNote))
                .Select(x=> new GetFileDTO
                {
                   ID=x.ID.ToString(),
                   FileTitle = x.Title,
                   FileNote = x.Note,
                   NumberOfPage = x.NumberOfPage,
                   CreatedDate = x.CreatedDate.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss"),
                   UpdatedDate = x.UpdatedDate.HasValue ? x.UpdatedDate.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss") : null,
                })
                .ToListAsync();
                return files;
            }
            return new List<GetFileDTO>();
        }
        public async Task<GetByIdFileDTO> GetByIdFileAsync(string id)
        {
            AppSeller? appSeller = FindSeller().Result;

            if (appSeller != null)
            {
                var file = await _fileReadRepository
                .Table
                .Include(x => x.AppSeller)
                .Where(x => x.AppSellerID == appSeller.Id)
                .Where(x=>x.ID == new Guid(id))
                .Select(x => new GetByIdFileDTO
                {
                    FileID = x.ID.ToString(),
                    FileTitle = x.Title,
                    FileNote = x.Note,
                    FileContent = x.FileContent,
                    FileName = x.FileName,
                    ContentType = x.ContentType,
                }).FirstOrDefaultAsync();
                return file;
            }
            return new GetByIdFileDTO();
        }
        public async Task<UpdateFileResponseDTO> UpdateFileAsync(VM_Update_File vmUpdateFile)
        {
            AppSeller? appSeller = FindSeller().Result;
            if (appSeller != null)
            {
                WebFotokopi.Domain.Entities.File file = await _fileReadRepository.GetByIdAsync(vmUpdateFile.FileID);
                if (string.Equals(appSeller.Id, file.AppSellerID))
                {
                    file.Title = vmUpdateFile.FileTitle;
                    file.Note = vmUpdateFile.FileNote;

                    bool succeeded = _fileWriteRepository.Update(file);
                    await _fileWriteRepository.SaveAsync();
                    if (succeeded)
                        return new() { Succeeded = succeeded, Message = "Dosya güncelleme işlemi başarı ile tamamlandı" };
                }
                return new() { Succeeded = false, Message = "Dosya güncelleme işlemi sırasında kullanıcı doğrulanamadı" };
            }
            return new() { Succeeded = false, Message = "Dosya güncelleme işlemi sırasında kullanıcı doğrulanamadı" };
        }
        public async Task<DeleteFileResponseDTO> DeleteFileAsync(string id)
        {
            AppSeller? appSeller = FindSeller().Result;
            if (appSeller != null)
            {
                WebFotokopi.Domain.Entities.File file = await _fileReadRepository.GetByIdAsync(id);
                if (string.Equals(appSeller.Id, file.AppSellerID))
                {
                    
                    bool succeeded = await _fileWriteRepository.RemoveAsync(id);
                    await _fileWriteRepository.SaveAsync();
                    if (succeeded)
                        return new() { Succeeded = succeeded, Message = "Dosya silme işlemi başarı ile tamamlandı" };
                }
                return new() { Succeeded = false, Message = "Dosya silme işlemi sırasında kullanıcı doğrulanamadı" };
            }
            return new() { Succeeded = false, Message = "Dosya silme işlemi sırasında kullanıcı doğrulanamadı" };
        }

        public async Task<IEnumerable<GetFileDTO>> GetFileForCustomerAsync(VM_FilterGet_File_ForCustomer vmFilterGetFileForCustomer)
        {
            AppCustomer? appCustomer = FindCustomer().Result;

            if (appCustomer != null)
            {
                var files = await _fileReadRepository
                .Table
                .Include(x => x.AppSeller)
                .Include(x => x.AppSeller.SellerAddress)
                .Where(x=>x.AppSeller.SellerAddress.DistrictID == appCustomer.CustomerAddress.DistrictID)
                .Where(x => x.AppSellerID == vmFilterGetFileForCustomer.SellerID)
                .Where(x => x.SellerOwner == true)
                .Where(x => string.IsNullOrEmpty(vmFilterGetFileForCustomer.FileTitle) || x.Title.Contains(vmFilterGetFileForCustomer.FileTitle))
                .Where(x => string.IsNullOrEmpty(vmFilterGetFileForCustomer.FileNote) || x.Note.Contains(vmFilterGetFileForCustomer.FileNote))
                .Select(x => new GetFileDTO
                {
                    ID = x.ID.ToString(),
                    FileTitle = x.Title,
                    FileNote = x.Note,
                    NumberOfPage = x.NumberOfPage,
                    CreatedDate = x.CreatedDate.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss"),
                    UpdatedDate = x.UpdatedDate.HasValue ? x.UpdatedDate.Value.ToLocalTime().ToString("MM/dd/yyyy HH:mm:ss") : null,
                })
                .ToListAsync();
                return files;
            }
            return new List<GetFileDTO>();
        }
        public async Task<GetByIdFileDTO> GetByIdFileForCustomerAsync(string id)
        {
            AppCustomer? appCustomer = FindCustomer().Result;

            if (appCustomer != null)
            {
                var file = await _fileReadRepository
                .Table
                .Include(x => x.AppSeller)
                .Where(x => x.AppSeller.SellerAddress.DistrictID == appCustomer.CustomerAddress.DistrictID)
                .Where(x => x.SellerOwner == true)
                .Where(x => x.ID == new Guid(id))
                .Select(x => new GetByIdFileDTO
                {
                    FileID = x.ID.ToString(),
                    FileTitle = x.Title,
                    FileNote = x.Note,
                    FileContent = x.FileContent,
                    FileName = x.FileName,
                    ContentType = x.ContentType,
                }).FirstOrDefaultAsync();
                byte[] fileContentBytes = Convert.FromBase64String(file.FileContent);
                byte[] fileWithWatermarkBytes = AddWatermarkToPdf(fileContentBytes, "WebFotokopi");

                // Base64'e dönüştür
                string fileWithWatermarkBase64 = Convert.ToBase64String(fileWithWatermarkBytes);

                // Dosya içeriğini değiştir
                file.FileContent = fileWithWatermarkBase64;
                return file;
            }
            return new GetByIdFileDTO();
        }
        public async Task<DeleteFileResponseDTO> DeleteFileForCustomerAsync(string id)
        {
            AppCustomer? appCustomer = FindCustomer().Result;
            if (appCustomer != null)
            {
                WebFotokopi.Domain.Entities.File file = await _fileReadRepository.GetByIdAsync(id);
                if (string.Equals(appCustomer.Id, file.AppCustomerID) && !file.SellerOwner)
                {

                    bool succeeded = await _fileWriteRepository.RemoveAsync(id);
                    await _fileWriteRepository.SaveAsync();
                    if (succeeded)
                        return new() { Succeeded = succeeded, Message = "Dosya silme işlemi başarı ile tamamlandı" };
                }
                return new() { Succeeded = false, Message = "Dosya silme işlemi sırasında kullanıcı doğrulanamadı" };
            }
            return new() { Succeeded = false, Message = "Dosya silme işlemi sırasında kullanıcı doğrulanamadı" };
        }
        private byte[] AddWatermarkToPdf(byte[] pdfContent, string watermarkText)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfReader pdfReader = new PdfReader(pdfContent);
                using (PdfStamper pdfStamper = new PdfStamper(pdfReader, outputStream))
                {
                    int pageCount = pdfReader.NumberOfPages;
                    for (int i = 1; i <= pageCount; i++)
                    {
                        // Sayfa boyutu ve orta nokta hesaplaması
                        Rectangle pageSize = pdfReader.GetPageSizeWithRotation(i);
                        float x = pageSize.Width / 2;
                        float y = pageSize.Height / 2;

                        // Filigran boyutunu ayarla
                        Font font = new Font(Font.FontFamily.HELVETICA, 48, Font.BOLD); // Kalın ve büyük font
                        float fontSize = Math.Min(pageSize.Width, pageSize.Height) / 5;
                        font.Size = fontSize;

                        // Filigran metninin eklenmesi
                        PdfContentByte pdfContentByte = pdfStamper.GetOverContent(i);
                        ColumnText.ShowTextAligned(pdfContentByte, Element.ALIGN_CENTER, new Phrase(watermarkText, font), x + 30, y, 45);
                    }
                }

                return outputStream.ToArray();
            }
        }




    }
}
