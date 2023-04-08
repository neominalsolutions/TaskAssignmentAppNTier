namespace TaskAssignmentAppNTier.Middlewares
{
  public static class MiddlewareExtentions
  {
    public static IApplicationBuilder UseException(this IApplicationBuilder applicationBuilder)
    {
      applicationBuilder.UseMiddleware<ExceptionMiddleware>();
      return applicationBuilder;
    }
  }
}
