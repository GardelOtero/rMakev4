using rMakev2.Models;

namespace rMakev2.ViewModel.Interfaces
{
    public interface IDocumentViewModel
    {
        public void SelectDocument(Document document);
        public void CloneDocument();
        public void NewDocument();

        public void NewDocumentMenu(Project project);

        public void UpdateDocumentMenu(Document document);

        public void DeleteDocument();

        public void DeleteDocumentMenu(Document document);
        public void BlocktoElement(string elementsJs);
        public void ElementstoCSharp();
        public void MergeDocumentsIntoNewOne(Document First, Document Second);
    }
}
