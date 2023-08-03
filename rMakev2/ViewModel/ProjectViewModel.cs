using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using rMakev2.Models;
using rMakev2.Services;
using rMakev2.DTOs;
using System.ComponentModel;
using Blazored.LocalStorage;
using rMakev2.Pages;
using rMakev2.ViewModel.Interfaces;
using rMakev2.Services.Interfaces;

namespace rMakev2.ViewModel
{
    public class ProjectViewModel: IProjectViewModel
    {
        private ToastService _toastService;
        private ICommunicationService _communicationService;
        private AIChat _aiChat;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;


        public ProjectViewModel(IToastService toast, ICommunicationService communicationService, NavigationManager navigationManager, AIChat aiChat, ILocalStorageService localStorageService)
        {
            this._toastService = toast as Blazored.Toast.Services.ToastService;
            this._communicationService = communicationService;
            this._navigationManager = navigationManager;
            this._aiChat = aiChat;
            this._localStorageService = localStorageService;

            portfolio = new Portfolio();

            _navigationManager = navigationManager;
        }

        private Portfolio portfolio;
        public Portfolio Portfolio
        {
            get { return portfolio; }
            set
            {
                portfolio = value;
            }
        }

        public async Task OnPropertyChanged()
        {
            


            await _localStorageService.SetItemAsync("portfolio", Portfolio);
        }

        public async Task NewProject()
        {
            Portfolio.AddProject();
            await OnPropertyChanged();

        }

        public async Task InitializeProjects(Models.App app)
        {
            portfolio = app.Portfolio;

            //if(token != null)
            //    await LoadProyectAsync(token);
            
            await OnPropertyChanged();

        }

        public async Task DeleteProject(Project project)
        {
            Portfolio.RemoveProject(project);
            await OnPropertyChanged();

        }

        public async Task ForkProject(Project project)
        {
            Portfolio.ForkProject(project);
            await OnPropertyChanged();

        }

        public async Task UpdateProject(Project projext)
        {
            var oldProj = Portfolio.Projects.Where(x => x.GUID == projext.GUID).FirstOrDefault();

            var index = Portfolio.Projects.IndexOf(oldProj);

            if (index == -1)
                return;
                
            Portfolio.Projects[index] = projext;


            await OnPropertyChanged();

        }

        public void LoadDocuments(Project project)
        {
            _navigationManager.NavigateTo("/app/" + project.GUID);
        }

        public async Task LoadProyectAsync(string token)
        {
            portfolio = await _communicationService.LoadAsync(token, Portfolio);
        }

        public async Task SaveContentAsync()
        {
            // HashMyContent();
            await _communicationService.SaveAsync(Portfolio);
            this._toastService.ShowSuccess("Project Saved");
        }


    }
}
