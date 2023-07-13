using Microsoft.AspNetCore.Components;
using rMakev2.Models;

namespace rMakev2.ViewModel.Interfaces
{
    public interface IProjectViewModel
    {
        public Task NewProject();

        public Task InitializeProjects(Models.App app);

        public Task DeleteProject(Project project);

        public Task UpdateProject(Project projext);

        public void LoadDocuments(Project project);

        public Task LoadProyectAsync(string token);
    }
}
