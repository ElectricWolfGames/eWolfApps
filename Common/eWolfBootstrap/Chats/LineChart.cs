using System.Collections.Generic;

namespace eWolfBootstrap.Chats
{
    public class LineChart
    {
        // https://mdbootstrap.com/docs/jquery/javascript/charts/#radar-chart
        private List<LineChartData> _doughnutDatas = new List<LineChartData>();

        public void Add(int data, string name)
        {
            LineChartData lineChartData = new LineChartData
            {
                Name = $"'{name}'",
                Value = data
            };

            _doughnutDatas.Add(lineChartData);
        }
    }
}
