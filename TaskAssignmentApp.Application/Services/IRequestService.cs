using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssignmentApp.Application.Services
{
  public interface IRequestService<TRequestDto,TResponseDto> where TResponseDto:new()
  {
    Task<TResponseDto> HandleAsync(TRequestDto @request);
  }
}
