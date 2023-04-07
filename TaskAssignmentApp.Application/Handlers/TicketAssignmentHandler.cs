using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Application.Exceptions;

namespace TaskAssignmentApp.Application.Handlers
{
  public class TicketAssignmentHandler : IRequestHandler<TicketAssignmentRequestDto, TicketAssignmentResponseDto>
  {
    private readonly ITicketAssignmentCheckService _ticketAssignmentCheckService;
    private readonly IEmployeeRepository _employeeRepository;

    public TicketAssignmentHandler(ITicketAssignmentCheckService ticketAssignmentCheckService, IEmployeeRepository employeeRepository)
    {
      _employeeRepository = employeeRepository;
      _ticketAssignmentCheckService = ticketAssignmentCheckService;
    }
    public async Task<TicketAssignmentResponseDto> Handle(TicketAssignmentRequestDto request, CancellationToken cancellationToken)
    {
      var response = new TicketAssignmentResponseDto();

      var employee = await _employeeRepository.WhereAsync(x => x.Id == request.EmployeeId);

      if (employee == null)
      {
        throw new EmployeeNotFoundException();

        // application exception
        // böyle bir çalışan kaydı yok
      }
      else
      {
        //emp.Tickets1.cle
        //emp.AddTicket(new Ticket());

        // dto entity maplendi.

        var ticket = new Ticket(
          description: "Ticket-1",
          workingHour: 6,
          startDate: DateTime.Now,
          endDate: DateTime.Now);

        ticket.Assign(
          employee: employee[0], ticketAssignmentCheckService: _ticketAssignmentCheckService);
      }


      return await Task.FromResult(response);
    }
  }
}
