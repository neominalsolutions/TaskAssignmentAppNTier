using FluentValidation.AspNetCore;
using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Application.Services;
using TaskAssignmentApp.Application.Validators;
using TaskAssignmentApp.Infrastructure.Token.JWT;

namespace TaskAssignmentAppNTier.ServiceExtensions
{


  public static class ApplicationModuleServices
  {
    /// <summary>
    /// Ticket Nesnesinin program tanımlarını yapacak kişiler bu class üzerinden çalışacak.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      // mediator paket düzgün çalışması için service eklememiz lazım
      services.AddMediatR(configuration =>
      {
        // configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        // doğru library katmanında mediatr kullanılcağı için referans bir obje seçip oradaki dll'i current projecte e refrection ile çalışma zamanında load ediyoruz.
        configuration.RegisterServicesFromAssemblyContaining<TicketAssignmentRequestDto>();
      });


      services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TicketAssignmentValidator>()); // controller implemente olsun diye


      services.AddSingleton<IAccessTokenService, JwtTokenService>();

      return services;
    }
  }
}
