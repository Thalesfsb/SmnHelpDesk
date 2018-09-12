using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Routing;

namespace SmnHelpDesk.Web.Application
{
    public static class UriWebApi
    {
        private static HttpClient _client;

        public static HttpResponseMessage Get(string route, object parameters)
        {
            return SetCliente().GetAsync(Route(route, parameters)).Result;
        }

        public static HttpResponseMessage Post(string route, object content)
        {
            return SetCliente().PostAsync(route, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")).Result;
        }

        public static HttpResponseMessage Put(string route, object content)
        {
            return SetCliente().PutAsync(route, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")).Result;
        }

        public static HttpResponseMessage Delete(string route, object parameters)
        {
            return SetCliente().DeleteAsync(Route(route, parameters)).Result;
        }

        private static string Route(object route, object parameters)
        {
            var r = route?.ToString() ?? string.Empty;
            if (parameters == null)
                return r;

            var queryString = new List<string>();
            foreach (var prop in new RouteValueDictionary(parameters))
            {
                var qs = $"{prop.Key}=";
                var @decimal = prop.Value as decimal?;
                var dateTime = prop.Value as DateTime?;
                if (@decimal != null)
                    qs += Regex.Replace(@decimal.ToString(), @"(?<=\d)\,(?=\d)", ".", RegexOptions.Compiled);
                else if (dateTime != null)
                    qs += dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    qs += prop.Value;
                queryString.Add(qs);
            }

            return r + (queryString.Any() ? "?" + string.Join("&", queryString) : string.Empty);
        }

        private static HttpClient SetCliente()
        {
            _client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["CaminhoApi"]) };
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return _client;
        }
    }
}
