using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssignmentApp.Persistance.ORM.EntityFramework.Identity
{
  public class ApplicationRole:IdentityRole
  {
    public string? Description { get; set; }

  }
}
