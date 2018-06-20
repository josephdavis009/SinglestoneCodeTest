using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Singlestone_JBD_061318.Helpers
{
    public static class HttpHelper
    {
        public static HttpResponseMessage GetResponse(Uri baseAddress, string requestUri)
        {
            HttpClient client = new HttpClient() { BaseAddress = baseAddress };
            return client.GetAsync(requestUri).Result;
        }
    }
}