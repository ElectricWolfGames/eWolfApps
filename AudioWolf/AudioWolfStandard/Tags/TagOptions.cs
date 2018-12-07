using System;

namespace AudioWolfStandard.Tags
{
    [Serializable]
    public class TagOptions
    {
        public char Seperator { get; set; } = ' ';
        public bool TagInBoxs { get; set; }
        public bool RenameFileWithTags { get; set; }
        public bool KeepFirstPartOfName { get; set; }

        public bool TagNotStoredInFileName { get; set; }
    }
}
