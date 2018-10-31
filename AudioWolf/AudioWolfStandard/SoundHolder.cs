using AudioWolfStandard.Data;
using System.Collections.Generic;

namespace AudioWolfStandard
{
    public class SoundHolder
    {
        public List<SoundItemData> SoundItems = new List<SoundItemData>();

        public SoundHolder()
        {
        }

        public void Add(SoundItemData item)
        {
            SoundItems.Add(item);
        }
    }
}
