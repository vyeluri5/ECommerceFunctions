using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EcommFunctions.Common;
using EcommFunctions.Functions.Interfaces;
using EcommFunctions.Models;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace EcommFunctions.Functions.Types
{
    public class ProductFunction : IProductFunction
    {
        protected CloudTable table;
        public TraceWriter Log { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new instance of <see cref="ProductFunction"/>
        /// </summary>
        /// <param name="_cloudTable"><see cref="CloudTable"/> instance</param>
        public ProductFunction(CloudTable _cloudTable)
        {
            this.table = _cloudTable;
        }

        /// <inheritdoc/>
        public async Task<object> GetProductAsync<TInput, TType>(TInput input, TType options = default(TType))
        {
            var category = input as string;
            var id = options as string;

            ProductEntity productEntity = (ProductEntity)await AzureUtils.RetrieveEntityUsingPointQueryAsync<ProductEntity>(table, category, id);

            return productEntity;
        }

        /// <inheritdoc/>
        public async Task<object> CreateProductAsync<TInput, TType>(TInput input, TType options = default(TType))
        {
            var req = input as HttpRequestMessage;

            dynamic body = await req.Content.ReadAsStringAsync();
            ProductEntity productEntity = JsonConvert.DeserializeObject<ProductEntity>(body as string);

            productEntity.PartitionKey = productEntity.Category.ToEnum<ProductCategory>().ToString();
            productEntity.RowKey = Guid.NewGuid().ToString();

            productEntity = (ProductEntity)await AzureUtils.InsertOrMergeEntityAsync<ProductEntity>(table, productEntity);

            return productEntity;
        }
    }
}
