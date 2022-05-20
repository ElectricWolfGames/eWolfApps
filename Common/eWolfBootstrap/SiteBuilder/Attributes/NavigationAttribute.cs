using eWolfBootstrap.SiteBuilder.Enums;
using System;

namespace eWolfBootstrap.SiteBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Class |
                       AttributeTargets.Struct,
                       AllowMultiple = true)]
    public sealed class NavigationAttribute : Attribute
    {
        private readonly int _index;
        private readonly NavigationTypes _navigationTypes;

        public NavigationAttribute(NavigationTypes type, int index)
        {
            _navigationTypes = type;
            _index = index;
        }

        public int Index
        {
            get { return _index; }
        }

        public NavigationTypes NavigationType
        {
            get { return _navigationTypes; }
        }
    }
}