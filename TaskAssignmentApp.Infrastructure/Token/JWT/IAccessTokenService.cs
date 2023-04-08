using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace TaskAssignmentApp.Infrastructure.Token.JWT
{
    public interface IAccessTokenService
    {
        TokenResponseDto CreateAccessToken(ClaimsIdentity identity);
    }
}
