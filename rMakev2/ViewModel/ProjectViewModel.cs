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
            await _localStorageService.SetItemAsync("PortfolioLocal", Portfolio.ToLocalStorage());
            try
            {
                foreach(var proj in Portfolio.Projects.ToList())
                {
                    await _localStorageService.SetItemAsync("Project-" + proj.GUID, proj.ToLocalStorage()); 

                    foreach(var doc in proj.Documents.ToList())
                    {
                        await _localStorageService.SetItemAsync("Document-" + doc.GUID, doc.ToLocalStorage());

                        foreach(var ele in doc.Elements.ToList())
                        {
                            await _localStorageService.SetItemAsync("Element-" + ele.GUID, ele.ToLocalStorage());
                        }
                    }
                }          

            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task NewProject()
        {
            await SaveLocalProject(Portfolio.AddProject());

            await _localStorageService.SetItemAsync("PortfolioLocal", Portfolio.ToLocalStorage());
            //await OnPropertyChanged();

        }

        public async Task InitializeProjects(Models.App app)
        {
            portfolio = app.Portfolio;

            //if(token != null)
            //    await LoadProyectAsync(token);
            
            //await OnPropertyChanged();

        }

        public async Task SaveLocalProject(Project project)
        {
            //await _localStorageService.SetItemAsync("PortfolioLocal", Portfolio.ToLocalStorage());

            await _localStorageService.SetItemAsync("Project-" + project.GUID, project.ToLocalStorage());

            foreach (var doc in project.Documents.ToList())
            {
                await _localStorageService.SetItemAsync("Document-" + doc.GUID, doc.ToLocalStorage());

                foreach (var ele in doc.Elements.ToList())
                {
                    await _localStorageService.SetItemAsync("Element-" + ele.GUID, ele.ToLocalStorage());
                }
            }
        }

        public async Task DeleteProject(Project project)
        {

            foreach (var doc in project.Documents.ToList())
            {
                await _localStorageService.RemoveItemAsync("Document-" + doc.GUID);

                foreach (var ele in doc.Elements.ToList())
                {
                    await _localStorageService.RemoveItemAsync("Element-" + ele.GUID);
                }
            }


            await _localStorageService.RemoveItemAsync("Project-" + project.GUID);

            Portfolio.RemoveProject(project);

            await _localStorageService.SetItemAsync("PortfolioLocal", Portfolio.ToLocalStorage());


        }

        public async Task ForkProject(Project project)
        {
            await SaveLocalProject(Portfolio.ForkProject(project));

            await _localStorageService.SetItemAsync("PortfolioLocal", Portfolio.ToLocalStorage());

        }

        public async Task UpdateProject(Project projext)
        {
            var oldProj = Portfolio.Projects.Where(x => x.GUID == projext.GUID).FirstOrDefault();

            var index = Portfolio.Projects.IndexOf(oldProj);

            if (index == -1)
                return;
                
            Portfolio.Projects[index] = projext;


            await SaveLocalProject(projext);
        }

        public void LoadDocuments(Project project)
        {
            _navigationManager.NavigateTo("/app/" + project.GUID);
        }

        public async Task LoadProyectAsync(string token)
        {
            portfolio = await _communicationService.LoadAsync(token, Portfolio);
            await OnPropertyChanged();
        }

        public async Task SaveContentAsync()
        {
            // HashMyContent();
            await _communicationService.SaveAsync(Portfolio);
            this._toastService.ShowSuccess("Project Saved");
        }


    }
}
