using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssignmentApp.Persistance.ORM.EntityFramework.Identity;

namespace TaskAssignmentApp.Persistance.ORM.EntityFramework.Contexts
{
  public class AppIdentityContext:IdentityDbContext<ApplicationUser,ApplicationRole,string>
  {
    public AppIdentityContext(DbContextOptions<AppIdentityContext> opt):base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

      base.OnModelCreating(builder);

      builder.Entity<ApplicationUser>().ToTable("Users");
      builder.Entity<ApplicationRole>().ToTable("Roles");
      builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
      builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
      builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
      builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
      builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");


    }
  }
}
