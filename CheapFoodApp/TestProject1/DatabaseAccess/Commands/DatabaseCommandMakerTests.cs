using DatabaseAccess;
using DatabaseAccess.Commands;
using DatabaseAccessInterfaces.DatabaseTableValues;
using FluentAssertions;
using TestHelpers;

#nullable disable
namespace TestProject1.DatabaseAccess.Commands
{
    public class DatabaseCommandMakerTests : TestBase
    {
        IDatabaseCommandMaker _maker;

        [Test]
        public void ASimpleInsertCommandCanBeCreated()
        {
            var command = _maker.MakeInsertCommand("TableName",[
                new DatabaseTableIntValue("Int", 42),
                new DatabaseTableDoubleValue("Double", 4.2),
                new DatabaseTableStringValue("String", "value")
                ]);

            command.Query.Should().Be("INSERT INTO TableName(Int,Double,String) VALUES(@Int,@Double,@String)");
        }

        #region Support Code

        protected override void SetUpObjectUnderTest()
        {
            base.SetUpObjectUnderTest();
            _maker = new DatabaseCommandMaker();
        }

        #endregion
    }
}
