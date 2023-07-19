using CongestionTaxCalculator;

namespace CalculatorTests
{
    [TestClass]
    public class PrintTotalAmountTests
    {
        public static IEnumerable<object[]> TollStationSinglePasses => new[]
        {
            new object[] { "2023-01-18 06:00", 8 },
            new object[] { "2023-01-18 06:29", 8 },
            new object[] { "2023-01-18 06:30", 13 },
            new object[] { "2023-01-18 06:59", 13 },
            new object[] { "2023-01-18 07:00", 18 },
            new object[] { "2023-01-18 07:59", 18 },
            new object[] { "2023-01-18 08:00", 13 },
            new object[] { "2023-01-18 08:29", 13 },
            new object[] { "2023-01-18 08:30", 8 },
            new object[] { "2023-01-18 15:29", 13 },
            new object[] { "2023-01-18 15:30", 18 },
            new object[] { "2023-01-18 16:00", 18 }
        };

        public static IEnumerable<object[]> TollStationMultiplePasses => new[]
        {
            new object[] { "2023-01-18 06:00, 2023-01-18 08:30", 16 }, //  8,  8
            new object[] { "2023-01-18 06:00, 2023-01-18 08:00", 21 }, //  8, 13
            new object[] { "2023-01-18 06:00, 2023-01-18 07:59", 26 }, //  8, 18
            new object[] { "2023-01-18 06:00, 2023-01-18 18:30", 8 },  //  8,  0
            new object[] { "2023-01-18 06:30, 2023-01-18 08:00", 26 }, // 13, 13
            new object[] { "2023-01-18 06:30, 2023-01-18 07:59", 31 }, // 13, 18
            new object[] { "2023-01-18 06:30, 2023-01-18 18:30", 13 }, // 13,  0
            new object[] { "2023-01-18 07:00, 2023-01-18 15:30", 36 }, // 18, 18
            new object[] { "2023-01-18 18:00, 2023-01-18 19:00", 8 },  //  8,  0
            new object[] { "2023-01-18 06:00, 2023-01-18 08:30, 2023-01-18 10:00", 24 }, //  8,  8,  8
            new object[] { "2023-01-18 06:00, 2023-01-18 08:30, 2023-01-18 15:00", 29 }, //  8,  8, 13
            new object[] { "2023-01-18 06:00, 2023-01-18 08:30, 2023-01-18 15:30", 34 }, //  8,  8, 18
            new object[] { "2023-01-18 06:00, 2023-01-18 08:30, 2023-01-18 18:30", 16 }, //  8,  8,  0
            new object[] { "2023-01-18 06:30, 2023-01-18 15:00, 2023-01-18 18:00", 34 }, // 13, 13,  8
            new object[] { "2023-01-18 06:30, 2023-01-18 15:00, 2023-01-18 17:00", 39 }, // 13, 13, 13
            new object[] { "2023-01-18 06:30, 2023-01-18 15:00, 2023-01-18 16:00", 44 }, // 13, 13, 18
            new object[] { "2023-01-18 06:30, 2023-01-18 15:00, 2023-01-18 18:30", 26 }, // 13, 13,  0
            new object[] { "2023-01-18 07:00, 2023-01-18 15:30, 2023-01-18 18:00", 44 }, // 18, 18,  8
            new object[] { "2023-01-18 07:00, 2023-01-18 15:30, 2023-01-18 17:00", 49 }, // 18, 18, 13
            new object[] { "2023-01-18 07:00, 2023-01-18 15:30, 2023-01-18 16:59", 54 }, // 18, 18, 18
            new object[] { "2023-01-18 07:00, 2023-01-18 15:30, 2023-01-18 18:30", 36 }, // 18, 18,  0
            new object[] { "2023-01-18 18:30, 2023-01-18 19:00, 2023-01-18 20:00", 0 },  //  0,  0,  0
        };

        public static IEnumerable<object[]> TollStationNoChargedPasses => new[]
       {
            new object[] { "2023-06-03 06:00", 0 },
            new object[] { "2023-06-04 13:29", 0 },
            new object[] { "2023-06-04 06:30", 0 },
            new object[] { "2023-07-01 06:59", 0 },
            new object[] { "2023-07-18 18:30, 2023-07-18 19:00, 2023-07-18 20:00", 0 }
        };


        [TestMethod]
        [DynamicData(nameof(TollStationSinglePasses))]
        [DynamicData(nameof(TollStationNoChargedPasses))]
        public void PrintTotalAmount_GivenSinglePass_PrintsCorrectAmount(string passTime, int fee)
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            Calculator.PrintTotalAmount(passTime);
            var actual = stringWriter.ToString().Trim();

            Assert.AreEqual($"The total fee is {fee} kr", actual);
        }

        // Add more dynamic data
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PrintTotalAmount_GivenInvalidDate_ThrowsFormatException()
        {
            Calculator.PrintTotalAmount("2023-48-01");
        }

        [TestMethod]
        [DynamicData(nameof(TollStationMultiplePasses))]
        public void PrintTotalAmount_GivenMultiplePasses_PrintsCorrectAmount(string passTimes, int fee)
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            Calculator.PrintTotalAmount(passTimes);
            var actual = stringWriter.ToString().Trim();

            Assert.AreEqual($"The total fee is {fee} kr", actual);
        }

        [TestMethod]
        public void PrintTotalAmount_GivenTotalFeeOver60_Prints60()
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            Calculator.PrintTotalAmount("2023-05-31 06:00, 2023-05-31 06:30, 2023-05-31 07:20, 2023-05-31 08:00, 2023-05-31 08:15, 2023-05-31 08:20");
            var actual = stringWriter.ToString().Trim();

            Assert.AreEqual("The total fee is 60 kr", actual);
        }

        [TestMethod]
        public void PrintTotalAmount_GivenTPassesInOneHour_PrintsHighestFee()
        {
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            Calculator.PrintTotalAmount("2023-05-31 06:20, 2023-05-31 06:45, 2023-05-31 07:10");
            var actual = stringWriter.ToString().Trim();

            Assert.AreEqual("The total fee is 18 kr", actual);
        }
    }
}