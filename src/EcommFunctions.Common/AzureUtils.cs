using EcommFunctions.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommFunctions.Common
{
    public class AzureUtils
    {
        /// <summary>
        /// The Table Service supports two main types of insert operations.
        ///  1. Insert - insert a new entity. If an entity already exists with the same PK + RK an exception will be thrown.
        ///  2. Replace - replace an existing entity. Replace an existing entity with a new entity.
        ///  3. Insert or Replace - insert the entity if the entity does not exist, or if the entity exists, replace the existing one.
        ///  4. Insert or Merge - insert the entity if the entity does not exist or, if the entity exists, merges the provided entity properties with the already existing ones.
        /// </summary>
        /// <param name="table">The sample table name</param>
        /// <param name="entity">The entity to insert or merge</param>
        /// <returns>A Task object</returns>
        public static async Task<ProductEntity> InsertOrMergeEntityAsync(CloudTable table, ProductEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                ProductEntity insertedProduct = result.Result as ProductEntity;

                return insertedProduct;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
    }
}
