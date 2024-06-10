using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Domain.Entities.Identity
{
    public class AppSeller: BaseIdentityUser
    {
        public Guid SellerAddressID { get; set; }
        public SellerAddress SellerAddress { get; set; }
        public string CompanyName { get; set; }
        public ICollection<Package> Packages { get; set; }
        public bool View { get; set; } = false;
        public ICollection<File> FileContents { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public string? Logo2 { get; set; }

    }
}
