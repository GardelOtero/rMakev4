using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using Blazorise;

namespace rMakev2.Models
{
   /* public class Ui : INotifyPropertyChanged
    {
        public Ui(Models.App app)
        {
            Id = Guid.NewGuid().ToString();
            App = app ?? throw new Exceptions("app is null");
            App = app;
            AppId = app.Id;
            app.Ui = this;
            JsonFn();
        }
        public string Id { get; set; }
        private Project selectedProject;
        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                OnPropertyChanged();
            }
        }
        private Document selectedDocument;
        public Document SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                OnPropertyChanged();
            }
        }

        /*private Element selectedElement;
        public Element SelectedElement
        {
            get { return selectedElement; }
            set
            {
                selectedElement = value;
                OnPropertyChanged();
            }
        }


        [JsonIgnore]
        public string Json { get; set; }
        public string AppId { get; set; }

        public bool EditProjectName { get; set; }
        public bool EditedProjectName { get; set; } = false;
        public bool BlockRTAFocus { get; set; } = true;

        public bool DisplayMenu { get; set; } = true;
        public bool DisplayComents { get; set; } = false;
        public bool DisplayDocumentMenu { get; set; }
        public bool DisplayJson { get; set; } = false;


        public Modal? SaveModal { get; set; }
        public Modal? PublishModal { get; set; }
        public Modal? MergeModal { get; set; }

        public Modal? LoadModal { get; set; }
        public App App { get; set; }



        public void SelectProject(Project project)
        {
            SelectedProject = project;
        }
        public void SelectDocument(Document document)
        {
            SelectedDocument = document;
        }
        public void SwitchEditName()
        {

            if (EditProjectName == true)
            {
                EditProjectName = false;
                BlockRTAFocus = false;
            }
            else
            {
                EditProjectName = true;
                BlockRTAFocus = true;
            }
        }
        public void SwitchDisplayJson()
        {

            if (DisplayJson == true)
            {
                DisplayJson = false;
                
            }
            else
            {
                DisplayJson = true;
                
            }
        }

        /*public void EditItem(Element element)
        {

            foreach (var item in SelectedDocument.Elements)
            {
                item.EditItem = false;
            }


            if (element.EditItem == true)
            {
                element.EditItem = false;

            }
            else
            {
                element.EditItem = true;

            }
            
        }

        public void ShowMenu()
        {

            if (DisplayMenu == true)
            {
                DisplayMenu = false;
            }
            else
            {
                DisplayMenu = true;
            }
        }

        public void DocumentMenu()
        {

            if (DisplayDocumentMenu == true)
            {
                DisplayDocumentMenu = false;
            }
            else
            {
                DisplayDocumentMenu = true;
            }
        }

        public string JsonFn()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            Json = JsonSerializer.Serialize(App.Data, options);
            return Json;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            JsonFn();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }*/

}