using AudioWolfStandard.Tags;

namespace AudioWolfStandard.Helpers
{
    public static class TagHelper
    {
        public static string CleanseTagName(string name)
        {
            string[] parts = name.Split(' ');
            if (parts.Length == 1)
                return name.Trim();

            StringBuilder result = new StringBuilder();
            foreach (string part in parts)
            {
                string temp = part.Trim().ToLower();
                if (temp.Length > 1)
                {
                    string firstChar = temp[0].ToString().ToUpper();
                    temp = firstChar + temp.Substring(1, temp.Length - 1);
                }
                result.Append(temp);
            }

            return result.ToString();
        }

        public static TagData CreateTag(string name)
        {
            TagData tagData = new TagData
            {
                Name = CleanseTagName(name)
            };

            return tagData;
        }

        public static List<TagData> GetValidTags(string tags)
        {
            List<TagData> tagDatas = new List<TagData>();

            return tagDatas;
        }
    }
}
