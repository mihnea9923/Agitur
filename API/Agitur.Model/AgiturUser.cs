using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.Identity
{
    public class AgiturUser : IdentityUser
    {
        public string FullName { get; set; }
       
    }
}
