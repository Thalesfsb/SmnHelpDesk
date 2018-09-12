using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmnHelpDesk.Web.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Sucesso(object model = null)
        {
            Response.StatusCode = 200;
            return Content(JsonConvert.SerializeObject(model));
        }

        public ActionResult Error(List<string> erros)
        {
            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true;
            return Content(JsonConvert.SerializeObject(erros));
        }
    }
}