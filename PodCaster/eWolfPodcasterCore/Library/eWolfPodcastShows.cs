namespace eWolfPodcasterCore.Library
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eWolfPodcastShows
    {
        [System.Xml.Serialization.XmlElementAttribute("Show")]
        public eWolfPodcastShowsShow[] Show { get; set; }
    }
}
