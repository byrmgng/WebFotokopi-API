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
    public class PaperSize:BaseEntity
    {
        public string SizeName { get; set; }
        public ICollection<Package> ProductFeatures { get; set; }
    }
}
