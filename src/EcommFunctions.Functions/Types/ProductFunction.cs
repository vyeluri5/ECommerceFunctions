using System;
using System.Net.Http;
using System.Threading.Tasks;
using EcommFunctions.Common;
using EcommFunctions.Models;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace EcommFunctions.Functions.Types
{
    public class ProductFunction : IProductFunction
    {
        private readonly CloudTable _table;
        public TraceWriter Log { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new instance of <see cref="ProductFunction"/>
        /// </summary>
        /// <param name="cloudTable"><see cref="CloudTable"/> instance</param>
        public ProductFunction(CloudTable cloudTable)
        {
            _table = cloudTable ?? throw new ArgumentNullException(nameof(cloudTable));
        }

        /// <inheritdoc/>
        public async Task<object> GetProductAsync<TInput, TType>(TInput input, TType options = default(TType))
        {
            var category = input as string;
            var id = options as string;

            var productEntity = await AzureUtils.RetrieveEntityUsingPointQueryAsync<ProductEntity>(_table, category, id);

            return productEntity;
        }

        /// <inheritdoc/>
        public async Task<object> CreateProductAsync<TInput, TType>(TInput input, TType options = default(TType))
        {
            if (input is HttpRequestMessage req)
            {
                dynamic body = await req.Content.ReadAsStringAsync();
                var productEntity = JsonConvert.DeserializeObject<ProductEntity>(body as string);

                productEntity.PartitionKey = productEntity.Category.ToEnum<ProductCategory>().ToString();
                productEntity.RowKey = Guid.NewGuid().ToString();

                productEntity = (ProductEntity)await AzureUtils.InsertOrMergeEntityAsync(_table, productEntity);

                return productEntity;
            }
            else
                return null;
        }
    }
}
