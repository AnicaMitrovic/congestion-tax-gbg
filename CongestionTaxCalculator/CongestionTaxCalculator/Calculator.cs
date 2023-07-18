using System;

namespace CongestionTaxCalculator;

public class Calculator
{
    public static void PrintTotalAmount(string tollStationPasses)
    {
        string[] splittedPasses = tollStationPasses.Split(", ");
        DateTime[] parsedTollStationPasses = new DateTime[splittedPasses.Length];
        for (int i = 0; i < parsedTollStationPasses.Length; i++)
        {
            parsedTollStationPasses[i] = DateTime.Parse(splittedPasses[i]);
        }

        int totalFee = 0;

        foreach (var tollStationPass in parsedTollStationPasses)
        {
            totalFee += GetSinglePassFee(tollStationPass);
        }

        Console.WriteLine($"The total fee is {totalFee} kr");
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
