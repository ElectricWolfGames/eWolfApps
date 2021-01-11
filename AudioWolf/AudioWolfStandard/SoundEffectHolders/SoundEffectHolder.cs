using AudioWolfStandard.Data;
using AudioWolfStandard.Helpers;
using AudioWolfStandard.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AudioWolfStandard
{
    public class SoundEffectHolder
    {
        private List<ISoundDetails> _sounds = new List<ISoundDetails>();

        public SoundEffectHolder()
        {
        }

        public void Add(SoundDetails soundDetail)
        {
            _sounds.Add(soundDetail);
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

        public void FixNames()
        {
            foreach (ISoundDetails sound in _sounds)
            {
                sound.Name = FixName(sound.Name);
            }
        }

        public static string FixName(string name)
        {
            List<string> allParts = name.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (int i = 0; i < allParts.Count; i++)
            {
                string firstLetter = allParts[i][0].ToString();
                firstLetter = firstLetter.ToUpper();

                string partNew = $"{firstLetter}{allParts[i].Substring(1)}";
                allParts[i] = partNew;
            }

            allParts = allParts.Distinct().ToList();

            string fullName = string.Join(" ", allParts);

            return fullName;
        }
    }
}