using Jwt.Refresh.Token.Domain.Entities.Repositories;
using Newtonsoft.Json;

namespace rContentMan.Services
{
    public class TokenRepository: IUserRepository
    {
        private readonly AuthDbService _repo;
        public TokenRepository(AuthDbService authDbService) 
        {
            _repo = authDbService;
        }

        public async Task<string> GetUserIdByIdAndPasswordAsync(string id, string password, CancellationToken cancellationToken = default)
        {
            var user = await _repo.GetItemAsync(id);

            var response = JsonConvert.SerializeObject(user);

             if(id == "jorge")
            {
                return await Task.FromResult<string>("user_test@gmail.com");
            }


            return response;
        }

    }
}
