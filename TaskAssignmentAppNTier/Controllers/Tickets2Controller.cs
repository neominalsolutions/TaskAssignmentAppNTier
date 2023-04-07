using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Application.Services;

namespace TaskAssignmentAppNTier.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class Tickets2Controller : ControllerBase
  {
    private readonly IMediator _mediator;

    public Tickets2Controller(IMediator mediator)
    {
      _mediator = mediator;
    }


    [HttpPost("assing-new")]
    //[ExceptionFilter]
    public async Task<IActionResult> AssignTicketApplicationLayer([FromBody] TicketAssignmentRequestDto dto)
    {

      try
      {
        // mediator gelen request işlemek için send methodu kullanıyor
        var response = await _mediator.Send(dto);
        return Ok(response);
      }
      catch (Exception ex)
      {

        throw;
      }


      // var response = mediator.HandleAsync(request);
   
    }


  }
}
