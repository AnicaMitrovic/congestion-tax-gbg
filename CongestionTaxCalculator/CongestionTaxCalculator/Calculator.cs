namespace CongestionTaxCalculator;

public enum Fee
{
    Low = 8,
    Medium = 13,
    High = 18,
    Free = 0
}

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

    private static int GetPassFee(DateTime pass)
    {
        if (IsPeriodFreeOfCharge(pass)) return (int)Fee.Free;

        return pass.Hour switch
        {
            6 => pass.Minute <= 29 ? (int)Fee.Low : (int)Fee.Medium,
            7 => (int)Fee.High,
            8 => pass.Minute <= 29 ? (int)Fee.Medium : (int)Fee.Low,
            9 or 10 or 11 or 12 or 13 or 14 => (int)Fee.Low,
            15 => pass.Minute <= 29 ? (int)Fee.Medium : (int)Fee.High,
            16 => (int)Fee.High,
            17 => (int)Fee.Medium,
            18 => pass.Minute <= 29 ? (int)Fee.Low : (int)Fee.Free,
            _ => (int)Fee.Free,
        };
    }

    private static bool IsPeriodFreeOfCharge(DateTime dateTime) =>
        dateTime.DayOfWeek == DayOfWeek.Saturday ||
        dateTime.DayOfWeek == DayOfWeek.Sunday ||
        dateTime.Month == 7;
}
