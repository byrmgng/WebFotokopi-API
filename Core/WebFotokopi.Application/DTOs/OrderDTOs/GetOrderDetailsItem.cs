using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.OrderDTOs
{
    public class GetOrderDetailsItem
    {
        public string FileID { get; set; }
        public string FileTitle { get; set; }
        public string FileNote { get; set; }
        public string FileNumberOfFile { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string PackagePrice { get; set; }
        public string PackageColorMode { get; set; }
        public string PackageDuplexMode { get; set; }
        public string PackageSheetsPerPage { get; set; }
        public string PackagePageSize { get; set; }
        public string PackagePaperType { get; set; }
    }
}
