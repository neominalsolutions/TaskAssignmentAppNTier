using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Application.Exceptions;
using TaskAssignmentApp.Infrastructure.Token.JWT;
using TaskAssignmentApp.Persistance.ORM.EntityFramework.Identity;

namespace TaskAssignmentApp.Application.Handlers
{
  public class TokenRequestHandler : IRequestHandler<LoginRequestDto, TokenResponseDto>
  {
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IAccessTokenService jwtTokenService;

    public TokenRequestHandler(UserManager<ApplicationUser>  userManager, IAccessTokenService jwtTokenService, RoleManager<ApplicationRole> roleManager)
    {
      this.userManager = userManager;
      this.jwtTokenService = jwtTokenService;
      this.roleManager = roleManager;
    }


    public async  Task<TokenResponseDto> Handle(LoginRequestDto request, CancellationToken cancellationToken)
    {

      var user =  await this.userManager.FindByEmailAsync(request.Email);
      

      if(user != null)
      {
        var passwordCheck = await this.userManager.CheckPasswordAsync(user, request.Password);

        if(passwordCheck)
        {
          // user claim tablosuna baktığından boş
          var permissionClaims = await this.userManager.GetClaimsAsync(user); // kullanıcıya tanımlanmış olan tüm permission değerlerini okuduk.
          var roles = await this.userManager.GetRolesAsync(user);

          foreach (var roleName in roles)
          {
            var role = await this.roleManager.FindByNameAsync(roleName);
            var roleClaims = await this.roleManager.GetClaimsAsync(role);

            foreach (var roleClaim in roleClaims)
            {
              permissionClaims.Add(roleClaim);
            }
          }

         
          var claims = new List<Claim>();
          claims.Add(new Claim(ClaimTypes.Email, user.Email));
          claims.Add(new Claim("sub", user.Id));
          claims.Add(new Claim(ClaimTypes.Role, string.Join(",",roles)));

        

          claims.Add(new Claim("permissions", System.Text.Json.JsonSerializer.Serialize(permissionClaims.Select(a => new { Key = a.Type, Value = a.Value }))));

          // JWT ile Token generate et ve claimsleri token içerisine göm

          //var principle =  await this.signInManager.CreateUserPrincipalAsync(user);

          var identity = new ClaimsIdentity(claims);
          var tokenResponse = this.jwtTokenService.CreateAccessToken(identity);


          user.RefreshToken = tokenResponse.RefreshToken;
          user.RefreshTokenExpireAt = DateTime.Now.AddMinutes(45);

          await userManager.UpdateAsync(user);

          return tokenResponse;


        }
        else
        {
          throw new Exception("Kullanıcı Parolası hatalı");
        }
      }
      else
      {
        throw new AuthenticationFailedException();
      }

    }
  }
}
