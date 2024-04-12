using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CustomDateTime
{
    private readonly int year;
    private readonly int month;
    private readonly int day;

    private static readonly string dateFormat = "dd-MM-yyyy";
    private const int noYear = 0;
    private const int noMonth = 0;
    private const int noDay = 0;
    private const string noDateString = "n.d.";

    private static readonly IReadOnlyDictionary<int, int> daysInMonth = new Dictionary<int, int>
    {
        { 1, 31 },
        { 2, 29 },
        { 3, 31 },
        { 4, 30 },
        { 5, 31 },
        { 6, 30 },
        { 7, 31 },
        { 8, 31 },
        { 9, 30 },
        { 10, 31 },
        { 11, 30 },
        { 12, 31 }
    };

    private CustomDateTime(int year = noYear, int month = noMonth, int day = noDay)
    {
        this.year = year;
        this.month = month;
        this.day = day;
    }

    /// <summary>
    /// Creates a CustomDateTime object from a string based on the format dd-MM-yyyy.
    /// </summary>
    /// <param name="dateAsString"></param>
    /// <returns>A new CustomDateTime object.</returns>
    public static CustomDateTime FromString(string dateAsString)
    {
        ValidateDateTimeString(dateAsString);

        if (dateAsString.Equals(noDateString))
        {
            return new CustomDateTime();
        }
        else
        {
            return CreateCustomDateTime(dateAsString);
        }
    }

    private static void ValidateDateTimeString(string dateAsString)
    {
        if (string.IsNullOrEmpty(dateAsString))
        {
            throw new ArgumentException("Date string cannot be null or empty");
        }
        else if (dateAsString.Length > dateFormat.Length)
        {
            throw new ArgumentException("Too long date string given");
        }
        else if (!Regex.IsMatch(dateAsString, @"^[0-9-]*$|^n\.d\.$"))
        {
            throw new ArgumentException($"Date string can only contain numbers and dashes or \"{noDateString}\"");
        } 
        else if (dateAsString.StartsWith("-") || dateAsString.EndsWith("-"))
        {
            throw new ArgumentException("Date string cannot start or end with a dash");
        }
    }

    private static CustomDateTime CreateCustomDateTime(string dateAsString)
    {
        string[] dateParts = dateAsString.Split('-');
        int year;
        int month;
        int day;

        switch (dateParts.Length)
        {
            case 1:
                year = int.Parse(dateParts[0]);
                return new CustomDateTime(year);
            case 2:
                month = int.Parse(dateParts[0]);
                year = int.Parse(dateParts[1]);
                ValidateMonth(month);
                return new CustomDateTime(year, month);
            case 3:
                day = int.Parse(dateParts[0]);
                month = int.Parse(dateParts[1]);
                year = int.Parse(dateParts[2]);
                ValidateDayAndMonth(day, month);
                return new CustomDateTime(year, month, day);
            default:
                throw new ArgumentException("Invalid date string format");
        }
    }

    private static void ValidateMonth(int month)
    {
        if (month < 1 || month > 12)
        {
            throw new ArgumentException("Invalid month");
        }
    }

    private static void ValidateDayAndMonth(int day, int month)
    {
        ValidateMonth(month);
        int dayLimit = daysInMonth[month];
        if (day < 1 || day > dayLimit)
        {
            throw new ArgumentException("Invalid day");
        }
    }

    public override string ToString()
    {
        if (year == noYear && month == noMonth && day == noDay)
        {
            return noDateString;
        } 
        else if (month == noMonth && day == noDay)
        {
            return year.ToString();
        } 
        else if (day == noDay)
        {
            return $"{month.ToString("00")}-{year}";
        } 
        else
        {
            return $"{day.ToString("00")}-{month.ToString("00")}-{year}";
        }
    }
}
