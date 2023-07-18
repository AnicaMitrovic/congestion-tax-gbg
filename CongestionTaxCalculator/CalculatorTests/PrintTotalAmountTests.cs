using CongestionTaxCalculator;

namespace CalculatorTests
{
    [TestClass]
    public class PrintTotalAmountTests
    {
        [TestMethod]
        public void PrintTotalAmount_PrintsCorrectAmount()
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            Calculator.PrintTotalAmount("2023-05-31 17:45");
            var actual = stringWriter.ToString().Trim();

            Assert.AreEqual("The total fee is 13 kr", actual);
        }
    }
}