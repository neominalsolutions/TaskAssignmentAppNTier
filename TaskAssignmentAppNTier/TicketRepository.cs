using System.Linq.Expressions;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.Services;

namespace TaskAssignmentAppNTier
{
  public class TicketRepository : ITicketRepository
  {
    public Task Create(Ticket entity)
    {
      throw new NotImplementedException();
    }

    public Task<List<Ticket>> WhereAsync(Expression<Func<Ticket, bool>> expression)
    {
      throw new NotImplementedException();
    }
  }
}
