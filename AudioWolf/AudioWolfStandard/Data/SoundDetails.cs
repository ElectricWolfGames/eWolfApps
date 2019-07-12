using AudioWolfStandard.Interfaces;
using System.IO;

namespace AudioWolfStandard.Data
{
    public class SoundDetails : ISoundDetails
    {
        private string _name;

        private string _path;

        private bool _modified { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                _modified = true;
            }
        }

        public string FullPath
        {
            get
            {
                return $"{_path}//{_name}";
            }
            set
            {
                _name = Path.GetFileNameWithoutExtension(value);
                _path = value.Replace(Path.GetFileName(value), string.Empty);
            }
        }
    }
}
