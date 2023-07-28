namespace Domain.DTOs
{
    public class PublishProjectDTO
    {
        public string id { get; set; }
        public DateTime PublicationDate { get; set; }
        //public Dictionary<int, string> Documents { get; } = new Dictionary<int, string>();

        public List<PublishDocumentDTO> Documents { get; set; }

        public List<string> Authors { get; set; } = new List<string>(); //Person
        //public List<Person> Owners { get; } = new List<Person>(); // <- no me gusta tanto la idea... 
        public string Sign { get; set; }

        //private PublishProject(){}

        //public PublishProject(Project project)
        //{
        //  Id = Guid.NewGuid().ToString();
        // PublicationDate = DateTime.Now.Date;
        //}

        //public PublishDocument AddDocument(Document doc)
        //{
        //  PublishDocument publishDocument = new PublishDocument(doc);
        //Documents.Add(publishDocument.Order, publishDocument.Id);
        //return publishDocument;
        //}
    }
    public class PublishDocumentDTO
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Content { get; set; } = string.Empty;
        public string ContentType { get; set; }




    }
}
