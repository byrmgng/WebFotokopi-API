using System.ComponentModel.DataAnnotations.Schema;
using WebFotokopi.Domain.Entities.Commons;
using WebFotokopi.Domain.Entities.Identity;
namespace WebFotokopi.Domain.Entities
{
    public class CustomerAddress:BaseEntity
    {
        public int DistrictID { get; set; }
        public District District { get; set; }
        public string Address { get; set; }
        public AppCustomer AppCustomer { get; set; }
    }
}
