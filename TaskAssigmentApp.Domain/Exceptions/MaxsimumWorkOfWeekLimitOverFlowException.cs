using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssigmentApp.Domain.Exceptions
{
  public class MaxsimumWorkOfWeekLimitOverFlowException:ApplicationException
  {
    public MaxsimumWorkOfWeekLimitOverFlowException():base("Haftalık 40 saat üzerinde bir görev tanımı yapılamaz")
    {

    }
  }
}
