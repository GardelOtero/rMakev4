
using System.ComponentModel.DataAnnotations;
using System.Data;
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
            
            Projects.Add(createdProject);

            foreach(var item in project.Documents)
            {
                Document newdoc=new Document();
                newdoc.GUID = Guid.NewGuid().ToString();
                newdoc.Name = item.Name;
                newdoc.Order = item.Order;
                newdoc.ProjectId = createdProject.GUID;
                newdoc.Project = createdProject;
                newdoc.ParentDocumentId = item.GUID;
                newdoc.CreationDate = DateTime.Now;
                newdoc.Content = item.Content;
                createdProject.Documents.Add(newdoc);
                
                /*foreach (var element in item.Elements)
                {
                    Element newelement = new Element();
                    newelement.GUID = Guid.NewGuid().ToString();
                    newelement.Content = element.Content;
                    newelement.DocumentId = newdoc.GUID;
                    newelement.Document = newdoc;
                    newelement.Order = element.Order;
                    newelement.ParentElementId = item.GUID;
                    newdoc.Elements.Add(newelement);
                }*/

            }
            //Quita el Primer Document sin Texto
            createdProject.Documents.RemoveAt(0);
            return createdProject;
        }
        public void RemoveProject(Project project)
        {
           Projects.Remove(project);            
        }
    }
}