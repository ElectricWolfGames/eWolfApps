using System;

namespace eWolfBootstrap.SiteBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Class |
                       AttributeTargets.Struct,
                       AllowMultiple = false)]
    public sealed class AddGalleryAttribute : Attribute
    {

        public AddGalleryAttribute()
        {
        }
    }
}