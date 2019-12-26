using FileDuplicates.Data;

namespace FileDuplicates.Services
{
    public class FileDetailsHolderService
    {
        public FileDetailsObservableCollection<FileDetails> Details = new FileDetailsObservableCollection<FileDetails>();
    }
}