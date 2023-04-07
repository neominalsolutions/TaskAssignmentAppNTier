using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssignmentApp.Application.Dtos;

namespace TaskAssignmentApp.Application.Services
{
  public interface ITicketAssignment:IRequestService<TicketAssignmentRequestDto,TicketAssignmentResponseDto>
  {
     
  }
}
