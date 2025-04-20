using DatabaseAccess;
using DatabaseAccessInterfaces;
using DatabaseAccessInterfaces.DatabaseObjects;
using FluentAssertions;
using Moq;
using TestHelpers;

#nullable disable
namespace TestProject1.DatabaseAccess
{
    public class FoodBeingEditedTests : TestBase
    {
        Mock<IDatabaseAccess> _mockDatabaseAccess;
        FoodItem _foodItem;

        [Test]
        public void TheFoodBeingEditedRetrievesTheNameOfTheFool()
        {
            var fbe = new FoodBeingEdited(5, _mockDatabaseAccess.Object);

            fbe.Name.Should().Be("Food Name");
        }

        #region Support Code

        protected override void SetUpData()
        {
            base.SetUpData();
            _foodItem = new FoodItem
            {
                Id = 5,
                Name = "Food Name"
            };
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();
            _mockDatabaseAccess = new Mock<IDatabaseAccess>();
        }

        protected override void SetUpExpectations()
        {
            base.SetUpExpectations();
            _mockDatabaseAccess.Setup(m => m.GetFoodItem(5)).Returns(_foodItem);
        }

        #endregion
    }
}

