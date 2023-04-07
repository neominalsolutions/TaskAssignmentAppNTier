using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.SeedWork;
using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Persistance.ORM.EntityFramework.Contexts;

namespace TaskAssignmentApp.Infrastructure.ORM.EntityFramework
{
  public class EFEmployeeRepository : EFBaseRepository<Employee,TicketAppContext> ,IEmployeeRepository
  {
    private readonly TicketAppContext ticketAppContext;
    public EFEmployeeRepository(TicketAppContext context) : base(context)
    {
      ticketAppContext = context;
    }

    /// <summary>
    /// Nesneye özgü bir method varsa interface ile bu methodun implementasyonu yapılıyor
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Employee> FindByNameAsync(string name)
    {
      return ticketAppContext.Employees.FirstOrDefaultAsync(x => x.Name == name);
    }
  }
}
