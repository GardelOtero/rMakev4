using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using rMakev2.DTOs;
using Microsoft.AspNetCore.Components.Web;
using rMakev2.Models;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using Document = rMakev2.Models.Document;
using RestSharp;
using Microsoft.JSInterop;
using rMakev2.Services;
using Blazorise;

namespace rMakev2.ViewModel
{
    public class RmakeViewModel : INotifyPropertyChanged
    {
        private ToastService _toastService;
        private ICommunicationService _communicationService;
        private AIChat _aiChat;

        private NavigationManager _navigationManager;

        public RmakeViewModel(IToastService toast, ICommunicationService communicationService, NavigationManager navigationManager, AIChat aiChat)
        {
            this._toastService = toast as Blazored.Toast.Services.ToastService;
            this._communicationService = communicationService;
            this._navigationManager = navigationManager;
            this._aiChat = aiChat;

            InitializePortfolio();
            _navigationManager = navigationManager;
        }

        public RmakeViewModel()
        {

            InitializePortfolio();
        }
        private Models.App app;
        public Models.App App
        {
            get { return app; }
            set
            {
                app = value;
                OnPropertyChanged();
            }
        }
       /* private Ui ui;
        public Ui Ui
        {
            get { return ui; }
            set
            {
                ui = value;
                OnPropertyChanged();
            }
        }*/

        public List<Item> ListaOraciones = new List<Item>();
        public void InitializePortfolio()
        {
           // App = new Models.App;

            //Ui = new Models.Ui(App);
            App.Portfolio.Projects.Add(new Project(App.Portfolio));
            var ProjectZero = App.Portfolio.Projects.First();
            //Ui.SelectedProject = ProjectZero;
            //Ui.SelectedDocument = ProjectZero.Documents.First();

            Thread p1;
            p1 = new Thread(new ThreadStart(Save));
            p1.Start();

            ListaOraciones.Add(new Item("color: #F9E43D", "Connection made easy"));
            ListaOraciones.Add(new Item("color: #A2F8BC", "Web of documents"));
            ListaOraciones.Add(new Item("color: #9BFBF1", "Decentralized from the start"));
            ListaOraciones.Add(new Item("color: #F5ADFB", "Real collaboration"));
            ListaOraciones.Add(new Item("color: #F9E43D", "Built-in recognition"));
            ListaOraciones.Add(new Item("color: #FCB0B3", "Writing made easy"));

            //Creo las entidades por defecto.
        }


        public void Save()
        {
            while (true)
            {

                Thread.Sleep(300000);
                //HashMyContent();
                _communicationService.SaveAsync(App).Wait();
                this._toastService.ShowSuccess("Project Auto Saved");
            }
        }


