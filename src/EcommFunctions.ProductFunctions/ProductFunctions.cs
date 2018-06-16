using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using EcommFunctions.Common;
using EcommFunctions.Dependencies;
using EcommFunctions.Dependencies.Interfaces;
using EcommFunctions.Functions.Interfaces;
using EcommFunctions.Functions.Types;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace EcommFunctions.ProductFunctions
{
    public class ProductFunctions
    {
        public static IEcommFunctionFactory FactoryInstance = new ECommFunctionFactory();

        public int Test()
        {
            return 1;
        }

        [FunctionName("GetProduct")]
        public static async Task<HttpResponseMessage> GetProduct([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/product/{category}/{id}")]HttpRequestMessage req, string category, string id, TraceWriter log)
        {

            var table = await AzureConnect.CreateTableAsync("ECommCollection", Environment.GetEnvironmentVariable("AzureTable"));

            var res = await FactoryInstance.Create<IProductFunction>(log, table)
                    .GetProductAsync(category, id)
                    .ConfigureAwait(false);


            return req.CreateResponse(HttpStatusCode.OK, res, JsonMediaTypeFormatter.DefaultMediaType);
        }

        [FunctionName("CreateProduct")]
        public static async Task<HttpResponseMessage> CreateProduct([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "api/product")]HttpRequestMessage req, TraceWriter log)
        {
            var table = await AzureConnect.CreateTableAsync("ECommCollection", Environment.GetEnvironmentVariable("AzureTable"));

            var res = await FactoryInstance.Create<IProductFunction>(log, table)
                    .CreateProductAsync(req, Environment.GetEnvironmentVariable("AzureTable"))
                    .ConfigureAwait(false);

            return req.CreateResponse(HttpStatusCode.OK, res, JsonMediaTypeFormatter.DefaultMediaType);
        }

    }
}
