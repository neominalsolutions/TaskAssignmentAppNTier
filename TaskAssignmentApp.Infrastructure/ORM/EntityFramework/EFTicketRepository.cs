using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public override Task<List<Ticket>> WhereAsync(Expression<Func<Ticket, bool>> expression = null)
    {
      return dbContext.Tickets.AsNoTracking().Include(x => x.Employee).Where(expression).ToListAsync();
    }

    public override Task<List<Ticket>> ToListAsync()
    {
      return dbContext.Tickets.AsNoTracking().Include(x => x.Employee).ToListAsync();
    }
  }
}
