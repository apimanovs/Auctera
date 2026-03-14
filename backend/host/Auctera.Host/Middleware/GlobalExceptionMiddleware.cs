using Microsoft.AspNetCore.Mvc;

namespace Auctera.Host.Middleware;

/// <summary>
/// Represents the global exception middleware class.
/// </summary>
public sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger,
    IHostEnvironment environment)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;
    private readonly IHostEnvironment _environment = environment;

    /// <summary>
    /// Performs the invoke operation.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception,
                "Unhandled exception occurred while processing request {Path}. Full error: {Error}",
                context.Request.Path,
                exception.ToString());

            var problemDetails = BuildProblemDetails(context, exception);

            context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Performs the build problem details operation.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="exception">Exception.</param>
    /// <returns>The operation result.</returns>
    private ProblemDetails BuildProblemDetails(HttpContext context, Exception exception)
    {
        var (statusCode, title) = exception switch
        {
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = $"https://httpstatuses.com/{statusCode}",
            Detail = _environment.IsDevelopment() ? exception.ToString() : "An unexpected error occurred.",
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        return problemDetails;
    }
}
