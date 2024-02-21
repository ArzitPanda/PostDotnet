using Microsoft.AspNetCore.Mvc.Filters;

namespace SlackApi.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
