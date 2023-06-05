using System.Globalization;
using System.Xml.Linq;

namespace CongestionTaxCalculator;

public class Calculator
{
    public static readonly int MAX_DAILY_CHARGE = 60;

    public static void PrintTotalAmount(string tollStationPasses)
    {
        var tollStationPassesDateTimes = ParseInputData(tollStationPasses);

        var totalFee = GetTotalFee(tollStationPassesDateTimes);

        Console.WriteLine($"The total fee is {totalFee} kr");
    }

    private static DateTime[] ParseInputData(string tollStationPasses)
    {
        string[] passes = tollStationPasses.Split(", ");
        DateTime[] tollStationPassesDateTimes = new DateTime[passes.Length];
        for (int i = 0; i < tollStationPassesDateTimes.Length; i++)
        {
            tollStationPassesDateTimes[i] = DateTime.Parse(passes[i]);
        }

        return tollStationPassesDateTimes;
    }

    private static int GetTotalFee(DateTime[] tollStationPassesDateTimes)
    {
        var totalFee = 0;
        foreach (var tollStationPass in tollStationPassesDateTimes)
        {
            totalFee += GetPassFee(tollStationPass);
        }

        return Math.Min(totalFee, MAX_DAILY_CHARGE);
    }

    private static int GetPassFee(DateTime dateTime)
    {
        if (dateTime.DayOfWeek == DayOfWeek.Saturday || 
            dateTime.DayOfWeek == DayOfWeek.Sunday)
        {
            return 0;
        }

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
