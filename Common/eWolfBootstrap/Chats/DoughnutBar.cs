using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eWolfBootstrap.Chats
{
    public class DoughnutBar
    {
        private List<DoughnutData> _doughnutDatas = new List<DoughnutData>();
        private int _colourIndex = 0;

        private List<string> _colors = new List<string>()
            {
                "#F7464A","#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"
            };

        public void Add(int count, string name)
        {
            DoughnutData doughnutData = new DoughnutData
            {
                Name = $"'{name}'",
                Value = count,
                Color = $"'{_colors[_colourIndex++]}'"
            };

            if (_colourIndex == _colors.Count)
            {
                _colourIndex = 0;
            }

            _doughnutDatas.Add(doughnutData);
        }

        public string Output()
        {
            StringBuilder labels = new StringBuilder();
            StringBuilder data = new StringBuilder();
            StringBuilder colour = new StringBuilder();
            labels.Append("labels: [");
            data.Append("data: [");
            colour.Append("backgroundColor: [");

            labels.Append(string.Join(",", _doughnutDatas.Select(x => x.Name)));
            data.Append(string.Join(",", _doughnutDatas.Select(x => x.Value)));
            colour.Append(string.Join(",", _doughnutDatas.Select(x => x.Color)));

            labels.Append("],");
            data.Append("],");
            colour.Append("],");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<canvas id='doughnutChart'></canvas>");

            stringBuilder.Append("<script>");
            stringBuilder.Append("var ctxD = document.getElementById('doughnutChart').getContext('2d');");
            stringBuilder.Append("var myLineChart = new Chart(ctxD, {");
            stringBuilder.Append("type: 'doughnut',");
            stringBuilder.Append("data: {");
            //stringBuilder.Append("labels: ['Red', 'Green', 'Yellow', 'Grey', 'Dark Grey'],");
            stringBuilder.Append(labels);
            stringBuilder.Append("datasets: [{");
            stringBuilder.Append(data);
            stringBuilder.Append(colour);
            stringBuilder.Append("hoverBackgroundColor: ['#FF5A5E', '#5AD3D1', '#FFC870', '#A8B3C5', '#616774']");
            stringBuilder.Append("}]");
            stringBuilder.Append("},");
            stringBuilder.Append("options: {");
            stringBuilder.Append("responsive: true");
            stringBuilder.Append("}");
            stringBuilder.Append("});");

            stringBuilder.Append("</script>");

            return stringBuilder.ToString();
        }

        public class DoughnutData
        {
            public string Name { get; set; }

            public int Value { get; set; }

            public string Color { get; set; }
        }
    }
}
