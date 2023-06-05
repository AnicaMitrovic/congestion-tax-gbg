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
        if (IsPeriodFreeOfCharge(dateTime)) return 0;

        return dateTime.Hour switch
        {
            6 => dateTime.Minute <= 29 ? 8 : 13,
            7 => 18,
            8 => dateTime.Minute <= 29 ? 13 : 8,
            9 or 10 or 11 or 12 or 13 or 14 => 8,
            15 => dateTime.Minute <= 29 ? 13 : 18,
            16 => 18,
            17 => 13,
            18 => dateTime.Minute <= 29 ? 8 : 0,
            _ => 0,
        };
    }

    private static bool IsPeriodFreeOfCharge(DateTime dateTime) =>
        dateTime.DayOfWeek == DayOfWeek.Saturday ||
        dateTime.DayOfWeek == DayOfWeek.Sunday ||
        dateTime.Month == 7;
}
