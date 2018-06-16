using NUnit.Framework;
using ProductFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommFunctions.ProductFunctionsTests
{
    [TestFixture]
    public class ProductFunctionsTests
    {
        [Test]
        public void GetProductTest()
        {

            ProductFunctions.ProductFunctions getProd = new ProductFunctions.ProductFunctions();

            int method = getProd.Test();

            Assert.NotZero(method);
        }
    }
}
