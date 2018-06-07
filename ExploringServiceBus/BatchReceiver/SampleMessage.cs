using Microsoft.WindowsAzure.Storage.Table;

namespace BatchReceiver
{
    public class SampleMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CustomerEntity : TableEntity
    {
        public string Name { get; set; }

        public CustomerEntity(int id, string country)
        {
            PartitionKey = country;
            RowKey = id.ToString();
        }

        public CustomerEntity()
        {

        }
    }
}