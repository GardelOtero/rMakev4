using Microsoft.AspNetCore.Components;
using rMakev2.Models;

namespace rMakev2.ViewModel.Interfaces
{
    public interface IProjectViewModel
    {
        public Task NewProject();

        public Task InitializeProjects(Models.App app);
        public Task SaveContentAsync();

        public Task DeleteProject(Project project);

        public Task UpdateProject(Project projext);
        public Task ForkProject(Project project);

        public void LoadDocuments(Project project);

        public Task LoadProyectAsync(string token);
    }
}
