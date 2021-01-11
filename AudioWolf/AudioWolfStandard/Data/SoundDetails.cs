using AudioWolfStandard.Interfaces;
using System.IO;

namespace AudioWolfStandard.Data
{
    public class SoundDetails : ISoundDetails
    {
        private bool _modified;

        private string _name;

        private string _path;
        private string _orginalName;
        private string _extension;

        public string FullPath
        {
            get
            {
                return $"{_path}{_name}{_extension}";
            }
            set
            {
                _orginalName = value;
                _extension = Path.GetExtension(value);
                _name = Path.GetFileNameWithoutExtension(value);
                _path = value.Replace(Path.GetFileName(value), string.Empty);
                _modified = false;
            }
        }

        public string OrginalName
        {
            get
            {
                return _orginalName;
            }
        }

        public bool IsModified
        {
            get
            {
                return _modified;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    _modified = true;
                }
            }
        }
    }
}