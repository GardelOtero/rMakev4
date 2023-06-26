using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Serialization;

namespace rMakev2.Models
{
    public class Element
    {
        public Element()
        {

        }
        public Element(Document document)
        {
            Document = document ?? throw new Exceptions("Document is null");
            Id = Guid.NewGuid().ToString();
            Content = "";
            Author = "";
            Order = Document.Elements.Count() + 1;
            EditItem = true;
            DocumentId = document.Id;
            Document = document;
            Document.Elements.Add(this);

        }
        public Element(Document document, int previousElement)
        {
            Document = document ?? throw new Exceptions("Document is null");
            Id = Guid.NewGuid().ToString();
            Content = "";
            Author = "";
            EditItem = false;
            Order = previousElement + 1;
            foreach (var item in Document.Elements.Where(w => w.Order > previousElement+1))
            {
                item.Order = item.Order + 1;
            }
            DocumentId = document.Id;
            Document = document;
            Document.Elements.Add(this);
        }
        public string Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string AIContent { get; set; }
        public int Order { get; set; }
        public int AppearingOrder { get; set; }
        public string Ideary { get; set; }
        public string DocumentId { get; set; }
        [JsonIgnore]
        public Document Document { get; set; }
        public string Hash { get; set; }
        public bool IsValid { get; set; }
        public string ParentElementId { get; set; }
        public bool EditItem { get; set; }

        public void AddIdea(string Idea)
        {
            Ideary = Idea;
        }



    }
}
