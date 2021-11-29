using System.Collections.Generic;
using eWolfBootstrap.Enums;

namespace eWolfBootstrap.Interfaces
{
    public interface IPageHeader
    {
        string Author { get; set; }
        string Description { get; set; }
        List<BootstrapOptions> ExtraIncludes { get; set; }
        List<string> Keywords { get; set; }
        string Title { get; set; }
    }
}