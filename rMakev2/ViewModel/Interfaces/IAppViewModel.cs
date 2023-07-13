using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using rMakev2.Models;

namespace rMakev2.ViewModel.Interfaces
{
    public interface IAppViewModel
    {
        public void InitializePortfolio();
        public void Save();
        public void HideSaveModal();
        public void ShowSaveModal();
        public void DocumentMenu();
        public void HidePublishModal();
        public void ShowPublishModal();
        public Task SaveContentAsync();
        public void DisplayMenu();
        public void BlocktoElement(string elementsJs);
        public void ElementstoCSharp();
        public void MergeDocumentsIntoNewOne(Document First, Document Second);
        public void BlockRTAFocus();
        public void UnBlockRTAFocus();
        public string HashString(string text, string salt);
    }
}
