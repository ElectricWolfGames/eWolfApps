namespace eWolfPodcasterCore.Library
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eWolfPodcastShowsShow
    {
        private string nameField;

        private string urlField;

        private string categoryField;

        private string description;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public string Category
        {
            get
            {
                return categoryField;
            }
            set
            {
                categoryField = value;
            }
        }

        public string Description
        {
            get => description;
            set => description = value;
        }
    }
}
