using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.SeedWork;

namespace TaskAssigmentApp.Domain.Services
{
  /// <summary>
  /// Ticketlara veri tabanı üzerinden erişmek için bir servis açtık.
  /// </summary>
  public interface ITicketRepository:IRepository<Ticket>
  {
   
  }
}
