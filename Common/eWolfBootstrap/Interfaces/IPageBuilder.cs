using System;
using System.Collections.Generic;
using System.Text;

namespace eWolfBootstrap.Interfaces
{
    public interface IPageBuilder
    {
        void Append(string text);

        void Output();

        string GetString();
    }
}
