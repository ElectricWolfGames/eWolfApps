using AudioWolfStandard.Helpers;
using AudioWolfStandard.Interfaces;
using AudioWolfStandard.Services;

namespace AudioWolfStandard.Tags
{
    [Serializable]
    public class GlobalTagStore : TagHolderBase, ISaveable
    {
        public string GetFileName
        {
            get
            {
                return "GlobalTagStore";
            }
        }

        public GlobalTagStore()
        {
            // TODO : Need to load in the globel tag list.
            // Add the load code here.
            Load();
        }

        public static string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\AudioWolf");
        }

        public void Load()
        {
            PersistenceHelper<GlobalTagStore> ph = new PersistenceHelper<GlobalTagStore>(GetOutputFolder());

            string outputFileName = Path.Combine(GetOutputFolder(), GetFileName);
            GlobalTagStore tempSoundHolder = ph.LoadDataSingle(outputFileName);

            lock (Tags)
            {
                Tags.Clear();
                if (tempSoundHolder != null)
                {
                    Tags.AddRange(tempSoundHolder.Tags);
                }
            }
        }

        public override bool Add(string name)
        {
            TagData td = new TagData
            {
                Name = name
            };
            Add(td);
            return true;
        }

        public bool Add(TagData tag)
        {
            TagData tdold = GetTagWithName(tag.Name);
            if (tdold != null)
                return false;

            Tags.Add(tag);
            return true;
        }

        public void AddTagRange(List<TagData> tags)
        {
            Tags.AddRange(tags);
        }

        public static void AddTag(string name)
        {
            GlobalTagStore gts = ServiceLocator.Instance.GetService<GlobalTagStore>();
            if (gts.Add(TagHelper.CreateTag(name)))
            {
                // save the list or mark it as modified
                gts.SetModifed();
                gts.SaveIfNeeded();
            }
        }

        public void SaveIfNeeded()
        {
            if (Modifed)
                Save();
        }

        private void Save()
        {
            lock (Tags)
            {
                PersistenceHelper<GlobalTagStore> ph = new PersistenceHelper<GlobalTagStore>(GetOutputFolder());
                ph.SaveDataSingle(this);
            }
        }
    }
}
