using System;

namespace CongestionTaxCalculator;

public class Calculator
{
    public static readonly int MAX_DAILY_FEE = 60;
    private static readonly int MONTH_JULY = 7;

    public static void PrintTotalAmount(string tollStationPasses)
    {
        string[] splittedPasses = tollStationPasses.Split(", ");
        DateTime[] parsedTollStationPasses = new DateTime[splittedPasses.Length];
        for (int i = 0; i < parsedTollStationPasses.Length; i++)
        {
            try
            {
                parsedTollStationPasses[i] = DateTime.Parse(splittedPasses[i]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        int totalFee = 0;

        foreach (var tollStationPass in parsedTollStationPasses)
        {
            if(tollStationPass.DayOfWeek != DayOfWeek.Saturday && tollStationPass.DayOfWeek != DayOfWeek.Sunday && tollStationPass.Month != MONTH_JULY)
            {
                totalFee += GetSinglePassFee(tollStationPass);
            }
        }

        var totalDailyFee = Math.Min(totalFee, MAX_DAILY_FEE);

        Console.WriteLine($"The total fee is {totalDailyFee} kr");
    }

    private static int GetSinglePassFee(DateTime parsedTollStationPass)
    {
        switch (parsedTollStationPass.Hour)
        {
            case 6:
                return parsedTollStationPass.Minute <= 29 ? 8 : 13;
            case 7:
                return 18;
            case 8:
                return parsedTollStationPass.Minute <= 29 ? 13 : 8;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
                return 8;
            case 15:
                return parsedTollStationPass.Minute <= 29 ? 13 : 18;
            case 16:
                return 18;
            case 17:
                return 13;
            case 18:
                return parsedTollStationPass.Minute <= 29 ? 8 : 0;
            default:
                return 0;
        }
    }
}
