
using Blazorise;
using Newtonsoft.Json;
using rMakev2.Models;
using System.ComponentModel.DataAnnotations;

namespace rMakev2.DTOs
{

   
    public class SaveProjectDto
    {
        [JsonProperty(PropertyName = "id")]
        [Required]
        [RegularExpression(@"^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", ErrorMessage = "This is not a valid Guid")]
        public string Id { get; set; }
        public string DataToken { get; set; }
        public List<ProjectDTO> Projects { get; set; }
        //public UIDto Ui { get; set; }

    }

    /*public class UIDto
    {
        public string IdSelectedProject { get; set; }
        public string IdSelectedDocument { get; set; }
    }*/

    public class ProjectDTO
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<DocumentDTO> Documents { get; set; }
        public string ParentProjectId { get; set; }
    }

    public class DocumentDTO
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int Order { get; set; }
        public List<ElementDTO> Elements { get; set; }
        public string ProjectId { get; set; }
        public string ParentDocumentId { get; set; }

    }
    
    public class ElementDTO
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public string Ideary { get; set; }
        public string DocumentId { get; set; }
        public string ParentElementId { get; set; }
        public string Hash { get; set; }


    }
}
