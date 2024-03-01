namespace rContentMan.Models
{
    public class PublishDocument
    {
        public string id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Author { get; set; }
        public string PortfolioId { get; set; }
        public int? OrderId { get; set; }
        public List<string> ParentId { get; set; }
        public string Content { get; set; }
        public string ImageLink { get; set; }
        public string Tag { get; set; }
        

        public PublishDocument() { }

    }
}
