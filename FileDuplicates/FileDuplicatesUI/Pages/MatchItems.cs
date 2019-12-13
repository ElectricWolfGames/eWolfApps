using FileDuplicatesUI.Data;

namespace FileDuplicatesUI.Pages
{
    public class MatchItems
    {
        public Actions Action { get; set; }

        public string FilePathName
        {
            get
            {
                return $"{Path}\\{Name}";
            }
        }

        public string Name { get; set; }

        public string Path { get; set; }
        public FileDetails Store { get; set; }
    }
}