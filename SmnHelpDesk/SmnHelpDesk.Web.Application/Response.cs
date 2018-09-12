using System.Collections.Generic;
using System.Net;
using System.Net.Http;


namespace SmnHelpDesk.Web.Application
{
    public class Response
    {
        public Response(HttpResponseMessage responseMessage)
        {
            Ok = (int)responseMessage.StatusCode == 200;
            if (Ok)
                return;

            Erros = responseMessage.StatusCode == HttpStatusCode.InternalServerError
                ? new List<string> { responseMessage.Content.DeserealizeJsonAsString() }
                : responseMessage.Content.DeserealizeJson<List<string>>();
        }

        public bool Ok { get; set; }
        public List<string> Erros { get; set; }
    }

    public class Response<T> : Response
    {
        public T Dados { get; set; }

        public Response(HttpResponseMessage responseMessage) : base(responseMessage)
        {
            if (Ok)
                Dados = responseMessage.Content.DeserealizeJson<T>();
        }
    }
}
