namespace eWolfPodcasterCore.Library
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eWolfPodcastShowsShow
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }
    }
}