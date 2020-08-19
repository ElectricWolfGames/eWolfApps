using eWolfBootstrap.Helpers;
using eWolfBootstrap.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eWolfBootstrap.Builders
{
    // https://getbootstrap.com/docs/4.0/examples/
    public class ResponsiveSidebarPage : PageBuilder
    {
        private string _currentSection;
        private List<string> _sectionsName = new List<string>();
        private Dictionary<string, StringBuilder> _sections = new Dictionary<string, StringBuilder>();

        public ResponsiveSidebarPage(string fileName, string path, string offSet, IPageHeader pageHeader)
        {
            _fileName = fileName;
            _path = path;
            _stringBuilder.Append(PageHeaderHelper.PageHeader(pageHeader, offSet));
            _stringBuilder.Append("<Body>");
        }

        public void StartSection(string name)
        {
            _currentSection = name;
            _sectionsName.Add(name);
            _sections.Add(name, new StringBuilder());

            AppendSection($"<a id='{name}'></a>");
            AppendSection("<br /><br />");

            AppendSection($"<h2>{name}</h2>");
        }

        public void AppendSection(string text)
        {
            _sections[_currentSection].Append(text);
        }

        public void AppendCode(string code)
        {
            //_sections[_currentSection].Append("<code>");
            _sections[_currentSection].Append(code);
            //_sections[_currentSection].Append("</code>");
        }

        public override void Output()
        {
            // fixed pos
            // https://stackoverflow.com/questions/40497288/how-to-create-a-fixed-sidebar-layout-with-bootstrap-4

            StringBuilder nav = new StringBuilder();
            nav.Append("<nav class='col-md-2 d-none d-md-block sidebar position-fixed'>");
            // nav.Append("<nav class='col-md-2 sidebar position-fixed wolf-sidebar'>");
            nav.Append("<div class='sidebar-sticky'>");
            nav.Append("<ul class='nav flex-column'>");

            foreach (string name in _sectionsName)
            {
                nav.Append("<li class='nav-item'>");
                // nav.Append($"<a class='nav-link active' href='#{name}'>");
                nav.Append($"<a class='nav-link' href='#{name}'>");
                nav.Append("<span data-feather='home'></span>");
                nav.Append($"{name}");
                nav.Append(" </a>");
                nav.Append("</li>");
            }

            nav.Append("</ul>");
            nav.Append("</div>");
            nav.Append("</nav>");

            StringBuilder main = new StringBuilder();
            main.Append("<main role='main' class='border-primary border-left border-right col-md-9 ml-sm-auto col-lg-10 pt-3 px-4 wolf-main'>");
            foreach (string name in _sectionsName)
            {
                main.Append(_sections[name].ToString());
            }
            main.Append("</main>");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<div class='container mt-4'>");
            stringBuilder.Append(_stringBuilder.ToString());
            stringBuilder.Append("<div class='container-fluid'>");
            stringBuilder.Append("  <div class='row'>");
            stringBuilder.Append(nav.ToString());
            stringBuilder.Append(main.ToString());

            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");

            Directory.CreateDirectory(_path);
            File.WriteAllText(_path + _fileName, stringBuilder.ToString());
        }
    }
}
