namespace StaffZone.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

	public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An unhandled exception occurred");
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";

		var response = new
		{
			message = "An error occurred while processing your request.",
			details = exception.Message
		};

		switch (exception)
		{
			case ArgumentException argEx:
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				response = new { message = "Invalid argument provided.", details = argEx.Message };
				break;

			case InvalidOperationException invOpEx:
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				response = new { message = "Invalid operation.", details = invOpEx.Message };
				break;

			case KeyNotFoundException notFoundEx:
				context.Response.StatusCode = StatusCodes.Status404NotFound;
				response = new { message = "Resource not found.", details = notFoundEx.Message };
				break;

			default:
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				response = new { message = "An unexpected error occurred.", details = "Please contact support if the problem persists." };
				break;
		}

		return context.Response.WriteAsJsonAsync(response);
	}
}
