using System;
using NUnit.Framework;
using Increment;
using FluentAssertions;

namespace IncrementUnitTests
{
    public class IncrementTests
    {
        [Test]
        public void ShouldIncrementNumbersReturnSameTextWhenNoKeysFound()
        {
            string text = IncrementNumbers.Process("KG", "Text\nOther");
            text.Should().Be("Text\nOther");
        }

        [Test]
        public void ShouldIncrementNumbersReturnUpdateAllNumber()
        {
            string text = IncrementNumbers.Process("KG", "Text\nKG01Other\nKG01Again");
            text.Should().Be("Text\nKG02Other\nKG02Again");
        }

        [Test]
        public void ShouldIncrementNumbersReturnUpdateNumber()
        {
            string text = IncrementNumbers.Process("KG", "Text\nKG01Other");
            text.Should().Be("Text\nKG02Other");
        }
    }
}
