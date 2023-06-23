using RestSharp;
using rMakev2.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using rMakev2.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace rMakev2.Services
{
    public class CommunicationService : ICommunicationService
    {
        public async Task SaveAsync(Models.App App)
        {
            var save = new SaveProjectDto();
            save.Id = App.Portfolio.Id;
            save.DataToken = App.DataToken;
            save.Projects = new List<ProjectDTO>();
           // save.Ui = new UIDto();

            //save.Ui.IdSelectedDocument = App.Ui.SelectedDocument.Id;
            //save.Ui.IdSelectedProject = App.Ui.SelectedProject.Id;

            foreach (var item in App.Portfolio.Projects)
            {
                ProjectDTO project = new ProjectDTO();
                project.Id = item.Id;
                project.Name = item.Name;
                project.CreationDate = item.CreationDate;
                project.Documents = new List<DocumentDTO>();
                project.ParentProjectId = item.ParentProjectId;
                save.Projects.Add(project);
                
                foreach (var itemDoc in item.Documents)
                {
                    DocumentDTO document = new DocumentDTO();
                    document.Id = itemDoc.Id;
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

            //var client = new RestClient("https://localhost:7267/");
            var client = new RestClient("https://rcontentman.azurewebsites.net/");
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


        public async Task<SaveProjectDto> LoadAsync(string token)
        {
            HttpClient hc = new HttpClient();
            //token = "db1f334e-431e-47bd-a826-887005652c86";
            
            //string url = "https://localhost:7267/api/item/" + token;
            string url = "https://rcontentman.azurewebsites.net/api/item/" + token;
            var response = await hc.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var resultContent = response.Content.ReadAsStringAsync().Result;
                var loadedSaveProject = JsonConvert.DeserializeObject<SaveProjectDto>(resultContent);

                var hola = loadedSaveProject;
                return loadedSaveProject;
                
            }


            return null;
            
            //Logica
            //recibe el json
            //carga entiedades directas
            //crea selected projecy
            //crea selected document
        }

       
    }
}
