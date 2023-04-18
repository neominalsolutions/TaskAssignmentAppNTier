using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [ProducesErrorResponseType(typeof(AuthenticationFailedException))]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto @request)
    {

      var claims = HttpContext.User.Claims.ToList();

    

    
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

         var user =  await this.userManager.FindByIdAsync(userId);

        // client ve sunucu eşlemesini kontrol et
        if(user.RefreshToken == request.RefreshToken && user.RefreshTokenExpireAt == request.RefreshTokenExpireAt && user.RefreshTokenRevoked != false)
        {
          var token = this.accessTokenService.CreateAccessToken(new System.Security.Claims.ClaimsIdentity(HttpContext.User.Claims));

          user.RefreshToken = token.RefreshToken;
          user.RefreshTokenExpireAt = DateTime.Now.AddMinutes(45);

          await userManager.UpdateAsync(user);

          return Ok(token);
        }


      return BadRequest();
     
    }

  }
}
