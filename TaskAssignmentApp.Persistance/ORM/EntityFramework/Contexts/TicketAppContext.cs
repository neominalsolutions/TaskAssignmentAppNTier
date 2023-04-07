using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;

namespace TaskAssignmentApp.Persistance.ORM.EntityFramework.Contexts
{
  public class TicketAppContext:DbContext
  {
    public TicketAppContext(DbContextOptions<TicketAppContext> opt):base(opt)
    {

    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

  }
}
