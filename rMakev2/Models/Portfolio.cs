
using Blazorise;
using rMakev2.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
namespace rMakev2.Models
{
    public class Portfolio
    {

        public Portfolio() 
        {
            Id = 0;
            GUID = Guid.NewGuid().ToString();
            //App = app ?? throw new Exceptions("app is null");
            Projects = new List<Project>();
            //App = app;
            //AppId = app.Id;
            CreationDate = DateTime.Now;
            rIdSignature = "";
            SignatureDate = DateTime.Now;
            Authors = new List<string>();
            //if (dataToken == null)
            //    dataToken = Guid.NewGuid().ToString();
            //else
            //    DataToken = dataToken;
            //app.Portfolio = this;
        }
        public Portfolio(App app, string dataToken) {
            Id = 0;
            GUID = Guid.NewGuid().ToString();
            App = app ?? throw new Exceptions("app is null");            
            Projects = new List<Project>();
            App = app;
            AppId = app.Id;
            CreationDate = DateTime.Now;
            rIdSignature = "";
            SignatureDate = DateTime.Now;
            Authors = new List<string>();
            if (dataToken == null)
                dataToken = Guid.NewGuid().ToString();
            else
                DataToken = dataToken;
            app.Portfolio = this;   
        }
        public int Id { get; set; }
        public string GUID { get; set; }
        public List<Project> Projects { get; set; }
        [JsonIgnore]
        public App App { get; set; }
        public string AppId { get; set; }
        public DateTime CreationDate { get; set; }
        public string rIdSignature { get; set; }
        public DateTime SignatureDate { get; set; }
        public List<string> Authors { get; set; }
        public string DataToken { get; set; }
        public Project AddProject()
        {
            Project newProject = new Project(this);
            newProject.Name = "Project Name";

            Projects.Add(newProject);
            return newProject;
        }

        public Project ForkProject(Project project)
        {
            Project createdProject= new Project(this);
            createdProject.ParentProjectId = project.GUID;
            createdProject.Name = project.Name + "(Forked)";
            createdProject.Authors = project.Authors;
            createdProject.CreationDate = DateTime.Now;
            
            Projects.Add(createdProject);

            foreach(var item in project.Documents)
            {
                Document newdoc = new Document();
                newdoc.GUID = Guid.NewGuid().ToString();
                newdoc.Name = item.Name;
                newdoc.Order = item.Order;
                newdoc.ProjectId = createdProject.GUID;
                newdoc.Project = createdProject;
                newdoc.ParentDocumentId = item.GUID;
                newdoc.CreationDate = DateTime.Now;
                newdoc.Content = item.Content;
                newdoc.Elements = new List<Element>();
                newdoc.Authors = item.Authors;
                createdProject.Documents.Add(newdoc);

                foreach (var element in item.Elements)
                {
                    Element newelement = new Element();
                    newelement.GUID = Guid.NewGuid().ToString();
                    newelement.Content = "";
                    newelement.Author = "";
                    newelement.Order = newdoc.Elements.Count() + 1;
                    newelement.EditItem = true;
                    newelement.DocumentId = newdoc.GUID;
                    newelement.Document = newdoc;
                    newelement.CreationDate = DateTime.Now;
                    newelement.ParentGuid = "";
                    newelement.OrderParentId = 0;
                    newelement.Authors = new HashSet<string>();
                    newelement.BlockContent = new List<BlockElement>();
                    newdoc.Elements.Add(newelement);

                    foreach(var block in element.BlockContent)
                    {
                        BlockElement newblock = new BlockElement();

                        newblock.Authors = block.Authors;
                        newblock.id = block.id;
                        newblock.type = block.type;
                        newblock.elementId = block.elementId;
                        //newblock.data = block.data;


    }

                }

            }
            //Quita el Primer Document sin Texto
            createdProject.Documents.RemoveAt(0);
            createdProject.SelectedDocument = createdProject.Documents.First();
            return createdProject;
        }
        public void RemoveProject(Project project)
        {
           Projects.Remove(project);            
        }

        public LocalPortfolioDTO ToLocalStorage()
        {
            LocalPortfolioDTO localPorfolio = new LocalPortfolioDTO();

            localPorfolio.Id = Id;
            localPorfolio.GUID = GUID;
            localPorfolio.DataToken = DataToken;
            localPorfolio.SignatureDate = SignatureDate;
            localPorfolio.CreationDate = CreationDate;
            localPorfolio.rIdSignature = rIdSignature;
            localPorfolio.Authors = Authors;
            localPorfolio.AppId = AppId;
            localPorfolio.projectsGUID = new List<string>();

            foreach (var item in Projects)
            {
                localPorfolio.projectsGUID.Add(item.GUID);
            }


            return localPorfolio;
        }

        public void FromLocalDTO(LocalPortfolioDTO localPorfolio)
        {
            Id = localPorfolio.Id;
            GUID = localPorfolio.GUID;
            DataToken = localPorfolio.DataToken;
            SignatureDate = localPorfolio.SignatureDate;
            CreationDate = localPorfolio.CreationDate;
            rIdSignature = localPorfolio.rIdSignature;
            Authors = localPorfolio.Authors;
            AppId = localPorfolio.AppId;

            Projects = new List<Project>();
        }
    }
}