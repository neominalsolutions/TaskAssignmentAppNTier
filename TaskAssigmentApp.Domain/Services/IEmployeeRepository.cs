using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.SeedWork;

namespace TaskAssigmentApp.Domain.Services
{
  public interface IEmployeeRepository:IRepository<Employee>
  {
    Task<Employee> FindByNameAsync(string name);
  }
}
