using NUnit.Framework;

namespace EcommFunctions.ProductFunctionsTests
{
    [TestFixture]
    public class ProductFunctionsTests
    {
        [Test]
        public void GetProductTest()
        {

            var getProd = new ProductFunctions.ProductFunctions();

            var method = getProd.Test();

            Assert.NotZero(method);
        }
    }
}
