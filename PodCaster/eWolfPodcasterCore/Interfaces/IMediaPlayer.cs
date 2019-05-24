using eWolfPodcasterCore.Data;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IMediaPlayer
    {
        void Play();

        void Stop();

        void Pause();

        void PlayEpisode(EpisodeControl episode);

        void UpDateInterval(EpisodeControl episode, out bool playNextEpsoide);
    }
}