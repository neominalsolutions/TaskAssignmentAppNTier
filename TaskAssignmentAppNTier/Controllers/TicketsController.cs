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

    public TicketsController(ITicketAssignmentCheckService ticketAssignmentCheckService, ITicketAssignment ticketAssignment)
    {
      _ticketAssignmentCheckService = ticketAssignmentCheckService;
      _ticketAssignment = ticketAssignment;
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

  }
}
