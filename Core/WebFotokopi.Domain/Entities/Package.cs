using WebFotokopi.Domain.Entities.Commons;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Domain.Entities
{
    public class Package:BaseEntity
    {
        public AppSeller? AppSeller { get; set; }
        public required string SellerID { get; set; }
        public required string Name { get; set; }
        public float Price { get; set; }
        public bool ColorMode { get; set; }
        public bool DuplexMode { get; set; }
        public  ICollection<Product>? Products { get; set; }
        public  SheetsPerPage? SheetsPerPage { get; set; }
        public  PaperType? PaperType { get; set; }
        public  PaperSize? PaperSize { get; set; }
        public Guid SheetsPerPageID { get; set; }
        public Guid PaperTypeID { get; set; }
        public Guid PaperSizeID { get; set; }
        public bool isActive { get; set; }
        public bool View { get; set; } = true;
    }
}
    