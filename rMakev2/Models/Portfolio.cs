
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;
namespace rMakev2.Models
{
    public class Portfolio
    {
        public Portfolio(App app) {

            Id = Guid.NewGuid().ToString();
            App = app ?? throw new Exceptions("app is null");            
            Projects = new List<Project>();
            App = app;
            AppId = app.Id;
            app.Portfolio = this;   
        }
        
        public string Id { get; set; }
        public List<Project> Projects { get; set; }
        [JsonIgnore]
        public App App { get; set; }
        public string AppId { get; set; }

        public Project AddProject()
        {
            Project newProject = new Project(this);
            Projects.Add(newProject);
            return newProject;
        }

        public Project ForkProject(Project project)
        {
            Project createdProject= new Project(this);
            createdProject.ParentProjectId = project.Id;
            createdProject.Name = project.Name + "(Forked)";
            
            Projects.Add(createdProject);

            foreach(var item in project.Documents)
            {
                Document newdoc=new Document();
                newdoc.Id = Guid.NewGuid().ToString();
                newdoc.Name = item.Name;
                newdoc.Order = item.Order;
                newdoc.ProjectId = createdProject.Id;
                newdoc.Project = createdProject;
                newdoc.ParentDocumentId = item.Id;
                newdoc.CreationDate = DateTime.Now;
                newdoc.Content = item.Content;
                createdProject.Documents.Add(newdoc);
                
                /*foreach (var element in item.Elements)
                {
                    Element newelement = new Element();
                    newelement.Id = Guid.NewGuid().ToString();
                    newelement.Content = element.Content;
                    newelement.DocumentId = newdoc.Id;
                    newelement.Document = newdoc;
                    newelement.Order = element.Order;
                    newelement.ParentElementId = item.Id;
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