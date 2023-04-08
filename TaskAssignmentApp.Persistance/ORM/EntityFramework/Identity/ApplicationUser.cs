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

    /// <summary>
    /// Güncel Refresh Token bilgisi
    /// </summary>
    public string? RefreshToken { get; set; }


    /// <summary>
    /// 45 dk lık periodlar ile RefreshTokenExpire süresi
    /// </summary>
    public DateTime? RefreshTokenExpireAt { get; set; }

    /// <summary>
    /// Hesabını kitlediğimiz kullanıcılar için bu değeri true yaparız. ve artık hiç access token alamaz
    /// Güvenlik sebebi ile bu arkadaş artık accesstoken alamaz.
    /// </summary>
    public bool RefreshTokenRevoked { get; set; } = false;



  }
}
