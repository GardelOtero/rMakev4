using rMakev2.Models;

namespace rMakev2.DTOs
{
    public class PublishDocument
    {
        public string Id { get; set; } 
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Content { get; set; } = string.Empty;
        public string ContentType { get; set; }


        //private PublishDocument(){}
        
        /*public PublishDocument(PublishProject publishProject, Document document)
        {
            Id = Guid.NewGuid().ToString();
            ProjectId = publishProject.Id;
            Name = document.Name;
            Order = document.Order;
            Content = document.Content;

            ContentType = "html";

            /*foreach (Element element in document.Elements.OrderBy(x => x.Order)) {
                if(!String.IsNullOrWhiteSpace(element.Content))
                    Content += "<section>" + element.Content + "</section>";
            }*/
       // }
    }
}
