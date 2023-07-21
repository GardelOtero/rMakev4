using RestSharp;
using rMakev2.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using rMakev2.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Components;

namespace rMakev2.Services
{
    public class CommunicationService : ICommunicationService
    {

        private NavigationManager _navigationManager;

        public CommunicationService(NavigationManager navigationManager) 
        {
            _navigationManager = navigationManager;
        }

        public async Task SaveAsync(Models.App App)
        {
            var save = new SaveProjectDto();
            save.Id = App.Portfolio.GUID;
            //save.DataToken = App.DataToken;
            save.Projects = new List<ProjectDTO>();
           // save.Ui = new UIDto();

            //save.Ui.IdSelectedDocument = App.Ui.SelectedDocument.Id;
            //save.Ui.IdSelectedProject = App.Ui.SelectedProject.Id;

            foreach (var item in App.Portfolio.Projects)
            {
                ProjectDTO project = new ProjectDTO();
                project.Id = item.GUID;
                project.Name = item.Name;
                project.CreationDate = item.CreationDate;
                project.Documents = new List<DocumentDTO>();
                project.ParentProjectId = item.ParentProjectId;
                save.Projects.Add(project);
                
                foreach (var itemDoc in item.Documents)
                {
                    DocumentDTO document = new DocumentDTO();
                    document.Id = itemDoc.GUID;
                    document.Name = itemDoc.Name;
                    document.CreationDate = itemDoc.CreationDate;
                    document.Order = itemDoc.Order;
                    document.ProjectId = itemDoc.ProjectId;
                    document.Name = itemDoc.Name;
                    document.Content = itemDoc.Content;
                    //document.Elements = new List<ElementDTO>();
                    document.ParentDocumentId = itemDoc.ParentDocumentId;   
                    save.Projects.Where(x => x.Id == itemDoc.ProjectId).First().Documents.Add(document);

                    /*foreach (var itemElement in itemDoc.Elements)
                    {
                        ElementDTO element = new ElementDTO();
                        element.Id = itemElement.Id;
                        element.Content = itemElement.Content;
                        element.Order = itemElement.Order;
                        element.Ideary = itemElement.Ideary;
                        element.DocumentId = itemElement.DocumentId;
                        element.ParentElementId= itemElement.ParentElementId;
                        var pro = save.Projects.Where(x => x.Id == itemDoc.ProjectId).First();
                        pro.Documents.Where(x => x.Id == itemDoc.Id).First().Elements.Add(element);
                    }*/

                }


            }

            var client = new RestClient("https://localhost:7267/");
            //var client = new RestClient("https://rcontentman.azurewebsites.net/");
            var request = new RestRequest("api/item", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreReadOnlyProperties = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,

            };
            var objstr = System.Text.Json.JsonSerializer.Serialize(save, options);
            request.AddJsonBody(objstr);
            var sendReq = await client.ExecuteAsync(request);

        }


        public async Task<Portfolio> LoadAsync(string token, Portfolio portfolio)
        {
            HttpClient hc = new HttpClient();

            //string url = "https://rcontentman.azurewebsites.net/api/item/" + token;
            string url = "https://localhost:7267/api/item/" + token;
            var response = await hc.GetAsync(url);

            var Portfolio = new Portfolio(portfolio.App, "codename-rebel-creator");
            if (!response.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("/Error");
                return Portfolio;
            }

            var resultContent = response.Content.ReadAsStringAsync().Result;
            var loadedSaveProject = JsonConvert.DeserializeObject<SaveProjectDto>(resultContent);

            var hola = loadedSaveProject;


            //app = new Models.App(savedContent.Id, savedContent.PortfolioToken);

            portfolio.GUID = loadedSaveProject.Id;


            foreach (var proj in loadedSaveProject.Projects)
            {
                Project p = new Project();
                p.Name = proj.Name;
                p.GUID = proj.Id;
                p.CreationDate = proj.CreationDate;
                p.Portfolio = portfolio;
                p.PortfolioId = portfolio.GUID;
                p.ParentProjectId = proj.ParentProjectId;
                Portfolio.Projects.Add(p);


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


            return Portfolio;
            
            //Logica
            //recibe el json
            //carga entiedades directas
            //crea selected projecy
            //crea selected document
        }

        public async Task PublishAsync(Project project)
        {
            var publish = new PublishProject();
            publish.Id = project.GUID;
            publish.PublicationDate = DateTime.Now;
            publish.Authors = project.Author;
        }
       
    }
}
