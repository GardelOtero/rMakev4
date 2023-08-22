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
using rMakev2.ViewModel.Interfaces;
using MudBlazor;
using System.Text;
using rMakev2.Services.Interfaces;

namespace rMakev2.ViewModel
{
    public class RmakeViewModel : INotifyPropertyChanged, IDocumentViewModel, IAppViewModel
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


            Thread p1;
            p1 = new Thread(new ThreadStart(Save));
            p1.Start();

        }


        public void Save()
        {
            while (true)
            {

                Thread.Sleep(300000);
                //HashMyContent();
                _communicationService.SaveAsync(App.Portfolio).Wait();
                this._toastService.ShowSuccess("Portfolio Auto Saved");
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

        public void HideDeleteModal()
        {
            App.DeleteModal.Hide();
        }
        public void ShowDeleteProjectModal()
        {
            App.DeleteProjectModal.Show();
        }

        public void HideDeleteProjectModal()
        {
            App.DeleteProjectModal.Hide();
        }
        public void ShowDeleteModal()
        {
            App.DeleteModal.Show();
        }
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
        public void ShowJsonModal()
        {

            Project.SwitchDisplayJson();

        }

        public void HideAuthorModal()
        {
            App.AuthorModal.Hide();
        }
        public void ShowAuthorModal()
        {
            App.AuthorModal.Show();
        }

        public void HideDocAuthorModal()
        {
            App.DocAuthorModal.Hide();
        }
        public void ShowDocAuthorModal()
        {
            App.DocAuthorModal.Show();
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



        public void CloneDocument()
        {
            this._toastService.ShowSuccess("Your document has been cloned to a new one");
            SelectDocument(Project.CloneDocument(Project.SelectedDocument));
        }
        public void NewDocument()
        {
            project.AddDocument(project);
            this._toastService.ShowSuccess("You created a new document");
        }

        public void NewDocumentMenu(Project project)
        {
            SelectDocument(project.AddDocument(project));
            this._toastService.ShowSuccess("You created a new document");
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

            this._toastService.ShowSuccess("Document Deleted");

        }

        public void DeleteDocumentMenu(Document document)
        {
            Project project = document.Project;


            if (project.Documents.Count() > 1)
            {

                project.RemoveDocument(document);
                this._toastService.ShowSuccess("Document Deleted");

                if (document == Project.SelectedDocument)
                {
                    SelectDocument(project.Documents.FirstOrDefault());
                }

            }
            else if (project.Documents.Count() == 1)
            {
                project.RemoveDocument(document);
                this._toastService.ShowSuccess("Document Deleted");
                NewDocumentMenu(project);
            }
            
        }


        public async Task PublishContentAsync()
        {
            await _communicationService.PublishAsync(Project);
            this._toastService.ShowSuccess("Your Project has been Published");
        }

        public void DisplayMenu()
        {

            project.ShowMenu();
        }

        public void BlocktoElement(string elementsJs)
        {

            Root block = JsonSerializer.Deserialize<Root>(elementsJs);

            var elements = Project.SelectedDocument.Elements.FirstOrDefault();
            elements.BlockContent = new List<BlockElement>();
            elements.BlockContent.AddRange(block.blocks);


            foreach(var author in Project.SelectedDocument.Authors)
            {
                elements.AddAuthor(author);
            }


        }

        private string generateUniqueID(int _characterLength = 10)
        {
            StringBuilder _builder = new StringBuilder();
            Enumerable
                .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(_characterLength)
                .ToList().ForEach(e => _builder.Append(e));
            return _builder.ToString();
        }

        public string elementClone(string element)
        {

            Root content = JsonSerializer.Deserialize<Root>(Project.SelectedDocument.Content);

            var block = content.blocks.Where(x => x.id == element).FirstOrDefault(); //ikeAZG3fLM

            if (block == null)
            {
                return Project.SelectedDocument.Content;
            }

            string newId = generateUniqueID();

            var blockClone = new BlockElement();

            blockClone.id = newId;
            blockClone.type = block.type;
            blockClone.data = block.data;

            content.blocks.Insert(content.blocks.IndexOf(block), blockClone);

            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreReadOnlyProperties = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            string newContent = JsonSerializer.Serialize(content, options);

            return newContent;
            
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

        //public void HashMyContent()
        //{

        //    foreach (var project in App.Portfolio.Projects)
        //    {
        //        foreach (var document in project.Documents)
        //        {
        //            foreach (var element in document.Elements)
        //            {
        //                element.Hash = HashString(element.Content, element.Id);
        //            }
        //        }
        //    }
        //}

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


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

    }
}
