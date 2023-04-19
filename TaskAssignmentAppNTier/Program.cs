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
      builder.Services.AddHttpContextAccessor(); // classlar üzerinden IHttpContextAccessor servicene eriþebilmek için bunu ekledik.

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      //builder.Services.AddSwaggerGen();

      // swagger api authenticate olmak için header üzerinden beaerer Access token test etme iþlemi
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



      // eðer uygulama içerisinde bir db baðlantýsý olacak servis ile çalýþýrsak bu durumda scoped servis tercihi yapalým.
      // scope serviceler dýþ kaynaklara baðlanýrken web request bazlý çalýþýr.
      // EF Core scope servisler ile tanýmlanmýþ, en performanslý çalýþma yöntemi scope service

      builder.Services.AddDbContext<TicketAppContext>(opt =>
      {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("TicketConn"));
      });

      builder.Services.AddDbContext<AppIdentityContext>(opt =>
      {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("TicketConn"));
      });

      var key = Encoding.ASCII.GetBytes(JWTSettings.SecretKey);

      // kimlik doðrulama yöntemini tanýtýrýz.
      // servis olarak tanýttýk
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

      // Middleware servisler handler servicler,sessionserviceler hep addTransient olarak tanýmlanmalýdýr. (Middleware handler,session request bazlý çalýþýr her bir istekte yeni instance alýnmasý gerekir.)
      //builder.Services.AddTransient<IMiddleware, ExceptionMiddleware>();





      // Web Socket, Redis Connection gibi tek bir instance ile çalýþacak isek Singleton, Validation,Session, Event Handling gibi durumlar için transient

      // File IO iþlemleri, Db iþlemleri için web request bazlý iþlemler için scope service tercih ediyoruz.

      // api js uygulamarýn istek atmasý için defautda cors yani cross origin request kapalý bunun  dýþarý aýlmasý lazým.
      builder.Services.AddCors(opt=>
      {
        opt.AddDefaultPolicy(policy =>
        {
          policy.AllowAnyHeader(); // js den gelen bütün header deðerlerine izin ver
          policy.AllowAnyMethod(); // js post,put,delete,get istekleri atýlabilir
          policy.AllowAnyOrigin(); // herhangi bir domain üzerinden istek atamaya izin ver. www.a.com wwww.b.com
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

      app.MapControllers(); // request controllara düþsün diye


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