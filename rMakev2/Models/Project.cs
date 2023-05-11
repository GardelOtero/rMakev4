﻿using System.Text.Json.Serialization;

namespace rMakev2.Models
{
    public class Project
    {
        public Project()
        {
            Documents = new List<Document>();
        }
        public Project(Data data)
        {
            Data = data ?? throw new Exceptions("Data is null");
            Id = Guid.NewGuid().ToString();
            Name = "Project Name";
            //Name = "Project ("+data.Projects.Count() +")";
            CreationDate = DateTime.Now;
            Documents = new List<Document>();
            Data = data;
            DataId = data.Id;
            AddDocument(this);


        }
        
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Document> Documents { get; set; }
        [JsonIgnore]
        public Data Data { get; set; }
        public string DataId { get; set; }
        public string ParentProjectId { get; set; }

                
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
            Documents.Where(x => x.Id == document.Id).Select(x => { x.Name = document.Name; x.Content = document.Content; return x; });
        }

        internal Document CloneDocument(Document document)
        {
            Document newDocument = new Document(document.Project);
            newDocument.Name = document.Name + "(Cloned)";
            newDocument.ParentDocumentId = document.Id;
            newDocument.Content = document.Content;
            Documents.Add(newDocument);            

            /*foreach (var item in document.Elements)
            {
                Element newelement = new Element();
                newelement.Content= item.Content;
                newelement.DocumentId = newDocument.Id;
                newelement.Document = newDocument;
                newelement.ParentElementId = item.Id;
                newelement.Id = Guid.NewGuid().ToString();
                newelement.Order = item.Order;

                newDocument.Elements.Add(newelement);
            }
            //Quita el Primer Element sin texto
            newDocument.Elements.RemoveAt(0);*/
            return newDocument;
        }
    }
}
