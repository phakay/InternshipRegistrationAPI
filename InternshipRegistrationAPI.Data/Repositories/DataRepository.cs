using InternshipRegistrationAPI.Core.Contracts;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Data.Contracts;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace InternshipRegistrationAPI.Data.Repositories;


public class DataRepository<T> : IDataRepository<T> where T : class, IDistributableEntity
{
    private Container _container;

    public DataRepository(IApplicationDbContext dbContext, string containerId, string partitionKeyPath)
    {
        _container = dbContext.Database.CreateContainerIfNotExistsAsync(containerId, partitionKeyPath).Result;
    }

    public async Task<T> AddAsync(T entity)
    {
        try
        {
            return  await GetAsync(entity.Id, entity.PartitionKey);
        }
        catch (ItemNotFoundException)
        {
            var response = await _container.CreateItemAsync<T>(entity, new PartitionKey(entity.PartitionKey));
            return  response.Resource;
        }
    }

    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            var response = await _container.ReplaceItemAsync<T>(entity, entity.Id, new PartitionKey(entity.PartitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new ItemNotFoundException($"Then entity: {entity.Id} could not be found");
        }
    }
     
    public async Task<T> GetAsync(string id, string partitionKey)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new ItemNotFoundException($"Then entity: {id} could not be found");
        }
  
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
        return response.StatusCode == HttpStatusCode.NoContent;
    }
    

    public Task<bool> RemoveAsync(string id)
    {
        throw new NotImplementedException();
    }

}
