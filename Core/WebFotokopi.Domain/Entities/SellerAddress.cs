
using WebFotokopi.Domain.Entities.Commons;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Domain.Entities
{
    public class SellerAddress:BaseEntity
    {
        public int DistrictID { get; set; }
        public District District { get; set; }

        public string Address { get; set; }

        public AppSeller AppSeller { get; set; }



    }
}
