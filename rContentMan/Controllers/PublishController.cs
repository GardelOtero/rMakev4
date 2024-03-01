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

        [HttpGet("{PortfolioId}")]
        public async Task<string> GetAllByPortfolioAsync(string PortfolioId)
        {


            var x = await _cosmosDbService.GetItemsByPartitionAsync("SELECT * FROM c", PortfolioId);
            var z = JsonConvert.SerializeObject(x);
            return (z);


        }

        [HttpGet("{id}/{PortfolioId}")]

        public async Task<string> GetAsync(string id, string PortfolioId)
        {
            var x = await _cosmosDbService.GetItemFromPartitionAsync(id, PortfolioId);
            var z = JsonConvert.SerializeObject(x);
            return z;
        }

        [HttpPost]
        public async Task PostAsync([FromBody] PublishDocument item)
        {


             if (item != null)
            {


                var validate = await _cosmosDbService.GetItemAsync(item.PortfolioId);
                if (validate != null)
                {

                    await _cosmosDbService.UpdateItemAsync(item.PortfolioId, item);

                }
                else
                {


                    //item.id = Guid.NewGuid().ToString();
                    await _cosmosDbService.AddItemAsync(item);
                }


            }

        }

        [HttpPut]
        public async Task PutAsync(PublishDocument item)
        {
            await _cosmosDbService.UpdateItemAsync(item.PortfolioId, item);
        }

        
        [HttpDelete("{id}/{PortfolioId}")]
        public async Task DeleteAsync(string id, string PortfolioId)
        {
            await _cosmosDbService.DeleteItemFromPartitionAsync(id, PortfolioId);
        }
    }
}
  