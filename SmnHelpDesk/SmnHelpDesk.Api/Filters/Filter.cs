using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace SmnHelpDesk.Api.Filters
{
    public class Filter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return;
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}


