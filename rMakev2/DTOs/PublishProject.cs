using rMakev2.Models;

namespace rMakev2.DTOs
{
    public class PublishProject
    {
        public string Id { get; set; }
        public DateTime PublicationDate { get; set; }
        //public Dictionary<int, string> Documents { get; } = new Dictionary<int, string>();

        public List<PublishDocument> Documents { get; set; }

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
}
