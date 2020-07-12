using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Helpers
{
    public static class Extension
    {
        // the message passed will be set into headers
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            // here we set the message in headers
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Header");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
