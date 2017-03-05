using Xunit;
using Blogifier.Core;

namespace Blogifier.UnitTests
{
    public class UnitTests
    {
        [Fact]
        public void TestOne()
        {
            var c1 = new Core.Class1();
            Assert.True(c1.GetCoreTest() == "Test from core");
        }
    }
}