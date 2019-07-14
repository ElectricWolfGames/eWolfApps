using AudioWolfStandard.Data;
using AudioWolfStandard.Helpers;
using AudioWolfStandard.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

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

        public void RenameFiles()
        {
            foreach (ISoundDetails sound in _sounds)
            {
                if (sound.IsModified)
                {
                    string oldPath = sound.OrginalName;
                    string newPath = sound.FullPath;

                    if (oldPath != newPath)
                    {
                        try
                        {
                            File.Move(oldPath, newPath);
                            sound.FullPath = newPath;
                        }
                        catch
                        {
                            Console.WriteLine($"FAILED TO RENAME {oldPath}");
                        }
                    }
                }
            }
        }
    }
}
