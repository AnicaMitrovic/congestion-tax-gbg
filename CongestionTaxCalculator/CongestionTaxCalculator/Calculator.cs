using System.Globalization;

namespace CongestionTaxCalculator;

public class Calculator
{
    public static void PrintTotalAmount(string tollStationPasses)
    {
        var dateTime = DateTime.Parse(tollStationPasses);
        var totalFee = GetPassFee(dateTime);        

        Console.WriteLine($"The total fee is {totalFee} kr");
    }

    private static object GetPassFee(DateTime dateTime)
    {
        switch (dateTime.Hour)
        {
            case 6:
                return dateTime.Minute <= 29 ? 8 : 13;
            case 7:
                return 18;
            case 8:
                return dateTime.Minute <= 29 ? 13 : 8;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
                return 8;
            case 15:
                return dateTime.Minute <= 29 ? 13 : 18;
            case 16:
                return 18;
            case 17:
                return 13;
            case 18:
                return dateTime.Minute <= 29 ? 8 : 0;
            default:
                return 0;
        }
    }
}
