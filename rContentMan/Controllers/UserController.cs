using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rContentMan.Models;
using rContentMan.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rContentMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AuthDbService _cosmosDbService;
        public UserController(AuthDbService cosmosDbService)
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

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] LoginModel item)
        {


            if (item != null)
            {


                var validate = await _cosmosDbService.GetItemAsync(item.userId);
                if (validate != null)
                {
                    if(item.userId == validate.userId && item.Password == validate.Password)
                    {
                        return Ok();
                    }

                    return BadRequest();
                    

                }
                else
                {
                    return BadRequest();

                }


            }

            return BadRequest();

        }

        [HttpPost]
        public async Task PostAsync([FromBody] LoginModel item)
        {


            if (item != null)
            {


                var validate = await _cosmosDbService.GetItemAsync(item.userId);
                if (validate != null)
                {

                    await _cosmosDbService.UpdateItemAsync(item.userId, item);

                }
                else
                {


                    //item.id = Guid.NewGuid().ToString();
                    await _cosmosDbService.AddItemAsync(item);
                }


            }

        }
    }
}
