using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssignmentApp.Application.Exceptions
{
  public class AuthenticationFailedException:ApplicationException
  {
    public AuthenticationFailedException():base("Böyle bir kullanıcı kaydı bulunamadı")
    {

    }
  }
}
