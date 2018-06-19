namespace eWolfPodcaster.Data
{
    public enum ShowStorageType
    {
        /// <summary>
        /// Just stream from RSS feel
        /// </summary>
        RssFeed,

        /// <summary>
        /// It's a local folder.
        /// </summary>
        LocalStorage,

        /// <summary>
        /// Use the RSS feed and download it to a local drive.
        /// </summary>
        RssDownLoad,

        /// <summary>
        /// Continue listening to the show we have started to listen to.
        /// </summary>
        Continue,

        /// <summary>
        /// List any new shows so we can quickly find them.
        /// </summary>
        NewShows,
    }

}
