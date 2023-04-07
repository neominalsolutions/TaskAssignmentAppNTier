using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Application.Services;

namespace TaskAssignmentAppNTier
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddAuthorization();

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddControllers(); // controller implemente olsun diye

      // eðer uygulama içerisinde bir db baðlantýsý olacak servis ile çalýþýrsak bu durumda scoped servis tercihi yapalým.
      // scope serviceler dýþ kaynaklara baðlanýrken web request bazlý çalýþýr.
      // EF Core scope servisler ile tanýmlanmýþ, en performanslý çalýþma yöntemi scope service
      builder.Services.AddScoped<ITicketAssignmentCheckService, TicketAssignmentWeeklyCheckService>();
      builder.Services.AddScoped<ITicketRepository, TicketRepository>();
      builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
      builder.Services.AddScoped<ITicketAssignment, TicketAssignmentService>();

      // Web Socket, Redis Connection gibi tek bir instance ile çalýþacak isek Singleton, Validation,Session, Event Handling gibi durumlar için transient

      // File IO iþlemleri, Db iþlemleri için web request bazlý iþlemler için scope service tercih ediyoruz.

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers(); // request controllara düþsün diye

      var summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

      app.MapGet("/weatherforecast", (HttpContext httpContext) =>
      {
        var forecast = Enumerable.Range(1, 5).Select(index =>
              new WeatherForecast
              {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
              })
              .ToArray();
        return forecast;
      })
      .WithName("GetWeatherForecast");

      app.Run();
    }
  }
}