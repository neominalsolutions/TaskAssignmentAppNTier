using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssigmentApp.Domain.Exception
{
  public class MaximumAndMinimumTicketTrashholdOverFlowException: ApplicationException
  {
    public MaximumAndMinimumTicketTrashholdOverFlowException():base("Minimum 2 saat Makimum olarak 16 saatlik bir görev ataması yapılabilir")
    {

    }
  }
}
