using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssignmentApp.Application.Dtos
{
  public class RefreshTokenRequestDto
  {
    public string AcessToken { get; set; }
    public string RefreshToken { get; set; }

    public DateTime? RefreshTokenExpireAt { get; set; }


  }
}
