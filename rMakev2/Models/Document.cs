using Microsoft.AspNetCore.Components;
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

            
        }

        public void RemoveAuthor(string name)
        {
            Authors.Remove(name);
        }

        public void ChangeOrder(Element element, int order)
        {
            Element current = Elements.Where(w => w.GUID == element.GUID).SingleOrDefault();
            current.Order = order;
            current.Content = order.ToString();
        }

        public void ResetEdit()
        {
            foreach(var element in Elements)
            {
                element.EditItem = false;
            }
        }

        public List<string> separateElements()
        {
            var list = new List<string>();
            var pattern = "(<p>|</p>)";
            var isInTag = false;
            var inTagValue = String.Empty;

            foreach (var subStr in Regex.Split(Content, pattern))
            {
                if (subStr.Equals("<p>"))
                {
                    isInTag = true;
                    continue;
                }
                else if (subStr.Equals("</p>"))
                {
                    isInTag = false;
                    list.Add(String.Format("<p>{0}</p>", inTagValue));
                    continue;
                }

                if (isInTag)
                {
                    inTagValue = subStr;
                    continue;
                }

                if (!String.IsNullOrEmpty(subStr))
                    list.Add(subStr);

            }
            return list;
        }

        public void stringToElements()
        {
            List<string> elementList = separateElements();

            Elements = new List<Element>();

            string newDocTitle = "";
            string newDocContent = "";
            bool newDoc = false;

            foreach (var elementContent in elementList)
            {
                if(elementContent != "<p><br></p>")
                {
                    if(newDoc == true && elementContent.Contains("<span class=\"ql-size-huge\">"))
                    {
                        Document document = Project.AddDocument(Project);
                        document.Name = newDocTitle;
                        document.Content = newDocContent;

                        newDocContent = "";
                        newDocTitle = Regex.Replace(elementContent, "<.*?>", "");



                    }
                    else if(elementContent.Contains("<span class=\"ql-size-huge\">"))
                    {
                        newDocTitle = Regex.Replace(elementContent, "<.*?>", "");
                        newDoc = true;

                    }
                    else if(newDoc == true)
                    {
                        newDocContent += elementContent;
                    }
                    else
                    {
                        Element element = new Element(this);
                        element.Content = elementContent;

                    }

                }
            }

            if(newDocTitle != "")
            {
                Document document = Project.AddDocument(Project);
                document.Name = newDocTitle;
                document.Content = newDocContent;
            }
        }

        public void elementsToString()
        {
            string ContentString = "";

            foreach (var element in Elements.OrderBy(w => w.Order))
            {
                ContentString += element.Content;
            }

            Content = ContentString;

        }
    }
}
