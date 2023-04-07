using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;

namespace TaskAssigmentApp.Domain.Services
{
  /// <summary>
  /// Domaini ilgilendiren Bussiness Kuralların kontrol edildiği servislere Domain Service ismi veriyoruz.
  /// </summary>
  public interface ITicketAssignmentCheckService
  {
    /// <summary>
    /// Gönderilen Ticket bilgisi kişiye atababilir mi kontrolü yapar.
    /// </summary>
    /// <param name="emp"></param>
    /// <param name="ticket"></param>
    /// <returns></returns>
    bool TicketIsAssignable(Employee emp, Ticket ticket);
  }
}
