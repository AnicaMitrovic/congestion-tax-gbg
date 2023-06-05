namespace CongestionTaxCalculator;

public class Calculator
{
    public static readonly int MAX_DAILY_CHARGE = 60;

    public static void PrintTotalAmount(string tollStationPasses)
    {
        var tollStationPassesDateTimes = tollStationPasses.Split(',')
            .Select(pass => DateTime.Parse(pass.Trim()))
            .OrderBy(pass => pass)
            .ToList();

        var totalFee = GetTotalFee(tollStationPassesDateTimes);

        Console.WriteLine($"The total fee is {totalFee} kr");
    }

    private static int GetTotalFee(List<DateTime> passes)
    {
        var totalFee = 0;

        var hourlyPassGroups = GroupPassesPerHour(passes);

        foreach (var hourlyPassGroup in hourlyPassGroups)
        {
            var highestHourlyFee = GetHighestFeePerHour(hourlyPassGroup);
            totalFee += highestHourlyFee;
        }

        return Math.Min(totalFee, MAX_DAILY_CHARGE);
    }

    private static int GetHighestFeePerHour(List<DateTime> passes)
    {
        var highestFee = 0;

        foreach (var pass in passes)
        {
            var fee = GetPassFee(pass);
            highestFee = Math.Max(highestFee, fee);
        }

        return highestFee;
    }

    private static List<List<DateTime>> GroupPassesPerHour(List<DateTime> passes)
    {
        var groups = new List<List<DateTime>>();
        var currentGroup = new List<DateTime> { passes[0] };
        var previousPass = passes[0];

        for (int i = 1; i < passes.Count; i++)
        {
            var pass = passes[i];

            var timeDiff = pass - previousPass;

            if (timeDiff.TotalMinutes <= 60)
            {
                currentGroup.Add(pass);
            }
            else
            {
                groups.Add(currentGroup);
                currentGroup = new List<DateTime> { pass };
            }

            previousPass = pass;
        }

        groups.Add(currentGroup);

        return groups;
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
