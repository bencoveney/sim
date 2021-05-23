namespace Sim.Runner
{
  class Ticks
  {
    private static int ticksPerMinute = 1;
    private static int ticksPerHour = 60 * ticksPerMinute;
    private static int ticksPerDay = 24 * ticksPerHour;
    private static int ticksPerMonth = 30 * ticksPerDay;
    private static int ticksPerYear = 12 * ticksPerMonth;

    public static string ToDateString(int ticks)
    {
      var parts = ToParts(ticks);
      return $"{Pad(parts.Days)}/{Pad(parts.Months)}/{Pad(parts.Years)} {Pad(parts.Hours)}:{Pad(parts.Minutes)}";
    }

    public static string ToAbsString(int ticks)
    {
      var parts = ToParts(ticks);
      return $"{parts.Years} years, {parts.Months} months, {parts.Days} days, {parts.Hours} hours, {parts.Minutes} minutes";
    }

    public static TickParts ToParts(int ticks)
    {
      var years = NumberOfYears(ticks);
      var remainingAfterYears = ticks - (years * ticksPerYear);
      var months = NumberOfMonths(remainingAfterYears);
      var remainingAfterMonths = remainingAfterYears - (months * ticksPerMonth);
      var days = NumberOfDays(remainingAfterMonths);
      var remainingAfterDays = remainingAfterMonths - (days * ticksPerDay);
      var hours = NumberOfHours(remainingAfterDays);
      var minutes = remainingAfterDays - (hours * ticksPerHour);

      return new TickParts()
      {
        Years = years,
        Months = months,
        Days = days,
        Hours = hours,
        Minutes = minutes,
      };
    }

    public static int NumberOfYears(int ticks)
    {
      // Rely on int division to do a floor operation.
      return ticks / ticksPerYear;
    }

    public static int NumberOfMonths(int ticks)
    {
      // Rely on int division to do a floor operation.
      return ticks / ticksPerMonth;
    }

    public static int NumberOfDays(int ticks)
    {
      // Rely on int division to do a floor operation.
      return ticks / ticksPerDay;
    }

    public static int NumberOfHours(int ticks)
    {
      // Rely on int division to do a floor operation.
      return ticks / ticksPerHour;
    }

    public static int NumberOfMinutes(int ticks)
    {
      // Rely on int division to do a floor operation.
      return ticks / ticksPerMinute;
    }

    private static string Pad(double value)
    {
      return value.ToString().PadLeft(2, '0');
    }

    public static int From(int years, int months, int days, int hours, int minutes)
    {
      return years * ticksPerYear + months * ticksPerMonth + days * ticksPerDay + hours * ticksPerHour + minutes * ticksPerMinute;
    }

    public static int PerYear { get => ticksPerYear; }
    public static int PerMonth { get => ticksPerMonth; }
    public static int PerDay { get => ticksPerDay; }
    public static int PerHour { get => ticksPerHour; }
    public static int PerMinute { get => ticksPerMinute; }

    public class TickParts
    {
      public int Years;
      public int Months;
      public int Days;
      public int Hours;
      public int Minutes;
    }
  }
}