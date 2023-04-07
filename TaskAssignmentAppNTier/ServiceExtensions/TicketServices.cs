using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Application.Services;

namespace TaskAssignmentAppNTier.ServiceExtensions
{
  public static class TicketServices
  {
    /// <summary>
    /// Ticket Nesnesinin program tanımlarını yapacak kişiler bu class üzerinden çalışacak.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddTicketServices(this IServiceCollection services)
    {
      services.AddScoped<ITicketAssignment, TicketAssignmentService>();
      services.AddScoped<ITicketAssignmentCheckService, TicketAssignmentWeeklyCheckService>();
      services.AddScoped<ITicketRepository, TicketRepository>();

      return services;
    }
  }
}
