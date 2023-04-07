using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;

namespace TaskAssigmentApp.Domain.Services
{
  public class TicketAssignmentWeeklyCheckService : ITicketAssignmentCheckService
  {
    private readonly ITicketRepository _ticketRepository;

    public TicketAssignmentWeeklyCheckService(ITicketRepository ticketRepository)
    {
      _ticketRepository = ticketRepository;
    }


    // DB bağımlılığımız var ama DB direkt olarak erişmeyiz bağımlılıkları yönetmek için ara bir service vasıtası ile bağlanmamız gerekir.
    public bool TicketIsAssignable(Employee emp, Ticket ticket)
    {
     var tickets = _ticketRepository.WhereAsync(x => x.EmployeeId == emp.Id && x.StartDate.Date >= new DateTime(2023, 04, 3) && x.EndDate.Date <= new DateTime(2023, 04, 7)).GetAwaiter().GetResult();

      // atanmış bir görev varsa 40 saat üstünde olmamalıdır.
      if(tickets != null)
      {
        int weekOfWorkHours = tickets.Sum(x => x.WorkingHour);


        if (weekOfWorkHours > 40)
          return false;
        else
        {
          int totalHours = weekOfWorkHours + ticket.WorkingHour;

          if (totalHours > 40)
            return false;
          else
            return true;
            
        }
          return true;
      }

      // atanmış görev yoksa görev girilebilir.
      return true;
      
    }
  }
}
