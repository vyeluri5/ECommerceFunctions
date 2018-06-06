using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommFunctions.Common
{
    public class AzureConnect
    {
        /// <summary>
        /// Validate the connection string information in app.config and throws an exception if it looks like 
        /// the user hasn't updated this to valid values. 
        /// </summary>
        /// <param name="storageConnectionString">Connection string for the storage service or the emulator</param>
        /// <returns>CloudStorageAccount object</returns>
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw ex;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                throw ex;
            }

            return storageAccount;
        }

        /// <summary>
        /// Create a table for the sample application to process messages in. 
        /// </summary>
        /// <returns>A CloudTable object</returns>
        public static async Task<CloudTable> CreateTableAsync(string tableName, string connectionString)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(connectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            try
            {
                if (await table.CreateIfNotExistsAsync())
                {
                    Console.WriteLine("Created Table named: {0}", tableName);
                }
                else
                {
                    Console.WriteLine("Table {0} already exists", tableName);
                }
            }
            catch (StorageException)
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            Console.WriteLine();
            return table;
        }
    }
}
