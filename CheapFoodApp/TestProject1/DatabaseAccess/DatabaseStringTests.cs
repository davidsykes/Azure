using DatabaseAccessInterfaces;
using FluentAssertions;
using TestHelpers;

namespace TestProject1.DatabaseAccess
{
    public class DatabaseStringTests : TestBase
    {
        [Test]
        public void ADatabaseStringHasSurroundingWhitespaceRemoved()
        {
            var value = new DatabaseString("  hello  ");

            value.ToString().Should().Be("hello");
        }

        #region Support Code


        #endregion
    }
}

