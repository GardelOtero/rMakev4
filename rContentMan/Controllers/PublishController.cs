using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rContentMan.Models;
using rContentMan.Services;

namespace rContentMan.Controllers
{

    [Route("api/[controller]")]
    public class PublishController : ControllerBase
    {
        private readonly PublishDbService _cosmosDbService;
        public PublishController(PublishDbService cosmosDbService)
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
        public async Task PostAsync([FromBody] PublishDocument item)
        {


             if (item != null)
            {


                var validate = await _cosmosDbService.GetItemAsync(item.id);
                if (validate != null)
                {

                    await _cosmosDbService.UpdateItemAsync(item.id, item);

                }
                else
                {


                    //item.id = Guid.NewGuid().ToString();
                    await _cosmosDbService.AddItemAsync(item);
                }


            }

        }

        [HttpPut("{id}")]
        public async Task PutAsync(string id, PublishDocument item)
        {
            await _cosmosDbService.UpdateItemAsync(id, item);
        }
        
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
        }
    }
}
  