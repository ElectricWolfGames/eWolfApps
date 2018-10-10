namespace eWolfPodcasterCore.Library
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class eWolfPodcast
    {
        private eWolfPodcastShows[] showsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Shows")]
        public eWolfPodcastShows[] Shows
        {
            get
            {
                return this.showsField;
            }
            set
            {
                this.showsField = value;
            }
        }
    }
}
