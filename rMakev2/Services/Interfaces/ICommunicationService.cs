using rMakev2.Models;

namespace rMakev2.Services.Interfaces
{
    public interface ICommunicationService
    {
        public Task SaveAsync(Portfolio portfolio);
        public Task<Portfolio> LoadAsync(string token, Portfolio portfolio);
        //Task  SaveContentAsync();   
        public Task PublishAsync(Project project);
    }
}
