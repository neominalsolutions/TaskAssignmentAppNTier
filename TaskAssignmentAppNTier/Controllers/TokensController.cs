using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Application.Exceptions;
using TaskAssignmentApp.Infrastructure.Token.JWT;

namespace TaskAssignmentAppNTier.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TokensController : ControllerBase
  {
    private readonly IMediator mediator;

    public TokensController(IMediator mediator)
    {
      this.mediator = mediator;
    }


    [HttpPost("getAccessToken")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK,Type = typeof(TokenResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesErrorResponseType(typeof(AuthenticationFailedException))]
    public async Task<IActionResult> Token([FromBody] LoginRequestDto @request)
    {
      var response = await this.mediator.Send(@request);

      return Ok(response);
    }

  }
}
