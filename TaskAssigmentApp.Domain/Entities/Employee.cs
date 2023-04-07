using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssigmentApp.Domain.Entities
{
  public class Employee
  {
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string SurName { get; private set; }

    public Employee()
    {

    }

    public Employee(string name,string surname)
    {
      Id = Guid.NewGuid().ToString();
      this.Name = name;
      this.SurName = surname;

    }

    /// <summary>
    /// Çalışana atanan ticketlar
    /// </summary>
    public IReadOnlyCollection<Ticket> Tickets => _tickets;
    public List<Ticket> Tickets1 = new List<Ticket>();

    // has a relation ship

    private List<Ticket> _tickets = new List<Ticket>();

    /// <summary>
    /// Sadece Method üzerinden set edilmesi için AddTicket komutu kullandık
    /// </summary>
    /// <param name="ticket"></param>
    public void AddTicket(Ticket ticket)
    {
      // employee yeni bir görev eklediik.
      _tickets.Add(ticket);
    }

  }
}
