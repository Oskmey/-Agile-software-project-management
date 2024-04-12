using System;
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

        if (dateAsString.Equals("n.d."))
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
        else if (!Regex.IsMatch(dateAsString, @"^[0-9-]*$"))
        {
            throw new ArgumentException("Date string can only contain numbers and dashes");
        }
    }

    private static CustomDateTime CreateCustomDateTime(string dateAsString)
    {
        string[] dateParts = dateAsString.Split('-');
        switch (dateParts.Length)
        {
            case 1:
                return new CustomDateTime(int.Parse(dateParts[0]));
            case 2:
                return new CustomDateTime(int.Parse(dateParts[1]), int.Parse(dateParts[0]));
            case 3:
                return new CustomDateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]));
            default:
                throw new ArgumentException("Invalid date string format");
        }
    }

    public override string ToString()
    {
        if (year == noYear && month == noMonth && day == noDay)
        {
            return "n.d.";
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
