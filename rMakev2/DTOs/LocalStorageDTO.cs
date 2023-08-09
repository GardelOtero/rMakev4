using rMakev2.Models;

namespace rMakev2.DTOs
{
    public class LocalPortfolioDTO
    {
        public int Id { get; set; }
        public string GUID { get; set; }
        public List<string> projectsGUID { get; set; }
        public string AppId { get; set; }
        public DateTime CreationDate { get; set; }
        public string rIdSignature { get; set; }
        public DateTime SignatureDate { get; set; }
        public List<string> Authors { get; set; }
        public string DataToken { get; set; }
    }

    public class LocalProjectDTO
    {
        public int Id { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public List<string> Labels { get; set; }
        public List<string> Versions { get; set; }
        public bool IsPublic { get; set; }
        public bool IsWebsite { get; set; }
        public DateTime CreationDate { get; set; }
        public HashSet<string> Authors { get; set; }
        public string PathPreviewImage { get; set; }
        public List<string> DocumentsGUID { get; set; }
        public string PortfolioId { get; set; }
        public string ParentProjectId { get; set; }
        public bool DisplayMenu { get; set; }
        public bool DisplayDocumentMenu { get; set; }
        public bool DisplayJson { get; set; }
        public bool BlockRTAFocus { get; set; }
        public string selectedDocumentGUID { get; set; }
    }

    public class LocalDocumentDTO
    {
        public int Id { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public HashSet<string> Authors { get; set; }
        public string PathPreviewImage { get; set; }
        public bool IsPublic { get; set; }
        public int Order { get; set; }
        public List<string> ElementsGUID { get; set; }
        public string Content { get; set; }
        public string ProjectId { get; set; }
        public string ParentDocumentId { get; set; }
        public bool IsOrdered { get; set; }
    }

    public class LocalElementDTO
    {
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
        public string Hash { get; set; }
        public bool IsValid { get; set; }
        public string ParentElementId { get; set; }
        public bool EditItem { get; set; }
        public DateTime CreationDate { get; set; }
        public string ParentGuid { get; set; }
        public int OrderParentId { get; set; }
        public HashSet<string> Authors { get; set; }
    }
}
