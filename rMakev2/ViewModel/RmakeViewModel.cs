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
using static MudBlazor.CategoryTypes;
using rMakev2.Pages;

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

        private Project project;
        private List<BlockElement> elements;

        public Project Project
        {
            get { return project; }
            set
            {
                project = value;
                OnPropertyChanged();
            }
        }

        public void InitializePortfolio()
        {
            App = new Models.App("rebel");

            Project = new Models.Project();
            //App.Portfolio.Projects.Add(new Project(App.Portfolio));
            //Project.SelectedDocument = Project.Documents.FirstOrDefault(x => x.Id == Project.Id);
            //Ui.SelectedProject = ProjectZero;
            //Project.SelectedDocument = ProjectZero.Documents.First();

            Thread p1;
            p1 = new Thread(new ThreadStart(Save));
            p1.Start();

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


        public void HideSaveModal()
        {
            App.SaveModal.Hide();
        }
        public void ShowSaveModal()
        {
            App.SaveModal.Show();
        }

        //public void ShowJsonModal()
        //{
        //    App.SwitchDisplayJson();
        //}

        public void DocumentMenu()
        {
            Project.DocumentMenu();
        }

        public void HidePublishModal()
        {
            App.PublishModal.Hide();
        }
        public void ShowPublishModal()
        {
            App.PublishModal.Show();
        }

        //public void HideLoadModal()
        //{
        //    App.LoadModal.Hide();
        //}
        //public void ShowLoadModal()
        //{
        //    App.LoadModal.Show();
        //}

        public void SelectDocument(Document document)
        {
            Project.SelectDocuments(document);

        }


        // public async Task AiGenerate(Element element)
        //{
        //    var content = element.Content;
        //    element.AIContent= await _aiChat.UseChatService("Improve and expand this text: " + content);
            
        //}

        public void CloneDocument()
        {
           this._toastService.ShowSuccess("Document cloned");
           SelectDocument(Project.CloneDocument(Project.SelectedDocument));
        }
        public void NewDocument()
        {
            project.AddDocument(project);
            this._toastService.ShowSuccess("New document created");
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

            if (Project.Documents.Count() > 1)
            {
                Project.RemoveDocument(Project.SelectedDocument);
                SelectDocument(Project.Documents.First());
            }
            else if (Project.Documents.Count() == 1)
            {
                Project.RemoveDocument(Project.SelectedDocument);
                NewDocument();
            }

         }

        public void DeleteDocumentMenu(Document document)
        {
            Project project = document.Project;


            if (project.Documents.Count() > 1)
            {

                project.RemoveDocument(document);

                if (document == Project.SelectedDocument)
                {
                    SelectDocument(project.Documents.FirstOrDefault());
                }

            }
            else if (project.Documents.Count() == 1)
            {
                project.RemoveDocument(document);
                NewDocumentMenu(project);
            }

        }


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
            
            project.ShowMenu();
        }
        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                SwitchProjectName();
            }

        }

        public void BlocktoElement(string elementsJs)
        {
            
            Root block = JsonSerializer.Deserialize<Root>(elementsJs);

            elements = new List<BlockElement>();
            elements.AddRange(block.blocks);

            List<Models.Element> elementsC = new List<Models.Element>();
            //HashSet<string> encounteredIds = new HashSet<string>();

            foreach (Document document in project.Documents)
            {
                foreach (Models.Element element in document.Elements)
                {
                    foreach (BlockElement blockelement in element.BlockContent) {
                        var oldElement = element.BlockContent.Where(x => x.id == blockelement.id).FirstOrDefault();

                        var index = element.BlockContent.IndexOf(oldElement);

                        if (index == -1)
                            element.BlockContent.Add(blockelement);
                        else
                        element.BlockContent[index] = blockelement;
                    }
                    
                   
                }
            }
        
       }

        public void ElementstoCSharp()
        {
            List<Models.Element> elementsC = new List<Models.Element>();
            HashSet<string> encounteredIds = new HashSet<string>();

            foreach (Document document in project.Documents)
            {
                foreach (Models.Element element in document.Elements)
                {
                    element.BlockContent.AddRange(elements);
                }
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
        public void BlockRTAFocus()
        {


            Project.BlockRTAFocus = true;


        }
        public void UnBlockRTAFocus()
        {

            Project.BlockRTAFocus = false;

        }
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

            App.Portfolio.GUID = savedContent.Id;


            foreach (var proj in savedContent.Projects)
            {
                Project p = new Project();
                p.Name = proj.Name;
                p.GUID = proj.Id;
                p.CreationDate = proj.CreationDate;
                p.Portfolio = app.Portfolio;
                p.PortfolioId = app.Portfolio.GUID;
                p.ParentProjectId = proj.ParentProjectId;
                this.App.Portfolio.Projects.Add(p);


                foreach (var doc in proj.Documents)
                {
                    var Pro = app.Portfolio.Projects.Where(x => x.GUID == proj.Id).FirstOrDefault();
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public void SuccessNotification(string message)
        {
            this._toastService.ShowSuccess(message);
        }

    }
}
