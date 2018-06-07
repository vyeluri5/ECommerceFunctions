using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using EcommerceFunctions.Dependencies;
using EcommFunctions.Common;
using EcommFunctions.Functions.Interfaces;
using EcommFunctions.Functions.Types;
using EcommFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace ProductFunctions
{
    public static class ProductFunctions
    {
        public static IEcommFunctionFactory factoryInstance = new ECommFunctionFactory();

        [FunctionName("GetProduct")]
        public static  async Task<HttpResponseMessage> GetProduct([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/product/{category}/{id}")]HttpRequestMessage req,  string category, string id, TraceWriter log)
        {
            string guid = new Guid().ToString();

            CloudTable table = await AzureConnect.CreateTableAsync("ECommCollection", Environment.GetEnvironmentVariable("AzureTable"));

            var res = await factoryInstance.Create<IProductFunction>(log, table)
                    .GetProductAsync(category, id)
                    .ConfigureAwait(false);
                

            return req.CreateResponse(HttpStatusCode.OK, res, JsonMediaTypeFormatter.DefaultMediaType);
        }

        [FunctionName("CreateProduct")]
        public static async Task<HttpResponseMessage> CreateProduct([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "api/product")]HttpRequestMessage req, TraceWriter log)
        {
            string guid = new Guid().ToString();

            CloudTable table = await AzureConnect.CreateTableAsync("ECommCollection", Environment.GetEnvironmentVariable("AzureTable"));

            var res = await factoryInstance.Create<IProductFunction>(log, table)
                    .CreateProductAsync(req, Environment.GetEnvironmentVariable("AzureTable"))
                    .ConfigureAwait(false);

            return req.CreateResponse(HttpStatusCode.OK, res, JsonMediaTypeFormatter.DefaultMediaType);
        }

    }
}
