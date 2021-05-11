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
      var years = Math.Floor(ticks / ticksPerYear);
      var remainingAfterYears = ticks - (years * ticksPerYear);
      var months = Math.Floor(remainingAfterYears / ticksPerMonth);
      var remainingAfterMonths = remainingAfterYears - (months * ticksPerMonth);
      var days = Math.Floor(remainingAfterMonths / ticksPerDay);
      var remainingAfterDays = remainingAfterMonths - (days * ticksPerDay);
      var hours = Math.Floor(remainingAfterDays / ticksPerHour);
      var remainingAfterHours = remainingAfterDays - (hours * ticksPerHour);
      var minutes = Math.Floor(remainingAfterHours / ticksPerMinute);
      return $"{Pad(days)}/{Pad(months)}/{Pad(years)} {Pad(hours)}:{Pad(minutes)}";
    }

    public static string ToAbsString(float ticks)
    {
      var years = Math.Floor(ticks / ticksPerYear);
      var remainingAfterYears = ticks - (years * ticksPerYear);
      var months = Math.Floor(remainingAfterYears / ticksPerMonth);
      var remainingAfterMonths = remainingAfterYears - (months * ticksPerMonth);
      var days = Math.Floor(remainingAfterMonths / ticksPerDay);
      var remainingAfterDays = remainingAfterMonths - (days * ticksPerDay);
      var hours = Math.Floor(remainingAfterDays / ticksPerHour);
      var remainingAfterHours = remainingAfterDays - (hours * ticksPerHour);
      var minutes = Math.Floor(remainingAfterHours / ticksPerMinute);
      return $"{years} years, {months} months, {days} days, {hours} hours, {minutes} minutes";
    }

    private static string Pad(double value)
    {
      return value.ToString().PadLeft(2, '0');
    }

    public static float From(int years, int months, int days, int hours, int minutes)
    {
      return years * ticksPerYear + months * ticksPerMonth + days * ticksPerDay + hours * ticksPerHour + minutes * ticksPerMinute;
    }
  }
}