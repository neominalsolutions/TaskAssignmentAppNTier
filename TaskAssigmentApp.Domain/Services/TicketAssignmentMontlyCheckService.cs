using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;

namespace TaskAssigmentApp.Domain.Services
{

  /// <summary>
  /// Not Uygulama duruma göre bazı yerlerde aylık görev ataması yapabilir bazı durumlarda haftalık görev ataması yapabilir diye Open Closed mantığı kullanılarak, ITicketAssignmentCheckService service üzerinden sınıflar implemente edildi ve farklı serviceler ayrı bir kodbase olarak geliştirildi.
  /// </summary>
  internal class TicketAssignmentMontlyCheckService : ITicketAssignmentCheckService
  {
    /// <summary>
    /// Aylık toplam görev dağılımı için gerekli algoritmaları yazıcağız.
    /// Görev dağılımı yapılırken ay içerisinde çalışanın izinleri var mı kontrolü yapılır ona göre bir dağılım gereçekleştirelim.
    /// </summary>
    /// <param name="emp"></param>
    /// <param name="ticket"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool TicketIsAssignable(Employee emp, Ticket ticket)
    {
      throw new NotImplementedException();
    }
  }
}
