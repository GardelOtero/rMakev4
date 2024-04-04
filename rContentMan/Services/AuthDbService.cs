using Microsoft.Azure.Cosmos;
using rContentMan.Models;

namespace rContentMan.Services
{
    public class AuthDbService : ICosmosDbService<LoginModel>
    {
        private Microsoft.Azure.Cosmos.Container _container;

        public AuthDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(LoginModel item)
        {
            await this._container.CreateItemAsync<LoginModel>(item, new PartitionKey(item.userId));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<LoginModel>(id, new PartitionKey(id));
        }

        public async Task DeleteItemFromPartitionAsync(string id, string partitionId)
        {
            await this._container.DeleteItemAsync<LoginModel>(id, new PartitionKey(partitionId));
        }

        public async Task<LoginModel> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<LoginModel> response = await this._container.ReadItemAsync<LoginModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<LoginModel> GetItemFromPartitionAsync(string id, string partitionId)
        {
            try
            {
                ItemResponse<LoginModel> response = await this._container.ReadItemAsync<LoginModel>(id, new PartitionKey(partitionId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<LoginModel>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<LoginModel>(new QueryDefinition(queryString));
            List<LoginModel> results = new List<LoginModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<LoginModel>> GetItemsByPartitionAsync(string queryString, string id)
        {
            var query = this._container.GetItemQueryIterator<LoginModel>(new QueryDefinition(queryString),
                requestOptions: new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey(id)
                });
            List<LoginModel> results = new List<LoginModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateItemAsync(string id, LoginModel item)
        {
            await this._container.UpsertItemAsync<LoginModel>(item, new PartitionKey(id));

        }
    }
}
