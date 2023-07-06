using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using rMakev2.Models;
using rMakev2.Services;
using rMakev2.DTOs;
using System.ComponentModel;
using Blazored.LocalStorage;
using rMakev2.Pages;

namespace rMakev2.ViewModel
{
    public class ProjectViewModel
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
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged()
        {
            


            _localStorageService.SetItemAsync("portfolio", Portfolio);
        }

        public void NewProject()
        {
            Portfolio.AddProject();
            OnPropertyChanged();

        }

        public async Task InitializeProjects(Models.App app)
        {
            portfolio = app.Portfolio;

            //if(token != null)
            //    await LoadProyectAsync(token);
            
            OnPropertyChanged();

        }

        public void DeleteProject(Project project)
        {
            Portfolio.RemoveProject(project);
            OnPropertyChanged();

        }

        public void UpdateProject(Project projext)
        {
            var oldProj = Portfolio.Projects.Where(x => x.GUID == projext.GUID).FirstOrDefault();

            var index = Portfolio.Projects.IndexOf(oldProj);

            if (index == -1)
                return;
                
            Portfolio.Projects[index] = projext;


            OnPropertyChanged();

        }

        public void LoadDocuments(Project project)
        {
            _navigationManager.NavigateTo("/app/" + project.GUID);
        }

        public async Task LoadProyectAsync(string token)
        {
            Portfolio = new Portfolio(Portfolio.App, "codename-rebel-creator");
            
            SaveProjectDto savedContent = await _communicationService.LoadAsync(token);

            if (savedContent == null)
            {
                _navigationManager.NavigateTo("/Error");
                return;
            }

            //app = new Models.App(savedContent.Id, savedContent.PortfolioToken);

            Portfolio.GUID = savedContent.Id;


            foreach (var proj in savedContent.Projects)
            {
                Project p = new Project();
                p.Name = proj.Name;
                p.GUID = proj.Id;
                p.CreationDate = proj.CreationDate;
                p.Portfolio = Portfolio;
                p.PortfolioId = Portfolio.GUID;
                p.ParentProjectId = proj.ParentProjectId;
                this.Portfolio.Projects.Add(p);


                foreach (var doc in proj.Documents)
                {
                    var Pro = Portfolio.Projects.Where(x => x.GUID == proj.Id).FirstOrDefault();
                    Document d = new Document();
                    d.Name = doc.Name;
                    d.GUID = doc.Id;
                    d.CreationDate = doc.CreationDate;
                    d.Order = doc.Order;
                    d.Content = doc.Content;
                    d.Project = Pro;
                    d.ProjectId = Pro.GUID;
                    d.ParentDocumentId = doc.ParentDocumentId;
                    Pro.Documents.Add(d);

                    //foreach (var ele in doc.Elements)
                    //{
                    //    var Proj = app.Portfolio.Projects.Where(x => x.Id == proj.Id).FirstOrDefault();
                    //    var docum = Proj.Documents.Where(x => x.Id == doc.Id).FirstOrDefault();
                    //    Element e = new Element();
                    //    e.Id = ele.Id;
                    //    e.Content = ele.Content;
                    //    e.Order = ele.Order;
                    //    e.Ideary = ele.Ideary;
                    //    e.DocumentId = ele.DocumentId;
                    //    e.Document = docum;
                    //    e.ParentElementId = ele.ParentElementId;
                    //    e.Hash = ele.Hash;
                    //    docum.Elements.Add(e);

                    //}

                }


            }
           // app.Ui.SaveModal = app.Ui.SaveModal;


            //App.Portfolio.RemoveProject(app.Ui.SelectedProject);
            //app.Ui.SelectedProject = app.Portfolio.Projects.Where(x => x.Id == savedContent.Ui.IdSelectedProject).FirstOrDefault();
            //app.Ui.SelectedDocument = app.Ui.SelectedProject.Documents.Where(x => x.Id == savedContent.Ui.IdSelectedDocument).FirstOrDefault();



        }
            


        
    }
}
