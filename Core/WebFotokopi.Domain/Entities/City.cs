using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities.Commons;

namespace WebFotokopi.Domain.Entities
{
    public class City:BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