        /*public void HideSaveModal()
        {
            App.Ui.SaveModal.Hide();
        }
        public void ShowSaveModal()
        {
            App.Ui.SaveModal.Show();
        }

        public void ShowJsonModal()
        {
            App.Ui.SwitchDisplayJson();
        }

        public void DocumentMenu()
        {
            App.Ui.DocumentMenu();
        }

        public void HidePublishModal()
        {
            App.Ui.PublishModal.Hide();
        }
        public void ShowPublishModal()
        {
            App.Ui.PublishModal.Show();
        }

        public void HideLoadModal()
        {
            App.Ui.LoadModal.Hide();
        }
        public void ShowLoadModal()
        {
            App.Ui.LoadModal.Show();
        }*/
        /*public void EventSelectProject(ChangeEventArgs e)
        {
            string projectId = e.Value.ToString();
            Project project = Ui.SelectedProject = App.Portfolio.Projects.Where(w => w.Id == projectId).SingleOrDefault();
            Ui.SelectProject(project);
            this._toastService.ShowInfo("You have changed the project to " + project.Name);
        }
        public void EventSelectDocumentMenu(Document document1)
        {
            Ui.SelectDocument(document1);
            Ui.SelectProject(document1.Project);
        }
        public void EventSelectDocument(ChangeEventArgs e)
        {
            string documentId = e.Value.ToString();
            Document document = Ui.SelectedProject.Documents.Where(w => w.Id == documentId).SingleOrDefault();
            Ui.SelectDocument(document);
        }
        public void SelectProject(Project project)
        {
            Ui.SelectProject(project);
            SelectDocument(Ui.SelectedProject.Documents.FirstOrDefault());

        }
        public void SelectDocument(Document document)
        {
            Ui.SelectDocument(document);

        }*/
       /* public void NewProject()
        {
            SelectProject(App.Portfolio.AddProject());
            //SelectDocument(Ui.SelectedProject.Documents.FirstOrDefault());



            //this._toastService.ShowSuccess("New project created");

        }

        /*public async Task AiGenerate(Element element)
        {
            var content = element.Content;
            element.AIContent= await _aiChat.UseChatService("Improve and expand this text: " + content);
            
        }
        public void DeleteProject()
        {

            if (App.Portfolio.Projects.Count() >= 1)
            {
                App.Portfolio.RemoveProject(Ui.SelectedProject);
                SelectProject(App.Portfolio.Projects.First());
                //this._toastService.ShowSuccess("Project eliminated");
            }
            else
            {
                App.Portfolio.RemoveProject(Ui.SelectedProject);
                this._toastService.ShowSuccess("Project eliminated");
                NewProject();
            }
        }
        public void DeleteProjectMenu(Project project)
        {
            if (App.Portfolio.Projects.Count() > 1)
            {
                App.Portfolio.RemoveProject(project);
                SelectProject(App.Portfolio.Projects.First());
                SelectDocument(Ui.SelectedProject.Documents.First());

            }

            else if (App.Portfolio.Projects.Count() == 1)
            {
                App.Portfolio.RemoveProject(project);
                NewProject();
                SelectDocument(Ui.SelectedProject.Documents.First());
            }
        }

        public void ForkProject()
        {
            SelectProject(App.Portfolio.ForkProject(Ui.SelectedProject));

            this._toastService.ShowSuccess("Project Forked");
        }
        public void CloneDocument()
        {
            this._toastService.ShowSuccess("Document cloned");
            SelectDocument(Ui.SelectedProject.CloneDocument(Ui.SelectedDocument));
        }
        public void NewDocument()
        {
            SelectDocument(Ui.SelectedProject.AddDocument(Ui.SelectedProject));
            //this._toastService.ShowSuccess("New document created");
        }

        public void NewDocumentMenu(Project project)
        {
            SelectDocument(project.AddDocument(project));
        }

        public void UpdateDocumentMenu(Document document)
        {
            App.Portfolio.Projects.Where(x => x.Id == document.Project.Id).Select(x => x.Documents.Where(d => d.Id == document.Id)).FirstOrDefault().Select(x => { x.Name = document.Name; return x; });
        }

        public void DeleteDocument()
        {

            if (Ui.SelectedProject.Documents.Count() > 1)
            {
                Ui.SelectedProject.RemoveDocument(Ui.SelectedDocument);
                SelectDocument(Ui.SelectedProject.Documents.First());
            }
            else if (Ui.SelectedProject.Documents.Count() == 1)
            {
                Ui.SelectedProject.RemoveDocument(Ui.SelectedDocument);
                NewDocument();
            }

        }

        public void DeleteDocumentMenu(Document document)
        {
            Project project = document.Project;


            if (project.Documents.Count() > 1)
            {

                project.RemoveDocument(document);

                if (document == Ui.SelectedDocument)
                {
                    SelectDocument(project.Documents.FirstOrDefault());
                }

            }
            else if (project.Documents.Count() == 1)
            {
                project.RemoveDocument(document);
                NewDocumentMenu(project);
            }

        }*/

        
        public async Task SaveContentAsync()
        {
            // HashMyContent();
            await _communicationService.SaveAsync(App);
            this._toastService.ShowSuccess("Project Saved");
        }
        public void SwitchProjectName()
        {
           // Ui.SwitchEditName();


        }

