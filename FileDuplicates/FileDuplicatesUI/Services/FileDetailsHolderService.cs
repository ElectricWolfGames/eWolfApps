using FileDuplicatesUI.Data;

namespace FileDuplicatesUI.Services
{
    public class FileDetailsHolderService
    {
        public FileDetailsObservableCollection<FileDetails> Details = new FileDetailsObservableCollection<FileDetails>();
    }
}