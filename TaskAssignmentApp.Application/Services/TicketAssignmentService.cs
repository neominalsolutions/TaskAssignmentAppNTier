using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Entities;
using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Application.Dtos;

namespace TaskAssignmentApp.Application.Services
{
  // uygulamanın nasıl bir davranış ile görev verme işlemi yapıcağını koordine ediyorsunuz.
  public class TicketAssignmentService : ITicketAssignment
  {

    // Facade Design Patter: Bir kopleks işi tek bir servis üzerinden daha anlaşılır bir şekilde encapsulate etme yöntemi

    private readonly ITicketAssignmentCheckService _ticketAssignmentCheckService;
    private readonly IEmployeeRepository _employeeRepository;
    // Email Service yapıcaz

    public TicketAssignmentService(ITicketAssignmentCheckService ticketAssignmentCheckService, IEmployeeRepository employeeRepository)
    {
      _ticketAssignmentCheckService = ticketAssignmentCheckService;
      _employeeRepository = employeeRepository;

    }

    // employee Repository bağlanıp ilgili employee find etmek lazım.

    public async Task<TicketAssignmentResponseDto> HandleAsync(TicketAssignmentRequestDto request)
    {
      var response = new TicketAssignmentResponseDto();

      var emp = new Employee(name: "Ali", surname: "Tan");

      var employee = await  _employeeRepository.WhereAsync(x=> x.Id == request.EmployeeId);

      if(employee == null)
      {
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
