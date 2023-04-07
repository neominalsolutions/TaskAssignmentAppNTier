using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssignmentApp.Application.Dtos;

namespace TaskAssignmentApp.Application.Validators
{
  public class TicketAssignmentValidator:AbstractValidator<TicketAssignmentRequestDto>
  {

    public TicketAssignmentValidator()
    {
      RuleFor(x => x.Description).MaximumLength(200).WithMessage("Ticket Description alanı 200 karakter ile sınırlıdır").NotNull().NotEmpty().WithMessage("Ticket Description alanı zorunlu bir alandır boş geçilemez");
      RuleFor(x => x.EmployeeId).NotNull().NotEmpty().WithMessage("Çalışan ataması yapılmalıdır");
      RuleFor(x => x.StartDate).Must(StartDateControl).WithMessage("Geçmiş tarihli bir görev tanımı yapılamaz");
     
    }

    /// <summary>
    /// Ticket başlangıç tarihi geçmiş bir tarih girilemez
    /// </summary>
    /// <param name="startDate"></param>
    /// <returns></returns>
    private bool StartDateControl(DateTime startDate)
    {
      if(startDate.Date < DateTime.Now.Date)
      {
        return false;
      }

      return true;
    }
  }
}
