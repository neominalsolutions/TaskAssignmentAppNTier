using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskAssignmentApp.Infrastructure.Token.JWT;

namespace TaskAssignmentApp.Application.Dtos
{
  public class LoginRequestDto:IRequest<TokenResponseDto>
  {
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

  }
}
