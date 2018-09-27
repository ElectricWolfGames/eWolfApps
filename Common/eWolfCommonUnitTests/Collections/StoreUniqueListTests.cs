using eWolfCommon.Collections;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfCommonUnitTests.Collections
{
    [TestFixture]
    public class StoreUniqueListTests
    {
        [Test]
        public void ShouldAddTestToTopOfList()
        {
            StoreUniqueList<string> sul = new StoreUniqueList<string>(5);
            sul.Add("Item A");
            sul.Add("Item B");

            sul.Items.Should().HaveCount(2);
            string test = sul.Items[0];
            test.Should().Be("Item B");
        }

        [Test]
        public void ShouldNotAddSameItemTwice()
        {
            StoreUniqueList<string> sul = new StoreUniqueList<string>(5);
            sul.Add("Item A");
            sul.Add("Item A");

            sul.Items.Should().HaveCount(1);
        }

        [Test]
        public void ShouldNotStoreMoreThanTheMaximumItems()
        {
            int total = 5;
            StoreUniqueList<string> sul = new StoreUniqueList<string>(total);

            for (int i = 0; i < total + 1; i++)
            {
                sul.Add("Items " + i);
            }

            sul.Items.Should().HaveCount(total);
        }

        [Test]
        public void ShouldBeAbleToAddInts()
        {
            StoreUniqueList<int> listOfInts = new StoreUniqueList<int>(5);
            listOfInts.Add(5);
            listOfInts.Add(10);

            listOfInts.Items.Should().HaveCount(2);
        }

        [Test]
        public void ShouldNotHaveMoreThenMaxItems()
        {
            StoreUniqueList<int> listOfInts = new StoreUniqueList<int>(5);
            listOfInts.Add(5);
            listOfInts.Add(6);
            listOfInts.Add(7);
            listOfInts.Add(8);
            listOfInts.Add(9);
            listOfInts.Add(10);
            listOfInts.Add(11);
            listOfInts.Add(12);

            listOfInts.Items.Should().HaveCount(5);
        }
    }
}
