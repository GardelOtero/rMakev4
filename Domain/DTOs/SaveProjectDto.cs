

namespace Domain.DTOs
{

   
    public class SaveProjectDto
    {
        
        public string id { get; set; }
        public string DataToken { get; set; }
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
        public List<DocumentDTO> Documents { get; set; }
        public string ParentProjectId { get; set; }
    }

    public class DocumentDTO
    {
        
        public string id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
        public string ProjectId { get; set; }
        public string ParentDocumentId { get; set; }

    }

    public class ElementDTO
    {

        public string id { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public string Ideary { get; set; }
        public string DocumentId { get; set; }
        public string ParentElementId { get; set; }

    }
}
