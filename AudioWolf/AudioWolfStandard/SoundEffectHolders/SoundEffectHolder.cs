using AudioWolfStandard.Data;
using AudioWolfStandard.Helpers;
using AudioWolfStandard.Interfaces;
using System.Collections.Generic;

namespace AudioWolfStandard
{
    public class SoundEffectHolder
    {
        private List<ISoundDetails> _sounds = new List<ISoundDetails>();

        public SoundEffectHolder()
        {
        }

        public void Populate()
        {
            List<string> names = FileSearchHelper.GetAllFiles();
            foreach (string name in names)
            {
                SoundDetails sd = new SoundDetails()
                {
                    FullPath = name
                };
                _sounds.Add(sd);
            }
        }

        public List<ISoundDetails> Sounds
        {
            get
            {
                return _sounds;
            }
        }
    }
}
