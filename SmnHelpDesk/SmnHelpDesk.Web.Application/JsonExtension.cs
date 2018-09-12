using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace SmnHelpDesk.Web.Application
{
    public static class JsonExtension
    {
        public static T DeserealizeJson<T>(this HttpContent conteudo)
        {
            return JsonConvert.DeserializeObject<T>(conteudo.ReadAsStringAsync().Result);
        }
        public static string DeserealizeJsonAsString(this HttpContent conteudo)
        {
            return conteudo.ReadAsStringAsync().Result.Replace(Environment.NewLine, string.Empty);
        }
    }
}
