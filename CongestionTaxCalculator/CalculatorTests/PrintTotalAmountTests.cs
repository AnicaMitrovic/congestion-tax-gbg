using CongestionTaxCalculator;

namespace CalculatorTests
{
    [TestClass]
    public class PrintTotalAmountTests
    {
        public static IEnumerable<object[]> SinglePassInterval8 => new[]
        {
            new object[] { "2023-05-31 06:00" },
            new object[] { "2023-05-31 06:29" },
            new object[] { "2023-05-31 08:30" },
            new object[] { "2023-05-31 14:59" },
            new object[] { "2023-05-31 18:00" },
            new object[] { "2023-05-31 18:29" },
        };

        public static IEnumerable<object[]> SinglePassInterval13 => new[]
        {
            new object[] { "2023-05-31 06:30" },
            new object[] { "2023-05-31 06:59" },
            new object[] { "2023-05-31 08:00" },
            new object[] { "2023-05-31 08:29" },
            new object[] { "2023-05-31 15:00" },
            new object[] { "2023-05-31 15:29" },
            new object[] { "2023-05-31 17:00" },
            new object[] { "2023-05-31 17:59" },
        };

        public static IEnumerable<object[]> SinglePassInterval18 => new[]
        {
            new object[] { "2023-05-31 07:00" },
            new object[] { "2023-05-31 07:59" },
            new object[] { "2023-05-31 15:30" },
            new object[] { "2023-05-31 16:59" },
        };

        public static IEnumerable<object[]> SinglePassIntervalFree => new[]
        {
            new object[] { "2023-05-31 18:30" },
            new object[] { "2023-05-31 05:59" },
        };

        public static IEnumerable<object[]> SinglePassSaturday => new[]
        {
            new object[] { "2023-06-03 05:00" },
            new object[] { "2023-05-06 12:00" },
            new object[] { "2023-07-01 17:00" },
        };

        public static IEnumerable<object[]> SinglePassSunday => new[]
{
            new object[] { "2023-06-04 05:00" },
            new object[] { "2023-05-07 12:00" },
            new object[] { "2023-07-02 17:00" },
        };

        [TestMethod]
        [DynamicData(nameof(SinglePassInterval8))]
        public void GivenSinglePassInInterval8_Prints8(string pass)
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            var expected = "The total fee is 8 kr";

            Calculator.PrintTotalAmount(pass);
            var actuall = stringWriter.ToString().Trim();

            Assert.AreEqual(expected, actuall);
        }

        [TestMethod]
        [DynamicData(nameof(SinglePassInterval13))]
        public void GivenSinglePassInInterval13_Prints13(string pass)
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            var expected = "The total fee is 13 kr";

            Calculator.PrintTotalAmount(pass);
            var actuall = stringWriter.ToString().Trim();

            Assert.AreEqual(expected, actuall);
        }

        [TestMethod]
        [DynamicData(nameof(SinglePassInterval18))]
        public void GivenSinglePassInInterval18_Prints18(string pass)
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            var expected = "The total fee is 18 kr";

            Calculator.PrintTotalAmount(pass);
            var actuall = stringWriter.ToString().Trim();

            Assert.AreEqual(expected, actuall);
        }

        [TestMethod]
        [DynamicData(nameof(SinglePassIntervalFree))]
        public void GivenSinglePassInIntervalFree_Prints0(string pass)
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            var expected = "The total fee is 0 kr";

            Calculator.PrintTotalAmount(pass);
            var actuall = stringWriter.ToString().Trim();

            Assert.AreEqual(expected, actuall);
        }

        [TestMethod]
        public void GivenMultiplePassesUpTo60_PrintsSum()
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            var expected = "The total fee is 34 kr";

            Calculator.PrintTotalAmount("2023-05-31 08:00, 2023-05-31 12:00, 2023-05-31 17:45");
            var actuall = stringWriter.ToString().Trim();

            Assert.AreEqual(expected, actuall);
        }

        [TestMethod]
        public void GivenMultiplePassesOver60_Prints60()
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            var expected = "The total fee is 60 kr";

            Calculator.PrintTotalAmount("2023-05-31 06:00, 2023-05-31 08:00, 2023-05-31 10:00, 2023-05-31 12:00, 2023-05-31 15:00, 2023-05-31 17:45");
            var actuall = stringWriter.ToString().Trim();

            Assert.AreEqual(expected, actuall);
        }

        [TestMethod]
        [DynamicData(nameof(SinglePassSaturday))]
        public void GivenSinglePassOnSaturday_Prints0(string pass)
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            var expected = "The total fee is 0 kr";

            Calculator.PrintTotalAmount(pass);
            var actuall = stringWriter.ToString().Trim();

            Assert.AreEqual(expected, actuall);
        }

        [TestMethod]
        [DynamicData(nameof(SinglePassSunday))]
        public void GivenSinglePassOnSunday_Prints0(string pass)
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            var expected = "The total fee is 0 kr";

            Calculator.PrintTotalAmount(pass);
            var actuall = stringWriter.ToString().Trim();

            Assert.AreEqual(expected, actuall);
        }
    }
}