
using Exceptions;

namespace StockApp.Middleware
{
    public class ExcpetionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<Exception> _logger;
        public ExcpetionHandlingMiddleware(ILogger<Exception> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (FinnhubException ex)
            {
                LogException(ex);
                throw;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        private void LogException(Exception ex)
        {
            if(ex.InnerException != null)
            {
                if(ex.InnerException.InnerException != null)
                {
                    _logger.LogError("{ExceptionType} {ExceptionMessage}", ex.InnerException.InnerException.GetType().ToString(), ex.InnerException.InnerException.Message);
                }
                else
                {
                    _logger.LogError("{ExceptionType} {ExceptionMessage}", ex.InnerException.GetType().ToString(), ex.InnerException.Message);
                }
            }
            else
            {
                _logger.LogError("{ExceptionType} {ExceptionMessage}", ex.GetType().ToString(), ex.Message);
            }            
        }
    }
}
