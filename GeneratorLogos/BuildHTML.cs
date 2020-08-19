using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneratorLogos
{
    public class BuildHTML
    {
        public string Name { get; set; }
        public string FileName { get; set; }

        public string ColorA { get; set; }
        public string ColorB { get; set; }

        public BuildHTML(string name, string fileName)
        {
            Name = name;
            FileName = fileName;
        }

        public void Build()
        {
            string path = @$"C:\GitHub\eWolfApps\GeneratorLogos\Logos\{FileName}";
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<body>");
            sb.AppendLine("<canvas id='myCanvas' width='1000' height='800'");
            sb.AppendLine($"style='border: 1px solid #{ColorA};'>");
            sb.AppendLine("Your browser does not support the canvas element.");
            sb.AppendLine("</canvas>");

            sb.AppendLine("<script>");
            sb.AppendLine("var canvas = document.getElementById('myCanvas');");
            sb.AppendLine("var ctx=canvas.getContext('2d');");
            sb.AppendLine("ctx.font='74px Comic Sans MS';");
            sb.AppendLine($"ctx.fillStyle = '#{ColorB}';");
            sb.AppendLine("ctx.textAlign = 'center';");
            sb.AppendLine($"ctx.fillText('{Name}', canvas.width/2, canvas.height/2);");
            sb.AppendLine("ctx.font = '74px Comic Sans MS';");
            sb.AppendLine("ctx.lineWidth = 2;");
            sb.AppendLine($"ctx.strokeStyle = '#{ColorA}';");
            sb.AppendLine($"ctx.strokeText('{Name}',canvas.width/2, canvas.height/2);");
            sb.AppendLine("</script>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            var file = sb.ToString();

            File.WriteAllText(path, file);
        }
    }
}
