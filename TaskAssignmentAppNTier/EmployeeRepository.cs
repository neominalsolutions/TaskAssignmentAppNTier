using System.Linq.Expressions;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.Services;

namespace TaskAssignmentAppNTier
{
  public class EmployeeRepository : IEmployeeRepository
  {
    public Task Create(Employee entity)
    {
      throw new NotImplementedException();
    }

    public Task<List<Employee>> WhereAsync(Expression<Func<Employee, bool>> expression)
    {
      throw new NotImplementedException();
    }
  }
}
