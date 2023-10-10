using InternshipRegistrationAPI.Core.Contracts;
using InternshipRegistrationAPI.Data.Contracts;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace InternshipRegistrationAPI.Data.Repositories;


public class DataRepository<T> : IDataRepository<T> where T : class, ICosmosDbDocument
{
    private Container _container;
  
    public DataRepository(string containerId, string partitionKeyPath)
    {
        _container = new ApplicationDbContext().Database.CreateContainerIfNotExistsAsync(containerId, partitionKeyPath).Result;
    }

    public async Task<T> AddAsync(T entity)
    {
        try
        {
            return  await GetAsync(entity.Id, entity.PartitionKey);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            var response = await _container.CreateItemAsync<T>(entity, new PartitionKey(entity.PartitionKey));
            return  response.Resource;
        }
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var response = await _container.ReplaceItemAsync<T>(entity, entity.Id, new PartitionKey(entity.PartitionKey));
        return response.Resource;
    }

    public async Task<T> GetAsync(string Id, string partitionKey)
    {
        var response = await _container.ReadItemAsync<T>(Id, new PartitionKey(partitionKey));
        return response.Resource;
    }

    public async Task<T> GetAsync(string id)
    {
        return await GetAsync(id, id);
    }

    public async Task<IEnumerable<T>> GetAsync()
    {
        string sqlQueryText = "SELECT * FROM c";

        var queryDefinittion = new QueryDefinition(sqlQueryText);
        FeedIterator<T> queryResultIterator = _container.GetItemQueryIterator<T>(queryDefinittion);

        List<T> results = new List<T>();

        while (queryResultIterator.HasMoreResults)
        {
            var currentResult = await queryResultIterator.ReadNextAsync();
            foreach (var result in currentResult)
            {
                results.Add(result);
            }
        }

        return results;
    }


    public async Task<bool> RemoveAsync(string id, string partitionKey)
    {
        ItemResponse<T> response = await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
        return true;
    }
    

    public async Task<bool> RemoveAsync(string id)
    {
        throw new NotImplementedException();
    }

}
