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
    private readonly ITicketRepository _ticketRepository;

    public TicketAssignmentHandler(ITicketAssignmentCheckService ticketAssignmentCheckService, IEmployeeRepository employeeRepository, ITicketRepository ticketRepository)
    {
      _employeeRepository = employeeRepository;
      _ticketAssignmentCheckService = ticketAssignmentCheckService;
      _ticketRepository = ticketRepository;
    }
    public async Task<TicketAssignmentResponseDto> Handle(TicketAssignmentRequestDto request, CancellationToken cancellationToken)
    {
      var response = new TicketAssignmentResponseDto();

      var employees = await _employeeRepository.WhereAsync(x => x.Id == request.EmployeeId);

      if (employees.Count == 0)
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
          description: request.Description,
          workingHour: request.WorkingHour,
          startDate: request.StartDate,
          endDate: request.EndDate);

        ticket.Assign(
          employee: employees[0], ticketAssignmentCheckService: _ticketAssignmentCheckService);

       await _ticketRepository.Create(ticket);


      }


      return await Task.FromResult(response);
    }
  }
}
