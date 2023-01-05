﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rContentMan.Models;
using rContentMan.Services;

namespace rContentMan.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        public ItemController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<string> GetAsync()
        {


            var x = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
            var z = JsonConvert.SerializeObject(x);
            return (z);


        }

        [HttpGet("{id}")]

        public async Task<string> GetAsync(string id)
        {
            var x = await _cosmosDbService.GetItemAsync(id);
            var z = JsonConvert.SerializeObject(x);
            return z;
        }

        [HttpPost]
        public async Task PostAsync([FromBody] Item item)
        {

            if (item != null)
            {

                try
                {
                    item.Id = Guid.NewGuid().ToString();

                    await _cosmosDbService.AddItemAsync(item);
                    Console.Write(item);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }

        }

        [HttpPut("{id}")]
        public async Task PutAsync(string id, Item item)
        {
            await _cosmosDbService.UpdateItemAsync(id, item);
        }





    }
}
