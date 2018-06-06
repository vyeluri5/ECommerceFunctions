using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EcommerceFunctions.Dependencies;
using EcommFunctions.Functions.Interfaces;
using EcommFunctions.Functions.Types;
using EcommFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ProductFunctions
{
    public static class ProductFunctions
    {
        public static IEcommFunctionFactory factoryInstance = new ECommFunctionFactory();

        [FunctionName("GetProduct")]
        public static  async Task<HttpResponseMessage> GetProduct([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/product/{id}")]HttpRequestMessage req,  string id, TraceWriter log)
        {
            string guid = new Guid().ToString();

            var res = await factoryInstance.Create<IProductFunction>(log)
                    .InvokeAsync(req, Environment.GetEnvironmentVariable("AzureTable"))
                    .ConfigureAwait(false);
                



            
            //log.Info("C# HTTP trigger function processed a request.");

            //// parse query parameter
            //string name = req.GetQueryNameValuePairs()
            //    .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
            //    .Value;

            //if (name == null)
            //{
            //    // Get request body
            //    dynamic data = await req.Content.ReadAsAsync<object>();
            //    name = data?.name;
            //}

            return req.CreateResponse(HttpStatusCode.OK, "Hello " + res);

            //return name == null
            //    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
            //    : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        }
    }
}
