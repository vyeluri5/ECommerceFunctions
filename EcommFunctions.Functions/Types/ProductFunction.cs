using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommFunctions.Common;
using EcommFunctions.Functions.Interfaces;
using EcommFunctions.Models;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;

namespace EcommFunctions.Functions.Types
{
    public class ProductFunction : IProductFunction
    {
        public TraceWriter Log { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<object> InvokeAsync<TInput, TType>(TInput input, TType options = default(TType))
        {

            CloudTable table = await AzureConnect.CreateTableAsync("sample-collection", options as string);

            string guid = new Guid().ToString();
            ProductEntity productEntity = new ProductEntity()
            {
                PartitionKey = "MensShirts",
                RowKey = "1",
                Name = "Victor Pasa T-Shirt",
                Description = "T-shirt with blue strips with short seleves",
                Timestamp = DateTime.Now
            };

            productEntity = await AzureUtils.InsertOrMergeEntityAsync(table, productEntity);

            return productEntity.PartitionKey;
        }

    }
}
