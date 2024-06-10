using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities.Commons;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Domain.Entities
{
    public class File:BaseEntity
    {
        public AppSeller AppSeller { get; set; }
        public string AppSellerID { get; set; }
        public AppCustomer AppCustomer { get; set; }
        public string? AppCustomerID { get; set; }
        public string FileContent { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public ICollection<Product> Products { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public bool SellerOwner { get; set; } = false;
        public int NumberOfPage { get; set; }
    }
}
