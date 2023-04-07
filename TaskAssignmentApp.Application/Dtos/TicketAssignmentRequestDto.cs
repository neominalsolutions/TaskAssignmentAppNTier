using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssignmentApp.Application.Dtos
{

  // required
  // stringLength
  // compare
  // max,min,range
  // emailaddress
  // regularExpression
  // Fluent Validation 
  public class TicketAssignmentRequestDto:IRequest<TicketAssignmentResponseDto>
  {
    [Required(ErrorMessage ="Bu alan boş geçilemez")]
    public string Description { get; set; }
    public string EmployeeId { get; set; }
    public int WorkingHour { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

  }
}
