﻿
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rMakev2.Models
{
    public class Project
    {
        public Project()
        {
            GUID = Guid.NewGuid().ToString();
            Name = "";
            Labels = new List<string>();
            Versions = new List<string>();
            IsPublic = false;
            IsWebsite = false;
            //Name = "Project ("+data.Projects.Count() +")";
            CreationDate = DateTime.Now;
            Authors = new HashSet<string>();
            PathPreviewImage = "";
            Documents = new List<Document>();
            AddDocument(this);
            SelectedDocument = Documents.FirstOrDefault();

        }
        public Project(Portfolio portfolio)
        {
            Portfolio = portfolio ?? throw new Exceptions("Data is null");
            GUID = Guid.NewGuid().ToString();
            Name = "";
            Labels = new List<string>();
            Versions = new List<string>();
            IsPublic = false;
            IsWebsite = false;
            //Name = "Project ("+data.Projects.Count() +")";
            CreationDate = DateTime.Now;
            Authors = new HashSet<string>();
            PathPreviewImage = "";
            Documents = new List<Document>();
            Portfolio = portfolio;
            PortfolioId = portfolio.GUID;
            AddDocument(this);
            SelectedDocument = Documents.FirstOrDefault(); 
            


        }
        public int Id { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public List<string> Labels { get; set; }
        public List<string> Versions { get; set; }
        public bool IsPublic { get; set; }
        public bool IsWebsite { get; set; }
        public DateTime CreationDate { get; set; }
        public HashSet<string> Authors { get; set; }
        public string PathPreviewImage { get; set; }
        public List<Document> Documents { get; set; }
        [JsonIgnore]
        public Portfolio Portfolio { get; set; }
        public string PortfolioId { get; set; }
        public string ParentProjectId { get; set; }

        public bool DisplayMenu { get; set; } = true;

        public bool DisplayDocumentMenu { get; set; }
        [JsonIgnore]
        public string Json { get; set; }

        public bool DisplayJson { get; set; } = false;


        public bool BlockRTAFocus { get; set; } = true;

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

        public void AddAuthor(string name)
        {
            if (name == "" || name == null)
                return;

            Authors.Add(name);

            foreach(var doc in Documents)
            {
                if (!doc.Authors.Contains(name)) 
                    doc.Authors.Add(name);
            }
        }

        public void RemoveAuthor(string name) 
        { 
            Authors.Remove(name);

            foreach (var doc in Documents)
            {
                if (doc.Authors.Contains(name))
                    doc.Authors.Remove(name);
            }
        }


        public Document AddDocument(Project project)
        {
            {
                Document newDocument = new Document(project);
                Documents.Add(newDocument);
                return newDocument;
            }
        }



        public Document RemoveDocument(Document document)
        {
            Documents.Remove(document);
            return document;
        }

        public void UpdateDocument(Document document)
        {
            Documents.Where(x => x.GUID == document.GUID).Select(x => { x.Name = document.Name; x.Content = document.Content; return x; });
        }
        public void SelectDocuments(Document document)
        {
            SelectedDocument = document;
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
        public string JsonFn()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            Json = JsonSerializer.Serialize(Portfolio.Projects, options);
            return Json;
        }

        internal Document CloneDocument(Document document)
        {
            Document newDocument = new Document(document.Project);
            newDocument.Name = document.Name + "(Cloned)";
            newDocument.ParentDocumentId = document.GUID;
            newDocument.Content = document.Content;
            Documents.Add(newDocument);            

            foreach (var item in document.Elements)
            {
                Element newelement = new Element();
                newelement.Content= item.Content;
                newelement.DocumentId = newDocument.GUID;
                newelement.Document = newDocument;
                newelement.ParentElementId = item.GUID;
                newelement.GUID = Guid.NewGuid().ToString();
                newelement.Order = item.Order;

                newDocument.Elements.Add(newelement);
            }
            //Quita el Primer Element sin texto
            newDocument.Elements.RemoveAt(0);
            return newDocument;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
