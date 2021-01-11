namespace eWolfBootstrapTests.Helpers
{
    public class HTMLExtractionTests
    {
        [Test]
        [TestCase("<tr><th>Header</th></tr>", "<th>Header</th>")]
        [TestCase("<tr><th colspan=\"2\" style=\"text-align:center;font-size:125%;font-weight:bold\">GCR Class 1B<br />LNER Class L1 (later L3)</th></tr>",
            "<th colspan=\"2\" style=\"text-align:center;font-size:125%;font-weight:bold\">GCR Class 1B<br />LNER Class L1 (later L3)</th>")]
        public void ShouldGetTagLine(string raw, string expected)
        {
            string results = HTMLExtraction.GetTagLine(raw, "th", 0);
            results.Should().Be(expected);
        }
    }
}