using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Reflection;
using System.Text;
using TaskAssigmentApp.Domain.Services;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Application.Services;
using TaskAssignmentApp.Application.Validators;
using TaskAssignmentApp.Infrastructure.Token.JWT;
using TaskAssignmentApp.Persistance.ORM.EntityFramework.Contexts;
using TaskAssignmentApp.Persistance.ORM.EntityFramework.Identity;
using TaskAssignmentAppNTier.Middlewares;
using TaskAssignmentAppNTier.ServiceExtensions;

namespace TaskAssignmentAppNTier
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddAuthorization();
      builder.Services.AddHttpContextAccessor(); // classlar �zerinden IHttpContextAccessor servicene eri�ebilmek i�in bunu ekledik.

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      //builder.Services.AddSwaggerGen();

      // swagger api authenticate olmak i�in header �zerinden beaerer Access token test etme i�lemi
      builder.Services.AddSwaggerGen(opt =>
      {

        var securityScheme = new OpenApiSecurityScheme()
        {
          Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.Http,
          Scheme = JwtBearerDefaults.AuthenticationScheme,
          BearerFormat = "JWT" // Optional
        };

        var securityRequirement = new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "bearerAuth"
            }
        },
        new string[] {}
    }
};

        opt.AddSecurityDefinition("bearerAuth", securityScheme);
        opt.AddSecurityRequirement(securityRequirement);
      });



      // e�er uygulama i�erisinde bir db ba�lant�s� olacak servis ile �al���rsak bu durumda scoped servis tercihi yapal�m.
      // scope serviceler d�� kaynaklara ba�lan�rken web request bazl� �al���r.
      // EF Core scope servisler ile tan�mlanm��, en performansl� �al��ma y�ntemi scope service

      builder.Services.AddDbContext<TicketAppContext>(opt =>
      {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("TicketConn"));
      });

      builder.Services.AddDbContext<AppIdentityContext>(opt =>
      {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("TicketConn"));
      });

      var key = Encoding.ASCII.GetBytes(JWTSettings.SecretKey);

      // kimlik do�rulama y�ntemini tan�t�r�z.
      // servis olarak tan�tt�k
      builder.Services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
            .AddJwtBearer(x =>
            {
              x.RequireHttpsMetadata = true;
              x.SaveToken = true;
              x.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
              };

            });


      builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
      {
        opt.User.RequireUniqueEmail = true;
        opt.Password.RequireDigit = true;
        opt.Password.RequireLowercase = true;
        opt.Password.RequireUppercase = true;
        opt.Password.RequireNonAlphanumeric = false;

      }).AddEntityFrameworkStores<AppIdentityContext>();

      builder.Services.AddApplicationServices();
      builder.Services.AddTicketServices();

      // Middleware servisler handler servicler,sessionserviceler hep addTransient olarak tan�mlanmal�d�r. (Middleware handler,session request bazl� �al���r her bir istekte yeni instance al�nmas� gerekir.)
      //builder.Services.AddTransient<IMiddleware, ExceptionMiddleware>();





      // Web Socket, Redis Connection gibi tek bir instance ile �al��acak isek Singleton, Validation,Session, Event Handling gibi durumlar i�in transient

      // File IO i�lemleri, Db i�lemleri i�in web request bazl� i�lemler i�in scope service tercih ediyoruz.

      // api js uygulamar�n istek atmas� i�in defautda cors yani cross origin request kapal� bunun  d��ar� a�lmas� laz�m.
      builder.Services.AddCors(opt=>
      {
        opt.AddDefaultPolicy(policy =>
        {
          policy.AllowAnyHeader(); // js den gelen b�t�n header de�erlerine izin ver
          policy.AllowAnyMethod(); // js post,put,delete,get istekleri at�labilir
          policy.AllowAnyOrigin(); // herhangi bir domain �zerinden istek atamaya izin ver. www.a.com wwww.b.com
        });
      });

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

 
      app.UseHttpsRedirection();
      app.UseAuthentication(); // middleware aktif ettik.
      app.UseAuthorization();

      app.MapControllers(); // request controllara d��s�n diye


      app.UseException();
      //app.UseMiddleware<ExceptionMiddleware>();


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

      app.UseCors();

      app.Run();
    }
  }
}