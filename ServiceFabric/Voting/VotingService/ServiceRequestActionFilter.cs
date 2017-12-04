using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace VotingService
{
    internal class ServiceRequestActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string activityId = Guid.NewGuid().ToString();

            ServiceEventSource.Current.ServiceRequestStart(actionContext.ActionDescriptor.ActionName, activityId);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ServiceEventSource.Current.ServiceRequestStop(actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                actionExecutedContext.Exception?.ToString() ?? string.Empty);
        }
    }
}
