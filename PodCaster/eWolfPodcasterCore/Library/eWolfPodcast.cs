namespace eWolfPodcasterCore.Library
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class eWolfPodcast
    {
        private eWolfPodcastShows showsField;

        /// <remarks/>
        public eWolfPodcastShows Shows
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
