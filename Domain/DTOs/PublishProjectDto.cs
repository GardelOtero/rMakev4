namespace rMakev2.DTOs
{
    public class PublishProjectDto
    {
        public string id { get; set; }

        public string domain { get; set; }

        public DateTime Date { get; set; }

        public List<PublishDocumentDTO> Documents { get; set; }
    }

    public class PublishDocumentDTO
    {
        public string id { get; set; } 
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string Content { get; set; }



    }
}
