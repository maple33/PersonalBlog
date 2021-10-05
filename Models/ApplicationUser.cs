using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string firstName { get; set; }
        [PersonalData]
        public string lastName { get; set; }
    }
}
