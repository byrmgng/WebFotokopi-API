using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities.Commons;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Domain.Entities
{
    public class Product:BaseEntity
    {
        public Guid PackageID { get; set; }
        public Package Package { get; set; }
        public Guid FileID { get; set; }
        public File File { get; set; }
        public string CustomerNote { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Guid OrderID { get; set; }
    }
}
