using RestSharp;
using rMakev2.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using rMakev2.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Components;
using rMakev2.Services.Interfaces;
using System.Text.RegularExpressions;

namespace rMakev2.Services
{
    public class CommunicationService : ICommunicationService
    {

        private NavigationManager _navigationManager;

        public CommunicationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public async Task SaveAsync(Portfolio portfolio)
        {
            
            var save = new SaveProjectDto();
            save.Id = portfolio.GUID;
            save.CreationDate = portfolio.CreationDate;
            save.rIdSignature = portfolio.rIdSignature;
            save.SignatureDate = portfolio.SignatureDate;
            save.Authors = portfolio.Authors;    
           
            //save.DataToken = App.DataToken;
            save.Projects = new List<ProjectDTO>();
            // save.Ui = new UIDto();

            //save.Ui.IdSelectedDocument = App.Ui.SelectedDocument.Id;
            //save.Ui.IdSelectedProject = App.Ui.SelectedProject.Id;

            foreach (var item in portfolio.Projects)
            {
                ProjectDTO project = new ProjectDTO();
                project.Id = item.GUID;
                project.Name = item.Name;
                project.CreationDate = item.CreationDate;
                project.Authors = item.Authors;
                project.PathPrewiewImage = item.PathPreviewImage;
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
                    document.Authors = itemDoc.Authors;
                    document.ProjectId = itemDoc.ProjectId;
                    document.Name = itemDoc.Name;
                    document.Content = itemDoc.Content;
                    document.Elements = new List<ElementDTO>();
                    document.ParentDocumentId = itemDoc.ParentDocumentId;
                    save.Projects.Where(x => x.Id == itemDoc.ProjectId).First().Documents.Add(document);

                    foreach (var itemElement in itemDoc.Elements)
                    {
                        ElementDTO element = new ElementDTO();
                        element.Id = itemElement.GUID;
                        element.Content = itemElement.Content;
                        element.Order = itemElement.Order;
                        element.Ideary = itemElement.Ideary;
                        element.Authors = itemDoc.Authors;
                        element.BlockContent = new List<BlockElementDTO>();
                        element.DocumentId = itemElement.DocumentId;
                        element.ParentElementId= itemElement.ParentElementId;
                        var pro = save.Projects.Where(x => x.Id == itemDoc.ProjectId).First();
                        pro.Documents.Where(x => x.Id == itemDoc.GUID).First().Elements.Add(element);

                        foreach (var itemblockElement in itemElement.BlockContent)
                        {
                            BlockElementDTO blockElement = new BlockElementDTO();
                            blockElement.id = itemblockElement.id;
                            blockElement.Authors = itemDoc.Authors;
                            blockElement.type = itemblockElement.type;
                            blockElement.elementId = itemblockElement.elementId;
                            blockElement.data = new DataDTO();
                            blockElement.data.text = itemblockElement.data.text;
                            blockElement.data.level = itemblockElement.data.level;
                            blockElement.data.caption = itemblockElement.data.caption;
                            blockElement.data.alignment = itemblockElement.data.alignment;
                            blockElement.data.url = itemblockElement.data.url;
                            blockElement.data.withBorder = itemblockElement.data.withBorder;
                            blockElement.data.withBackground = itemblockElement.data.withBackground;
                            blockElement.data.stretched = itemblockElement.data.stretched;
                            blockElement.data.style = itemblockElement.data.style;
                            blockElement.data.link = itemblockElement.data.link;
                            blockElement.data.code = itemblockElement.data.code;
                            blockElement.data.withHeadings = itemblockElement.data.withHeadings;
                            blockElement.data.content = itemblockElement.data.content;
                            blockElement.data.items = new List<ItemDTO>();
                            var pro1 = save.Projects.Where(x => x.Id == itemDoc.ProjectId).First();
                            var pro2 = pro1.Documents.Where(x => x.Id == itemElement.DocumentId).First();
                            pro2.Elements.Where(x => x.Id == itemElement.GUID).First().BlockContent.Add(blockElement);

                            if (itemblockElement.data.items != null)
                            {
                                foreach (var itemofItem in itemblockElement.data.items)
                                {
                                    ItemDTO itemBElement = new ItemDTO();
                                    itemBElement.content = itemofItem.content;
                                    itemBElement.items = itemofItem.items;
                                    itemBElement.text = itemofItem.text;
                                    itemBElement.@checked = itemofItem.@checked;
                                    //var pro12 = save.Projects.Where(x => x.Id == itemDoc.ProjectId).First();
                                    //var pro23 = pro12.Documents.Where(x => x.Id == itemElement.DocumentId).First();
                                    //var pro34 = pro23.Elements.Where(x => x.Id == itemblockElement.elementId).First();
                                    //pro34.BlockContent.Where(x => x.id == itemblockElement.id).First().data.items.Add(itemBElement);
                                    blockElement.data.items.Add(itemBElement);


                                }
                            }
                        }

                    }
                        

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


        public async Task<Portfolio> LoadAsync(string token, Portfolio portfolio)
        {
            HttpClient hc = new HttpClient();

            Regex guidRegEx = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

            string url = "https://rcontentman.azurewebsites.net/api/item/" + token;
            //string url = "https://localhost:7267/api/item/" + token;

            var response = await hc.GetAsync(url);

            var Portfolio = new Portfolio(portfolio.App, "codename-rebel-creator");

            var resultContent = response.Content.ReadAsStringAsync().Result;
            var loadedSaveProject = JsonConvert.DeserializeObject<SaveProjectDto>(resultContent);

            if (!response.IsSuccessStatusCode || loadedSaveProject == null || !guidRegEx.IsMatch(token))
            {
                _navigationManager.NavigateTo("/Error");
                return Portfolio;
            }

            var hola = loadedSaveProject;


            //app = new Models.App(savedContent.Id, savedContent.PortfolioToken);

            portfolio.GUID = loadedSaveProject.Id;
            portfolio.CreationDate = loadedSaveProject.CreationDate;
            portfolio.rIdSignature = loadedSaveProject.rIdSignature;
            portfolio.SignatureDate = loadedSaveProject.SignatureDate; 


            foreach (var proj in loadedSaveProject.Projects)
            {
                Project p = new Project();
                p.Name = proj.Name;
                p.GUID = proj.Id;
                p.CreationDate = proj.CreationDate;
                p.Authors = proj.Authors;
                p.PathPreviewImage = proj.PathPrewiewImage;
                p.Portfolio = portfolio;
                p.Documents = new List<Document>();
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
                    d.Authors = doc.Authors;
                    d.Content = doc.Content;
                    d.Project = Pro;
                    d.Elements = new List<Element>();
                    d.ProjectId = Pro.GUID;
                    d.ParentDocumentId = doc.ParentDocumentId;
                    Pro.Documents.Add(d);

                    foreach (var ele in doc.Elements)
                    {
                        var Proj = Portfolio.Projects.Where(x => x.GUID == proj.Id).FirstOrDefault();
                        var docum = Proj.Documents.Where(x => x.GUID == doc.Id).FirstOrDefault();
                        Element e = new Element();
                        e.GUID = ele.Id;
                        e.Content = ele.Content;
                        e.Order = ele.Order;
                        e.Ideary = ele.Ideary;
                        e.Authors = d.Authors;
                        e.DocumentId = ele.DocumentId;
                        e.Document = docum;
                        e.ParentElementId = ele.ParentElementId;
                        e.Hash = ele.Hash;
                        docum.Elements.Add(e);

                        foreach (var blo in ele.BlockContent)
                        {
                            var Proje = Portfolio.Projects.Where(x => x.GUID == proj.Id).FirstOrDefault();
                            var docume = Proje.Documents.Where(x => x.GUID == doc.Id).FirstOrDefault();
                            var elem = docume.Elements.Where(x => x.GUID == ele.Id).FirstOrDefault();
                            BlockElement b = new BlockElement();
                            b.id = blo.id;
                            b.Authors = blo.Authors;
                            b.type = blo.type;
                            Data dat = new Data();
                            b.data = dat;
                            dat.text = blo.data.text;
                            dat.level = blo.data.level;
                            dat.caption = blo.data.caption;
                            dat.alignment = blo.data.alignment;
                            dat.url = blo.data.url;
                            dat.withBorder = blo.data.withBorder;
                            dat.withBackground = blo.data.withBackground;
                            dat.stretched = blo.data.stretched;
                            dat.style = blo.data.style;
                            dat.link = blo.data.link;
                            dat.code = blo.data.code;
                            dat.items = new List<Item>();
                            elem.BlockContent.Add(b);

                            if (blo.data.items != null)
                            {
                                foreach (var item in blo.data.items)
                                {
                                    Item i = new Item();
                                    i.content = item.content;
                                    i.text = item.text;
                                    i.items = item.items;
                                    i.@checked = item.@checked;
                                    b.data.items.Add(i);
                                }
                            }


                        }

                    }


                }
                
                
                p.SelectedDocument = p.Documents.First();


            }


            Portfolio.GUID = portfolio.GUID;

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
            publish.Authors = project.Authors.ToList();
            publish.Sign = project.Authors.FirstOrDefault();
            publish.Documents = new List<PublishDocument>();

            foreach (var doc in project.Documents)
            {
                PublishDocument document = new PublishDocument();
                document.Id = doc.GUID;
                document.ProjectId = doc.ProjectId;
                document.Name = doc.Name;
                document.Order = doc.Order;
                document.Content = doc.Content;
                document.ContentType = "rebel";
                publish.Documents.Add(document);

            }
            var client = new RestClient("https://localhost:7247/");
            //var client = new RestClient("https://rcontentman.azurewebsites.net/");
            var request = new RestRequest("api/item", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreReadOnlyProperties = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,

            };
            var objstr = System.Text.Json.JsonSerializer.Serialize(publish, options);
            request.AddJsonBody(objstr);
            var sendReq = await client.ExecuteAsync(request);

        }
    }

}

