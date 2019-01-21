namespace eWolfPodcasterCore.Library
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eWolfPodcastShows
    {
        private eWolfPodcastShowsShow[] showField;

        [System.Xml.Serialization.XmlElementAttribute("Show")]
        public eWolfPodcastShowsShow[] Show
        {
            get
            {
                return this.showField;
            }
            set
            {
                this.showField = value;
            }
        }
    }
}
