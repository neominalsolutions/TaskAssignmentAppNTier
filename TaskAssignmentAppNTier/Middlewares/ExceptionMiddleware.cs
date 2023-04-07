using Microsoft.AspNetCore.Http;
using System;

namespace TaskAssignmentAppNTier.Middlewares
{
  public class ExceptionMiddleware 
  {
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      // tek bir yerden bütün kodu try cacth blogu içerisine alıyoruz
      try
      {
        await _next(context); // uygulama sürece devam etsin.
      }
      catch (Exception ex)
      {
        var ErrorResponse = new
        {
          title = "Application Error",
          status = StatusCodes.Status500InternalServerError,
          detail = "Application Error",
          errors = ex.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsJsonAsync(ErrorResponse);

      }

    }
  }
}
