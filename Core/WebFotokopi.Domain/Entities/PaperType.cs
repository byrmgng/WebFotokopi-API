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
    public class PaperType:BaseEntity
    {
        public string PaperTypeName { get; set; }
        public ICollection<Package> ProductFeatures { get; set; }

    }
}
