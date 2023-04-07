using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssignmentApp.Application.Dtos
{
  public class TicketAssignmentRequestDto
  {
    public string Description { get; set; }
    public string EmployeeId { get; set; }
    public int WorkingHour { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

  }
}
