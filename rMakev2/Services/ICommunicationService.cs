using rMakev2.Models;

namespace rMakev2.Services
{
    public interface ICommunicationService
    {
        public Task SaveAsync(Models.App app);
        public Task<Portfolio> LoadAsync(string token, Portfolio portfolio);
        //Task  SaveContentAsync();   
        public Task PublishAsync(Project project);
    }
}
