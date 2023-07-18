using System;

namespace CongestionTaxCalculator;

public class Calculator
{
    public static void PrintTotalAmount(string tollStationPasses)
    {
        var parsedTollStationPass = DateTime.Parse(tollStationPasses);

        int totalFee;

        switch (parsedTollStationPass.Hour)
        {
            case 6:
                totalFee = parsedTollStationPass.Minute <= 29 ? 8 : 13;
                break;
            case 7:
                totalFee = 18;
                break;
            case 8:
                totalFee = parsedTollStationPass.Minute <= 29 ? 13 : 8;
                break;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
                totalFee = 8;
                break;
            case 15:
                totalFee = parsedTollStationPass.Minute <= 29 ? 13 : 18;
                break;
            case 16:
                totalFee = 18;
                break;
            case 17:
                totalFee = 13;
                break;
            case 18:
                totalFee = parsedTollStationPass.Minute <= 29 ? 8 : 0;
                break;
            default:
                totalFee = 0;
                break;
        }

        Console.WriteLine($"The total fee is {totalFee} kr");
    }
}
