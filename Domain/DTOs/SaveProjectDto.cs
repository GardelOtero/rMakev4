

namespace Domain.DTOs
{

   
    public class SaveProjectDto
    {
        
        public string id { get; set; }
        public string DataToken { get; set; }
        public DateTime CreationDate { get; set; }
        public string rIdSignature { get; set; }
        public DateTime SignatureDate { get; set; }
        public List<string> Authors { get; set; }
        public List<ProjectDTO> Projects { get; set; }
       // public UIDto Ui { get; set; }

    }

    /*public class UIDto
    {
        public string IdSelectedProject { get; set; }
        public string IdSelectedDocument { get; set; }
    }*/

    public class ProjectDTO
    {
        
        public string id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public HashSet<string> Authors { get; set; }
        public string PathPreviewImage { get; set; }
        public List<DocumentDTO> Documents { get; set; }
        public string PortfolioId { get; set; }
        public string ParentProjectId { get; set; }
    }

    public class DocumentDTO
    {
        
        public string id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public HashSet<string> Authors { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
        public string ProjectId { get; set; }
        public string ParentDocumentId { get; set; }
        public List<ElementDTO> Elements { get; set; }

    }

    public class ElementDTO
    {

        public string id { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public string Ideary { get; set; }
        public string DocumentId { get; set; }
        public string ParentElementId { get; set; }
        public HashSet<string> Authors { get; set; }

        public List<BlockElementDTO> BlockContent { get; set; }

    }
    public class BlockElementDTO
    {
        public HashSet<string> Authors { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public DataDTO data { get; set; }
        public string elementId { get; set; }
    }

    public class DataDTO
    {
        public string text { get; set; }
        public int? level { get; set; }
        public string caption { get; set; }
        public string alignment { get; set; }
        public string url { get; set; }
        public bool? withBorder { get; set; }
        public bool? withBackground { get; set; }
        public bool? stretched { get; set; }
        public string style { get; set; }
        public List<ItemDTO> items { get; set; }
        public string link { get; set; }
        //public Meta meta { get; set; }
        public string code { get; set; }
        public bool? withHeadings { get; set; }
        public List<List<string>>? content { get; set; }
    }

    public class ItemDTO
    {
        public string content { get; set; }
        public List<object> items { get; set; }
        public string text { get; set; }
        public bool? @checked { get; set; }
    }
}
