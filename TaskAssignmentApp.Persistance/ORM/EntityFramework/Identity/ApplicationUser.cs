using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssignmentApp.Persistance.ORM.EntityFramework.Identity
{
  public class ApplicationUser:IdentityUser
  {
    public string? WebSiteUrl { get; set; }
    public string? CorporatePhoneNumber { get; set; }


  }
}
