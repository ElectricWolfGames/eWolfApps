using eWolfPodcasterCore.Data;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IMediaPlayer
    {
        void Forward(int minutes);

        void Pause();

        void Play();

        void PlayEpisode(EpisodeControl episode);

        void Rewind(int minutes);

        void Stop();

        void UpDateInterval(EpisodeControl episode, out bool playNextEpsoide);
    }
}