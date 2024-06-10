using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Domain.Entities.Identity
{
    public class BaseIdentityUser: IdentityUser
    {
        public string? RefleshToken { get; set; }
        public DateTime? RefleshTokenEndDate { get; set; }
    }
}
