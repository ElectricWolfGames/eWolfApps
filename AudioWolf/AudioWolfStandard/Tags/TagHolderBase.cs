namespace AudioWolfStandard.Tags
{
    [Serializable]
    public abstract class TagHolderBase
    {
        [NonSerialized]
        private bool _modifed = false;

        public List<TagData> Tags { get; } = new List<TagData>();

        public abstract bool Add(string name);

        public void ClearTags()
        {
            Tags.Clear();
        }

        protected TagData GetTagWithName(string name)
        {
            foreach (TagData td in Tags)
            {
                if (td.Name == name)
                    return td;
            }
            return null;
        }

        protected void SetModifed()
        {
            _modifed = true;
        }

        protected bool Modifed
        {
            get
            {
                return _modifed;
            }
        }
    }
}
