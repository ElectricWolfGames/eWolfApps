using AudioWolfStandard.Interfaces;
using System;
using System.Windows.Media;

namespace AudioWolfUI.Services
{
    public class MediaPlayerService
    {
        private static MediaPlayerService _mediaPlayerService = new MediaPlayerService();
        private readonly MediaPlayer _mediaPlayer;

        public MediaPlayerService()
        {
            _mediaPlayer = new MediaPlayer();
        }

        public void Play()
        {
            _mediaPlayer.Play();
        }

        public void PlayEpisode(ISoundDetails soundDetails)
        {
            _mediaPlayer.Open(new Uri(soundDetails.OrginalName));
            _mediaPlayer.Position = new TimeSpan(0);
            _mediaPlayer.Play();
        }
    }
}