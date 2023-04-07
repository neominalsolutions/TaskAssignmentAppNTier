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

      // e�er uygulama i�erisinde bir db ba�lant�s� olacak servis ile �al���rsak bu durumda scoped servis tercihi yapal�m.
      // scope serviceler d�� kaynaklara ba�lan�rken web request bazl� �al���r.
      // EF Core scope servisler ile tan�mlanm��, en performansl� �al��ma y�ntemi scope service
      builder.Services.AddScoped<ITicketAssignmentCheckService, TicketAssignmentWeeklyCheckService>();
      builder.Services.AddScoped<ITicketRepository, TicketRepository>();
      builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
      builder.Services.AddScoped<ITicketAssignment, TicketAssignmentService>();

      // Web Socket, Redis Connection gibi tek bir instance ile �al��acak isek Singleton, Validation,Session, Event Handling gibi durumlar i�in transient

      // File IO i�lemleri, Db i�lemleri i�in web request bazl� i�lemler i�in scope service tercih ediyoruz.

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers(); // request controllara d��s�n diye

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