using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.Exception;
using TaskAssigmentApp.Domain.Exceptions;
using TaskAssigmentApp.Domain.Services;

namespace TaskAssigmentApp.Domain.Entities
{
  public class Ticket
  {
    /// <summary>
    /// Minimumda 2 saatlik bir iş ataması olabilir
    /// </summary>
    private const int minimumThreshhold = 2;
    /// <summary>
    /// Maksimumda 2 günlük iş atanabilir
    /// </summary>
    private const int maximumThreshold = 16; // yani 2 günlük iş

    public string Id { get; private set; }
    public string Description { get; private set; }

    public string EmployeeId { get; set; }

    public Employee Employee { get; set; }



    /// <summary>
    /// Ne kadarlık bir iş
    /// </summary>
    public int WorkingHour { get; private set; }

    /// <summary>
    /// Görev başlangıç Tarihi
    /// </summary>
    public DateTime StartDate { get; private set; }

    /// <summary>
    /// Görev Bitiş Tarihi
    /// </summary>
    public DateTime EndDate { get; private set; }

    /// <summary>
    /// Ticket nesnesini oluşturuken hangi değerlerler required ise constructor üzerinden alalım
    /// </summary>
    public Ticket(string description, int workingHour, DateTime startDate, DateTime endDate)
    {
      Id = Guid.NewGuid().ToString();
      this.Description = description;
      this.WorkingHour = workingHour;
      this.StartDate = startDate;
      this.EndDate = endDate;

    }

    public Ticket()
    {

    }



    /// <summary>
    /// Maksimum 40 saatlik bir iş tanımı kontrol edeceğiz. Eğer Maksimum Hafta limiti dolduysa aşıldıysa MaksimumWorkOfWeekLimitOverFlowException vereceğiz.
    /// </summary>
    /// <param name="employee"></param>
    public void Assign(Employee employee, ITicketAssignmentCheckService ticketAssignmentCheckService)
    {
      // burada ise ilgili çalışana haftalık maksimum atanacak olan görev kontrolü yapılsın
      // 1 günlük görev atamasını 8 saat olarak kabul ediyoruz Bu durumda haftalık 40 saat makimum iş tanımı yapılabilir.
      // burada dbden çalışanın o hafta içerisindeki tüm çalışma saatlerinin toplamını bulup, buna göre bir karar veremem lazım.

      if(this.WorkingHour < minimumThreshhold || this.WorkingHour > maximumThreshold)
      {
        // Exception Fırlat

        throw new MaximumAndMinimumTicketTrashholdOverFlowException();
      }
      else if (!ticketAssignmentCheckService.TicketIsAssignable(employee, this))
      {
        throw new MaxsimumWorkOfWeekLimitOverFlowException();
      }

      employee.AddTicket(this);

      // Employee için ticket ekleme işlemi gerçekleştir.


    }




  }
}
