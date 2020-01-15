using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Interfaces;
using System;
using System.Windows.Media;

namespace eWolfPodcasterUI.Media
{
    public class MediaPlayerWrapper : IMediaPlayer
    {
        private readonly MediaPlayer _mediaPlayer;

        public MediaPlayerWrapper()
        {
            _mediaPlayer = new MediaPlayer();
        }

        public void Forward(int minutes)
        {
            _mediaPlayer.Position += new TimeSpan(0, minutes, 0);
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
        }

        public void Play()
        {
            _mediaPlayer.Play();
        }

        public void PlayEpisode(EpisodeControl episode)
        {
            _mediaPlayer.Open(new Uri(episode.UrlToPlay));
            _mediaPlayer.Position = new TimeSpan(episode.PlayedLength);
            _mediaPlayer.Play();
        }

        public void Rewind(int minutes)
        {
            _mediaPlayer.Position -= new TimeSpan(0, minutes, 0);
        }

        public void SetSpeed(float speed)
        {
            _mediaPlayer.SpeedRatio = speed;
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
        }

        public void UpDateInterval(EpisodeControl episode, out bool playNextEpsoide)
        {
            playNextEpsoide = false;
            if (_mediaPlayer.Source != null && _mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                episode.PlayedLength = _mediaPlayer.Position.Ticks;

                double MaxLength = 781;

                double totalWidth = MaxLength;

                totalWidth /= _mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                totalWidth *= _mediaPlayer.Position.TotalMilliseconds;
                episode.PlayedLengthScaled = (float)totalWidth;

                if (episode.PlayedLengthScaled >= MaxLength - 2)
                {
                    playNextEpsoide = true;
                }
            }
        }
    }
}