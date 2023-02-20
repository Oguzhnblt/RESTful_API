using Microsoft.AspNetCore.Http;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine($"Request received: {context.Request.Method} {context.Request.Path}"); // Hangi action'a girildiği bilgisii

        await _next(context);

        Console.WriteLine($"Action executed"); // Eylemin gerçekleştiğine dair mesaj 
    }
}

