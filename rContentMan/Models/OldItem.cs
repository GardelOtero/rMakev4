namespace rContentMan.Models
{
    



    public class OldItem
    {

        public string id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public List<OldSaveProjectDTO> Projects { get; set; }

        public DateTime CreationDate { get; set; }




    }



    public class OldSaveProjectDTO

    {

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
        public int OrderId { get; set; }


        public string PortfolioId { get; set; }

        public List<string> ParentId { get; set; }

        public List<OldSaveDocumentDTO> Documents { get; set; }

        public string Author { get; set; }




    }

    public class OldSaveDocumentDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string ProjectId { get; set; }
        public int OrderId { get; set; }
        public List<string> ParentId { get; set; }
        public List<OldSaveElementDTO> Elements { get; set; }
        public string GitSha { get; set; }
        public string ImageLink { get; set; }

        public string Tag { get; set; }
    }

    public class OldSaveElementDTO
    {
        public string Id { get; set; }
        public BlockContent Block { get; set; }
        public DateTime CreationDate { get; set; }
        public string Author { get; set; }
        public string DocumentId { get; set; }
        public int OrderId { get; set; }
        public string ParentElement { get; set; }
        public string NestParent { get; set; }
        public string HashString { get; set; }


    }

}
