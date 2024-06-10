using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.PaperSizeDTOs;
using WebFotokopi.Application.DTOs.PaperTypeDTOs;
using WebFotokopi.Application.DTOs.SheetsPerPageDTOs;

namespace WebFotokopi.Application.DTOs.PackageDTOs
{
    public class GetPackageDTO
    {
        public Guid PackageID { get; set; }
        public string PackageName { get; set; }
        public float Price { get; set; }
        public bool DuplexMode { get; set; }
        public bool ColorMode { get; set; }
        public bool isActive { get; set; }
        public ListPaperTypeDTO PaperType { get; set; }
        public ListPaperSizeDTO PaperSize { get; set; }
        public ListSheetsPerPageDTO SheetsPerPage { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
    }
}
