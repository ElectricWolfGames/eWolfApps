using eWolfCommon.Builders;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace eWolfCommonUnitTests.Builders
{
    public class HtmlBuilderTests
    {
        [Test]
        public void ShouldAddListItem()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartUnordedList();
            hb.AddListItem("here");
            hb.EndUnordedList();

            hb.Output().Should().Contain("<li>here</li>");
        }

        [Test]
        public void ShouldAddNewLine()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.AddNewLine();
            hb.Output().Should().Contain("br");
        }

        [Test]
        public void ShouldAppendTextToEnd()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.Append("Start");
            hb.Append("End");

            hb.Output().Should().Contain("StartEnd");
        }

        [Test]
        public void ShouldCreateRefLink()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.AddLink("MyLink", "myURL");

            string output = hb.Output();
            output.Should().Contain("MyLink");
            output.Should().Contain("myURL");
            output.Should().Contain("</a>");
        }

        [Test]
        public void ShouldCreateTableWithDataAndAtributes()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartTable();
            hb.AddTableRow(new string[] { "Row1Data1", "Row1Data2" }, new string[] { "bgcolor='#FF0000'", "bgcolor='#FF0000'" });
            hb.AddTableRow(new string[] { "Row2Data1", "Row2Data2" });
            hb.EndTable();

            hb.Output().Should().Contain("<table>");
            hb.Output().Should().Contain("<td bgcolor='#FF0000'>Row1Data1</td>");
            hb.Output().Should().Contain("<td bgcolor='#FF0000'>Row1Data2</td>");
            hb.Output().Should().Contain("<td >Row2Data1</td>");
            hb.Output().Should().Contain("<td >Row2Data2</td>");
            hb.Output().Should().Contain("</table>");
        }

        [Test]
        public void ShouldCreateTableWithDataRow()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartTable();
            hb.AddTableRow(new string[] { "Row1Data1", "Row1Data2" });
            hb.AddTableRow(null);
            hb.AddTableRow(new string[] { "Row2Data1", "Row2Data2" });
            hb.EndTable();

            hb.Output().Should().Contain("<table>");
            hb.Output().Should().Contain("<td >Row1Data1</td>");
            hb.Output().Should().Contain("<td >Row2Data1</td>");
            hb.Output().Should().Contain("<td >Row2Data2</td>");
            hb.Output().Should().Contain("<td >Row1Data2</td>");
            hb.Output().Should().Contain("</table>");
        }

        [Test]
        public void ShouldCreateTableWithHeader()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartTable();
            hb.AddTableHeader(new string[] { "HeaderA", "HeaderB" });
            hb.EndTable();

            hb.Output().Should().Contain("<table>");
            hb.Output().Should().Contain("<th>HeaderA</th>");
            hb.Output().Should().Contain("<th>HeaderB</th>");
            hb.Output().Should().Contain("</table>");
        }

        [Test]
        public void ShouldHandleTablesInTables()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartTable();
            hb.StartTable();
            hb.EndTable();
            hb.EndTable();

            hb.Output().Should().Contain("<table>");
            hb.Output().Should().Contain("</table>");
        }

        [Test]
        public void ShouldNotAllowClosingATableWithoutStarting()
        {
            HtmlBuilder hb = new HtmlBuilder();

            Action a = (() => hb.EndTable());
            a.Should().Throw<InvalidProgramException>();
        }

        [Test]
        public void ShouldOutputUnordedLists()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartUnordedList();
            hb.EndUnordedList();

            hb.Output().Should().Contain("<ul>");
            hb.Output().Should().Contain("</ul>");
        }

        [Test]
        public void ShouldStartAndEndTables()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartTable();
            hb.EndTable();

            hb.Output().Should().Contain("<table>");
            hb.Output().Should().Contain("</table>");
        }

        [Test]
        public void ShouldStartAndEndTablesWithBorder()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartTableWithBorder();
            hb.EndTable();

            hb.Output().Should().Contain("<table border='1'>");
            hb.Output().Should().Contain("</table>");
        }

        [Test]
        public void ShouldThrowExceptionWhenAddingListItemWithoutList()
        {
            HtmlBuilder hb = new HtmlBuilder();

            Action a = (() => hb.AddListItem("here"));
            a.Should().Throw<InvalidProgramException>();
        }

        [Test]
        public void ShouldThrowExceptionWhenTableIsNotClosed()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartTable();

            Action a = (() => hb.Output());
            a.Should().Throw<InvalidProgramException>();
        }

        [Test]
        public void ShouldThrowExceptionWhenUnordedListClosed()
        {
            HtmlBuilder hb = new HtmlBuilder();
            hb.StartUnordedList();

            Action a = (() => hb.Output());
            a.Should().Throw<InvalidProgramException>();
        }
    }
}
