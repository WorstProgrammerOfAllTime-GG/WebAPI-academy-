namespace ProjectAcademy.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate; _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                throw;
            }

            switch (httpContext.Response.StatusCode)
            {
                case 400:
                    _logger.LogWarning("Invalid request data");
                    break;

                case 401:
                    _logger.LogWarning("Authentication failed");
                    break;
            }
        }
        
    }
}