        public void DisplayMenu()
        {
           // Ui.ShowMenu();
        }
        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                SwitchProjectName();
            }

        }
        public void MergeDocumentsIntoNewOne(Document First, Document Second)
        {

        }
        /*public void HashMyContent()
        {

            foreach (var project in App.Portfolio.Projects)
            {
                foreach (var document in project.Documents)
                {
                    foreach (var element in document.Elements)
                    {
                        element.Hash = HashString(element.Content, element.Id);
                    }
                }
            }
        }*/
        /*public void BlockRTAFocus()
        {


            Ui.BlockRTAFocus = true;


        }
        public void UnBlockRTAFocus()
        {

            Ui.BlockRTAFocus = false;

        }*/
        public string HashString(string text, string salt)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }
            if (salt == String.Empty)
            {
                salt = "rebelsalt";
            }
            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }

        public async Task LoadProyectAsync(string token)
        {

            
            SaveProjectDto savedContent = await _communicationService.LoadAsync(token);

            if (savedContent == null)
            {
                _navigationManager.NavigateTo("/Error");
                return;
            }

            //app = new Models.App(savedContent.Id, savedContent.PortfolioToken);

            App.Portfolio.Id = savedContent.Id;


            foreach (var proj in savedContent.Projects)
            {
                Project p = new Project();
                p.Name = proj.Name;
                p.Id = proj.Id;
                p.CreationDate = proj.CreationDate;
                p.Portfolio = app.Portfolio;
                p.PortfolioId = app.Portfolio.Id;
                p.ParentProjectId = proj.ParentProjectId;
                this.App.Portfolio.Projects.Add(p);


                foreach (var doc in proj.Documents)
                {
                    var Pro = app.Portfolio.Projects.Where(x => x.Id == proj.Id).FirstOrDefault();
                    Document d = new Document();
                    d.Name = doc.Name;
                    d.Id = doc.Id;
                    d.CreationDate = doc.CreationDate;
                    d.Order = doc.Order;
                    d.Content = doc.Content;
                    d.Project = Pro;
                    d.ProjectId = Pro.Id;
                    d.ParentDocumentId = doc.ParentDocumentId;
                    Pro.Documents.Add(d);

                    foreach (var ele in doc.Elements)
                    {
                        var Proj = app.Portfolio.Projects.Where(x => x.Id == proj.Id).FirstOrDefault();
                        var docum = Proj.Documents.Where(x => x.Id == doc.Id).FirstOrDefault();
                        Element e = new Element();
                        e.Id = ele.Id;
                        e.Content = ele.Content;
                        e.Order = ele.Order;
                        e.Ideary = ele.Ideary;
                        e.DocumentId = ele.DocumentId;
                        e.Document = docum;
                        e.ParentElementId = ele.ParentElementId;
                        e.Hash = ele.Hash;
                        docum.Elements.Add(e);

                    }

                }


            }
           // app.Ui.SaveModal = app.Ui.SaveModal;


            //App.Portfolio.RemoveProject(app.Ui.SelectedProject);
            //app.Ui.SelectedProject = app.Portfolio.Projects.Where(x => x.Id == savedContent.Ui.IdSelectedProject).FirstOrDefault();
            //app.Ui.SelectedDocument = app.Ui.SelectedProject.Documents.Where(x => x.Id == savedContent.Ui.IdSelectedDocument).FirstOrDefault();



        }

        /*public void ShowAreaComment()
        {
            App.Ui.DisplayComents = App.Ui.DisplayComents == true ? false : true;
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public class Item
        {
            public string Color = "";
            public string Question = "";
            public Item(string color, string question)
            {
                Color = color;
                Question = question;
            }
        }

        public void SuccessNotification(string message)
        {
            this._toastService.ShowSuccess(message);
        }

    }
}
