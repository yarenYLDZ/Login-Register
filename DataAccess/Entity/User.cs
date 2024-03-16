using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class User : IdentityUser
    {
      public ICollection<Address> addresses {  get; set; }
      public string? conformationCode { get; set; }
    }
}
