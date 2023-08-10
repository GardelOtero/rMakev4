using Microsoft.AspNetCore.Components;
using rMakev2.DTOs;
using System;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace rMakev2.Models
{
    public class Document
    {
        public Document()
        {
           Elements = new List<Element>();
           Authors = new HashSet<string>();
        }
        public Document(Project project)
        {
            Project = project ?? throw new Exceptions("Project is null");
            GUID = Guid.NewGuid().ToString();
            Name = "";
            CreationDate = DateTime.Now;
            Authors = new HashSet<string>(project.Authors);
            PathPreviewImage = "";
            IsPublic = false;
            Order = Project.Documents.Count() + 1;
            Elements = new List<Element>();
            Content = "";
            Project = project;
            ProjectId = project.GUID;
            AddElement(this);

        }

        public Document(Project project, LocalDocumentDTO localDocument)
        {
            Project = project ?? throw new Exceptions("Project is null");

            Id = localDocument.Id;
            GUID = localDocument.GUID;
            Name = localDocument.Name;
            IsPublic = localDocument.IsPublic;
            CreationDate = localDocument.CreationDate;
            Authors = localDocument.Authors;
            PathPreviewImage = localDocument.PathPreviewImage;
            Order = localDocument.Order;
            Content = localDocument.Content;
            ParentDocumentId = localDocument.ParentDocumentId;
            ProjectId = localDocument.ProjectId;
            IsOrdered = localDocument.IsOrdered;

            Elements = new List<Element>();
        }
        public int Id { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public HashSet<string> Authors { get; set; }
        public string PathPreviewImage { get; set; }
        public bool IsPublic { get; set; }
        public int Order { get; set; }
        public List<Element> Elements { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
        public string Content { get; set; } //documento tendra contenido? si ya tiene elementos
        public string ProjectId { get; set; }
        public string ParentDocumentId { get; set; }
        public bool IsOrdered { get; set; } = false;



        
        public Element AddElement(Document document)
        {
            Element newElement = new Element(document);            
            return newElement;
        }
        public Element AddElement(Document document, int currentElement)
        {
            Element newElement = new Element(document, currentElement);
            return newElement;
        }
        public Element RemoveElement(Element element)
        {
            Elements.Remove(element);
            return element;
        }

        public void AddAuthor(string name)
        {
            if (name == "" || name == null) return;
            
            Authors.Add(name);

            foreach(var element in Elements)
            {
                element.AddAuthor(name);
            }
            
        }

        public void RemoveAuthor(string name)
        {
            Authors.Remove(name);
        }

        public LocalDocumentDTO ToLocalStorage()
        {
            LocalDocumentDTO localDocument = new LocalDocumentDTO();

            localDocument.Id = Id;
            localDocument.GUID = GUID;
            localDocument.Name = Name;
            localDocument.IsPublic = IsPublic;
            localDocument.CreationDate = CreationDate;
            localDocument.Authors = Authors;
            localDocument.PathPreviewImage = PathPreviewImage;
            localDocument.Order = Order;
            localDocument.Content = Content;
            localDocument.ParentDocumentId = ParentDocumentId;
            localDocument.ProjectId = ProjectId;
            localDocument.IsOrdered = IsOrdered;

            localDocument.ElementsGUID = new List<string>();

            foreach (var item in Elements)
            {
                localDocument.ElementsGUID.Add(item.GUID);
            }


            return localDocument;
        }

        public void FromLocalDTO(LocalDocumentDTO localDocument)
        {
            Id = localDocument.Id;
            GUID = localDocument.GUID;
            Name = localDocument.Name;
            IsPublic = localDocument.IsPublic;
            CreationDate = localDocument.CreationDate;
            Authors = localDocument.Authors;
            PathPreviewImage = localDocument.PathPreviewImage;
            Order = localDocument.Order;
            Content = localDocument.Content;
            ParentDocumentId = localDocument.ParentDocumentId;
            ProjectId = localDocument.ProjectId;
            IsOrdered = localDocument.IsOrdered;
        }

    }
}
