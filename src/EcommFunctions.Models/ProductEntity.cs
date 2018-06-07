using Microsoft.WindowsAzure.Storage.Table;

namespace EcommFunctions.Models
{
    public class ProductEntity : TableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
