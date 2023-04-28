using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Application.Exceptions;
using TaskAssignmentApp.Infrastructure.Token.JWT;
using TaskAssignmentApp.Persistance.ORM.EntityFramework.Identity;

namespace TaskAssignmentAppNTier.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TokensController : ControllerBase
  {
    private readonly IMediator mediator;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAccessTokenService accessTokenService;

    public TokensController(IMediator mediator, UserManager<ApplicationUser> userManager, IAccessTokenService accessTokenService)
    {
      this.mediator = mediator;
      this.userManager = userManager;
      this.accessTokenService = accessTokenService;
    }



    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(TokenResponseDto))]
    [ProducesErrorResponseType(typeof(AuthenticationFailedException))]
    public async Task<IActionResult> Token([FromBody] LoginRequestDto @request)
    {
     

      var response = await this.mediator.Send(@request);

      return Ok(response);
    }


    [HttpPost("revokeAccessToken")]
    public async Task<IActionResult> RevokeAccessToken()
    {
      // ilgili user bulup revoke edelim;

      return Ok();
    }


    [HttpPost("refreshToken")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(TokenResponseDto))]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto @request)
    {

      var user = this.userManager.Users.FirstOrDefault(x => x.RefreshToken == request.RefreshToken);

      if (user != null)
      {

        // client ve sunucu eşlemesini kontrol et
        if (user.RefreshToken == request.RefreshToken)
        {

          var claims = new List<Claim>();

          var roles = this.userManager.GetRolesAsync(user);

          claims.Add(new Claim(ClaimTypes.Email, user.Email));
          claims.Add(new Claim("sub", user.Id));
          claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roles)));
          claims.Add(new Claim("permissions", System.Text.Json.JsonSerializer.Serialize("[]")));

          var token = this.accessTokenService.CreateAccessToken(new System.Security.Claims.ClaimsIdentity(claims));

          //user.RefreshToken = token.RefreshToken;
          //user.RefreshTokenExpireAt = DateTime.Now.AddMinutes(45);

          //await userManager.UpdateAsync(user);

          return Ok(token);
        }

      }




      return BadRequest();
     
    }

  }
}
