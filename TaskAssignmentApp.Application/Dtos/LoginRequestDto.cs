﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssignmentApp.Infrastructure.Token.JWT;

namespace TaskAssignmentApp.Application.Dtos
{
  public class LoginRequestDto:IRequest<TokenResponseDto>
  {
    public string Email { get; set; }
    public string Password { get; set; }

  }
}
