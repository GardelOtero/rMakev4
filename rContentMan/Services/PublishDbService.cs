﻿using Microsoft.Azure.Cosmos;
using rContentMan.Models;

namespace rContentMan.Services
{
    public class PublishDbService : ICosmosDbService<PublishDocument>
    {
        private Microsoft.Azure.Cosmos.Container _container;

        public PublishDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(PublishDocument item)
        {
            await this._container.CreateItemAsync<PublishDocument>(item, new PartitionKey(item.id));
        }
        
        public async Task AddItemAsync(PublishDocument item, string id)
        {
            await this._container.CreateItemAsync<PublishDocument>(item, new PartitionKey(id));
        }


        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<PublishDocument>(id, new PartitionKey(id));
        }

        public async Task<PublishDocument> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<PublishDocument> response = await this._container.ReadItemAsync<PublishDocument>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PublishDocument>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<PublishDocument>(new QueryDefinition(queryString));
            List<PublishDocument> results = new List<PublishDocument>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateItemAsync(string id, PublishDocument item)
        {
            await this._container.UpsertItemAsync<PublishDocument>(item, new PartitionKey(id));

        }
    }
}