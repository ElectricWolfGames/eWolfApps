using AudioWolfStandard.Data;
using System;
using System.Collections.Generic;
using System.Text;

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
