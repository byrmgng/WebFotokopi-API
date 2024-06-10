using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.DTOs.SellerDTOs
{
    public class GetSellerAccountInfoDTO
    {
        public string? CityID { get; set; }
        public string? DistrictID { get; set; }
        public string? Address { get; set; }
        public string? CompanyName { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
