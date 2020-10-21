using eWolfBootstrap.Enums;
using System.Collections.Generic;

namespace eWolfBootstrap.Interfaces
{
    public interface IPageHeader
    {
        string Author { get; set; }
        List<BootstrapOptions> ExtraIncludes { get; set; }
        List<string> Keywords { get; set; }
        string MetaDetails { get; set; }
        string Title { get; set; }
    }
}
