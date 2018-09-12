namespace SmnHelpDesk.Web.Application
{
    public class BaseApplication
    {
        protected readonly string Controller;
        protected BaseApplication(string controller)
        {
            Controller = controller;
        }

        protected Response<T> Get<T>(object id = null, string route = null, object content = null)
        {
            var rota = string.IsNullOrEmpty(route) ? Controller : route;
            rota += id != null ? "/" + id : string.Empty;
            return new Response<T>(UriWebApi.Get(rota, content));
        }

        protected Response Put(object content, int? id = null, string route = null)
        {
            var rota = string.IsNullOrEmpty(route) ? Controller : route;
            return new Response(UriWebApi.Put($"{rota}/{id}", content));
        }

        public Response Post(object content, string route = null)
        {
            var rota = string.IsNullOrEmpty(route) ? Controller : route;
            return new Response(UriWebApi.Post(rota, content));
        }

        protected Response<T> Delete<T>(decimal? cpfCnpj, object content = null, string route = null)
        {
            var rota = string.IsNullOrEmpty(route) ? Controller : route;
            return new Response<T>(UriWebApi.Delete($"{rota}/{cpfCnpj}", content));
        }

        public Response<T> Post<T>(object content, string route = null)
        {
            var rota = string.IsNullOrEmpty(route) ? Controller : route;
            return new Response<T>(UriWebApi.Post(rota, content));
        }
        protected string ControllerAction(string action) => Controller + "/" + action;
    }
}
