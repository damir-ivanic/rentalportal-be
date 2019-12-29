using Microsoft.AspNetCore.Mvc.Filters;
using rentalportal.model.Core;

namespace rentalportal.api.Filters
{
    public class TransactionFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var unitOfWork = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

            if (context.Exception == null &&
                context.HttpContext.Response.StatusCode >= 200 &&
                context.HttpContext.Response.StatusCode < 300)
            {
                if (context.ModelState.IsValid)
                {
                    unitOfWork.SaveChanges();
                }
            }
        }
    }
}
