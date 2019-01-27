namespace eWolfPodcasterCore.Library
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class eWolfPodcast
    {
        [System.Xml.Serialization.XmlElementAttribute("Shows")]
        public eWolfPodcastShows Shows { get; set; }
    }
}
