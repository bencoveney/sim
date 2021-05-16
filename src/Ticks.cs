using System;

namespace Sim
{
  class Ticks
  {
    private static int ticksPerMinute = 1;
    private static int ticksPerHour = 60 * ticksPerMinute;
    private static int ticksPerDay = 24 * ticksPerHour;
    private static int ticksPerMonth = 30 * ticksPerDay;
    private static int ticksPerYear = 12 * ticksPerMonth;

    public static string ToDateString(float ticks)
    {
      var parts = ToParts(ticks);
      return $"{Pad(parts.Days)}/{Pad(parts.Months)}/{Pad(parts.Years)} {Pad(parts.Hours)}:{Pad(parts.Minutes)}";
    }

    public static string ToAbsString(float ticks)
    {
      var parts = ToParts(ticks);
      return $"{parts.Years} years, {parts.Months} months, {parts.Days} days, {parts.Hours} hours, {parts.Minutes} minutes";
    }

    public static TickParts ToParts(float ticks)
    {
      var years = NumberOfYears(ticks);
      var remainingAfterYears = ticks - (years * ticksPerYear);
      var months = NumberOfYears(remainingAfterYears);
      var remainingAfterMonths = remainingAfterYears - (months * ticksPerMonth);
      var days = NumberOfDays(remainingAfterMonths);
      var remainingAfterDays = remainingAfterMonths - (days * ticksPerDay);
      var hours = NumberOfHours(remainingAfterDays);
      var remainingAfterHours = remainingAfterDays - (hours * ticksPerHour);
      var minutes = Math.Floor(remainingAfterHours);

      return new TickParts()
      {
        Years = (int)years,
        Months = (int)months,
        Days = (int)days,
        Hours = (int)hours,
        Minutes = (int)minutes,
      };
    }

    public static int NumberOfYears(float ticks)
    {
      return (int)Math.Floor(ticks / ticksPerYear);
    }

    public static int NumberOfMonths(float ticks)
    {
      return (int)Math.Floor(ticks / ticksPerMonth);
    }

    public static int NumberOfDays(float ticks)
    {
      return (int)Math.Floor(ticks / ticksPerDay);
    }

    public static int NumberOfHours(float ticks)
    {
      return (int)Math.Floor(ticks / ticksPerHour);
    }

    public static int NumberOfMinutes(float ticks)
    {
      return (int)Math.Floor(ticks / ticksPerMinute);
    }

    private static string Pad(double value)
    {
      return value.ToString().PadLeft(2, '0');
    }

    public static float From(int years, int months, int days, int hours, int minutes)
    {
      return years * ticksPerYear + months * ticksPerMonth + days * ticksPerDay + hours * ticksPerHour + minutes * ticksPerMinute;
    }

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