using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Persistance.ORM.EntityFramework.Contexts;

namespace TaskAssignmentApp.Infrastructure.ORM.EntityFramework
{
  public class EFTicketRepository : EFBaseRepository<Ticket, TicketAppContext>, ITicketRepository
  {
    public EFTicketRepository(TicketAppContext context) : base(context)
    {
    }
  }
}
