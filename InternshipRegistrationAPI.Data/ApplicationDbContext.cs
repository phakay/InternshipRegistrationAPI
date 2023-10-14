using InternshipRegistrationAPI.Data.Contracts;
using Microsoft.Azure.Cosmos;
using System.Configuration;

namespace InternshipRegistrationAPI.Data
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
        private static readonly string DatabaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private readonly CosmosClient _client;


        public ApplicationDbContext()
        {
            _client = new CosmosClient(EndpointUri, PrimaryKey);
            Database = _client.CreateDatabaseIfNotExistsAsync(DatabaseId).Result;
        }

        public Database Database { get; }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
