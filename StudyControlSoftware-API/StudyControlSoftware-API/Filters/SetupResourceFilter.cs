using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudyControlSoftware_API.Database;

namespace StudyControlSoftware_API.Filters
{
    public class SetupResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            ApplicationContext? appContext = context.HttpContext.RequestServices.GetService<ApplicationContext>();

            if (appContext == null || appContext!.Users.Any())
                context.Result = new BadRequestResult();
        }

        public void OnResourceExecuted(ResourceExecutedContext context) { }
    }
}
