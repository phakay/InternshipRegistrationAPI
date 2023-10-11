using InternshipRegistrationAPI.Data.Contracts;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipRegistrationAPI.Data
{
    public class ApplicationDbContext : IDisposable, IApplicationDbContext
    {
        private static readonly string EndpointUri = "https://localhost:8081";
        private static readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private static readonly string DatabaseId = "InternshipRegistration";

        private readonly CosmosClient _client;

        public ApplicationDbContext()
        {
            _client = new CosmosClient(EndpointUri, PrimaryKey);
        }

        public Database Database => _client.CreateDatabaseIfNotExistsAsync(DatabaseId).Result;

        public void Dispose()
        {
            _client.Dispose();
        }

    }
}
