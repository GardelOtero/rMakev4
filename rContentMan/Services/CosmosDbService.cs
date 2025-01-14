﻿using Microsoft.Azure.Cosmos;
using rContentMan.Models;
using System.Collections.Concurrent;

namespace rContentMan.Services
{

    public class CosmosDbService : ICosmosDbService<Item>
    {
        private Microsoft.Azure.Cosmos.Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Item item)
        {
            await this._container.CreateItemAsync<Item>(item, new PartitionKey(item.id));
        }


        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Item>(id, new PartitionKey(id));
        }

        public async Task<Item> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Item> response = await this._container.ReadItemAsync<Item>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                ItemResponse<OldItem> response = await this._container.ReadItemAsync<OldItem>(id, new PartitionKey(id));

                var newitem = new Item(response.Resource);

                return newitem;
            }
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
            List<Item> results = new List<Item>();
            while (query.HasMoreResults)
            {


                var response = await query.ReadNextAsync();


                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateItemAsync(string id, Item item)
        {
            await this._container.UpsertItemAsync<Item>(item, new PartitionKey(id));

        }
    }
}
