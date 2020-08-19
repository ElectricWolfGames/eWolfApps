using System.Collections.Generic;

namespace eWolfBootstrap.Interfaces
{
    public interface IPageHeader
    {
        string Title { get; set; }
        List<string> Keywords { get; set; }
        string MetaDetails { get; set; }

        string Author { get; set; }
    }
}
