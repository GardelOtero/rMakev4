using Microsoft.AspNetCore.Mvc.RazorPages;
using rMakev2.DTOs;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Serialization;

namespace rMakev2.Models
{
    public class Element
    {
        public Element()
        {
            BlockContent = new List<BlockElement>();
        }
        public Element(Document document)
        {
            Document = document ?? throw new Exceptions("Document is null");
            GUID = Guid.NewGuid().ToString();
            Content = "";
            BlockContent = new List<BlockElement>();
            Author = "";
            Order = Document.Elements.Count() + 1;
            EditItem = true;
            DocumentId = document.GUID;
            Document = document;
            Document.Elements.Add(this);
            CreationDate = DateTime.Now;
            ParentGuid = "";
            OrderParentId = 0;
            Authors = new HashSet<string>();

        }
        public Element(Document document, int previousElement)
        {
            Document = document ?? throw new Exceptions("Document is null");
            GUID = Guid.NewGuid().ToString();
            Id = 0;
            Content = "";
            Author = "";
            EditItem = false;
            Order = previousElement + 1;
            foreach (var item in Document.Elements.Where(w => w.Order > previousElement+1))
            {
                item.Order = item.Order + 1;
            }
            DocumentId = document.GUID;
            Document = document;
            Document.Elements.Add(this);
            CreationDate = DateTime.Now;
            ParentGuid = "";
            OrderParentId = 0;
            Authors = new HashSet<string>();
            
        }

        public Element(Document document, LocalElementDTO localElement)
        {
            Document = document ?? throw new Exceptions("Document is null");

            Id = localElement.Id;
            GUID = localElement.GUID;
            Id = localElement.Id;
            Content = localElement.Content;
            BlockContent = localElement.BlockContent;
            Author = localElement.Author;
            AIContent = localElement.AIContent;
            Order = localElement.Order;
            AppearingOrder = localElement.AppearingOrder;
            Ideary = localElement.Ideary;
            DocumentId = localElement.DocumentId;
            Hash = localElement.Hash;
            IsValid = localElement.IsValid;
            ParentElementId = localElement.ParentElementId;
            EditItem = localElement.EditItem;
            CreationDate = localElement.CreationDate;
            ParentGuid = localElement.ParentGuid;
            OrderParentId = localElement.OrderParentId;
            Authors = localElement.Authors;
        }
        public string GUID { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public List<BlockElement> BlockContent { get; set; }
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
        public DateTime CreationDate { get; set; }
        public string ParentGuid { get; set; }
        public int OrderParentId { get; set; }
        public HashSet<string> Authors { get; set; }
        public void AddIdea(string Idea)
        {
            Ideary = Idea;
        }

        public void AddAuthor(string name)
        {
            if (name == "" || name == null) return;

            Authors.Add(name);

            foreach(var block in BlockContent)
            {
                block.Authors.Add(name);
            }
        }

        public LocalElementDTO ToLocalStorage()
        {
            LocalElementDTO localElement = new LocalElementDTO();

            localElement.Id = Id;
            localElement.GUID = GUID;
            localElement.Id = Id;
            localElement.Content = Content;
            localElement.BlockContent = BlockContent;
            localElement.Author = Author;
            localElement.AIContent = AIContent;
            localElement.Order = Order;
            localElement.AppearingOrder = AppearingOrder;
            localElement.Ideary = Ideary;
            localElement.DocumentId = DocumentId;
            localElement.Hash = Hash;
            localElement.IsValid = IsValid;
            localElement.ParentElementId = ParentElementId;
            localElement.EditItem = EditItem;
            localElement.CreationDate = CreationDate;
            localElement.ParentGuid = ParentGuid;
            localElement.OrderParentId = OrderParentId;
            localElement.Authors = Authors;

            return localElement;


        }

        public void FromLocalDTO(LocalElementDTO localElement)
        {
            Id = localElement.Id;
            GUID = localElement.GUID;
            Id = localElement.Id;
            Content = localElement.Content;
            BlockContent = localElement.BlockContent;
            Author = localElement.Author;
            AIContent = localElement.AIContent;
            Order = localElement.Order;
            AppearingOrder = localElement.AppearingOrder;
            Ideary = localElement.Ideary;
            DocumentId = localElement.DocumentId;
            Hash = localElement.Hash;
            IsValid = localElement.IsValid;
            ParentElementId = localElement.ParentElementId;
            EditItem = localElement.EditItem;
            CreationDate = localElement.CreationDate;
            ParentGuid = localElement.ParentGuid;
            OrderParentId = localElement.OrderParentId;
            Authors = localElement.Authors;
        }


    }
}
