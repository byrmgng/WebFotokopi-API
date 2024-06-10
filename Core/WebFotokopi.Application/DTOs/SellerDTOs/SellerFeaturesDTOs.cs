using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.PackageDTOs;

namespace WebFotokopi.Application.DTOs.SellerDTOs
{
    public class SellerFeaturesDTOs
    {
        public string SellerID { get; set; }
        public string CompanyName { get; set; }
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public IEnumerable<GetCustomersPackageDTO> Packages { get; set; }
    }
}
