using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Application.Services;
using TaskAssignmentAppNTier.Attributes;

namespace TaskAssignmentAppNTier.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TicketsController : ControllerBase
  {
    private readonly ITicketAssignmentCheckService _ticketAssignmentCheckService;
    private readonly ITicketAssignment _ticketAssignment;
    private readonly ITicketRepository ticketRepository;

    public TicketsController(ITicketAssignmentCheckService ticketAssignmentCheckService, ITicketAssignment ticketAssignment, ITicketRepository ticketRepository)
    {
      _ticketAssignmentCheckService = ticketAssignmentCheckService;
      _ticketAssignment = ticketAssignment;
      this.ticketRepository = ticketRepository;
    }

    [HttpPost("assign-old")]
    public IActionResult AssignTicket()
    {
      // ticket nesnesine bağlanıp gerekli işlemleri yapalım

      var emp = new Employee(name:"Ali",surname:"Tan");

      var ticket = new Ticket(
        description: "Ticket-1", 
        workingHour: 6, 
        startDate: DateTime.Now,
        endDate: DateTime.Now);

     

      ticket.Assign(
        employee:emp, ticketAssignmentCheckService: _ticketAssignmentCheckService);



      // var response = mediator.HandleAsync(request);
      return Ok();
    }


    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // http header üzerinden access token gönderilmez ise bu durumda 401 status code
    [HttpPost("assing-new")]
    [Role("SuperVisor","admin","manager")]
    
    //[Permission("create-user")]
    public async Task<IActionResult> AssignTicketApplicationLayer([FromBody] TicketAssignmentRequestDto dto)
    {

      var result = await HttpContext.AuthenticateAsync(scheme: JwtBearerDefaults.AuthenticationScheme);
      var response =  await _ticketAssignment.HandleAsync(dto);

      // var response = mediator.HandleAsync(request);
      return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
      var model = await ticketRepository.ToListAsync(); // hepsini getir. employee ve ticket değerleri joinleniş olarak gelecek.

      var response = model.Select(a => new TicketRequestDto
      {
        Description = a.Description,
        EmployeeName = $"{a.Employee.Name} {a.Employee.SurName}",
        Id = a.Id,
        WorkingHour = a.WorkingHour
      }).ToList();

      return Ok(response);
    }

  }
}
