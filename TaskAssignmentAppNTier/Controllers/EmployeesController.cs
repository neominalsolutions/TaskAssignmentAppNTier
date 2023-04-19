using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Application.Dtos;

namespace TaskAssignmentAppNTier.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeesController : ControllerBase
  {

    private readonly IEmployeeRepository employeeRepository;

    public EmployeesController(IEmployeeRepository employeeRepository)
    {
      this.employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployee()
    {
      var employees = await this.employeeRepository.ToListAsync();

      var dto = employees.Select(a => new EmployeeRequestDto
      {
        FullName = $"{a.Name.Trim()} {a.SurName.ToUpper()}",
        Id=a.Id
      });

      return Ok(dto);
    }
  }
}
