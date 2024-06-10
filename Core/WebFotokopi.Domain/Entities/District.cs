using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities.Commons;

namespace WebFotokopi.Domain.Entities
{
    public class District:BaseEntity
    {
        public int ID { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
        public string Name { get; set; }
        public ICollection<SellerAddress> SellerAddress { get; set; }
        public ICollection<CustomerAddress> CustomerAddresses { get; set; }

    }
}
