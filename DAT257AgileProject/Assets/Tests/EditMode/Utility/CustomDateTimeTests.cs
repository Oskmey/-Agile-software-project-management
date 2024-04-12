using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class CustomDateTimeTests
{
    // Valid date strings
    private const string ValidDateString = "01-01-1970";
    private const string ValidDateWithoutDayString = "01-1970";
    private const string ValidDateWithOnlyYearString = "1970";
    private const string NoDateGivenString = "n.d.";
    private const string ValidLeapYearString = "29-02-2020";

    // Invalid date strings
    private const string InvalidLengthDateString = "01-01-1970-01";
    private const string InvalidMonthDateString = "01-13-1970";
    private const string InvalidDayDateString = "32-01-1970";
    private const string EmptyDateString = "";
    private const string NullDateString = null;
    private const string NonNumericDateString = "01-01-1970a";
    private const string InvalidDateStringWithLeadingDash = "-01-01-1970";
    private const string InvalidDateStringWithoutDayWithLeadingDash = "-01-1970";
    private const string InvalidDateStringWithOnlyYearWithLeadingDash = "-1970";
    private const string InvalidDateStringWithTrailingDash = "01-01-1970-";
    private const string InvalidDateStringWithoutDayWithTrailingDash = "01-1970-";
    private const string InvalidDateStringWithOnlyYearWithTrailingDash = "1970-";
    private const string InvalidLeapYearString = "29-02-2021";

    [Test]
    public void CustomDateTime_GivenValidString_IsNotNull()
    {
        var customDateTime = CustomDateTime.FromString(ValidDateString);
        Assert.IsNotNull(customDateTime);
    }

    [Test]
    public void CustomDateTime_GivenValidString_CanBeConvertedBack()
    {
        var customDateTime = CustomDateTime.FromString(ValidDateString);
        Assert.AreEqual(ValidDateString, customDateTime.ToString());
    }

    [Test]
    public void CustomDateTime_GivenValidStringWithoutDay_IsNotNull()
    {
        var customDateTime = CustomDateTime.FromString(ValidDateWithoutDayString);
        Assert.IsNotNull(customDateTime);
    }

    [Test]
    public void CustomDateTime_GivenValidStringWithoutDay_CanBeConvertedBack()
    {
        var customDateTime = CustomDateTime.FromString(ValidDateWithoutDayString);
        Assert.AreEqual(ValidDateWithoutDayString, customDateTime.ToString());
    }

    [Test]
    public void CustomDateTime_GivenValidStringWithOnlyYear_IsNotNull()
    {
        var customDateTime = CustomDateTime.FromString(ValidDateWithOnlyYearString);
        Assert.IsNotNull(customDateTime);
    }

    [Test]
    public void CustomDateTime_GivenValidStringWithOnlyYear_CanBeConvertedBack()
    {
        var customDateTime = CustomDateTime.FromString(ValidDateWithOnlyYearString);
        Assert.AreEqual(ValidDateWithOnlyYearString, customDateTime.ToString());
    }

    [Test]
    public void CustomDateTime_GivenNoDateString_IsNotNull()
    {
        var customDateTime = CustomDateTime.FromString(NoDateGivenString);
        Assert.IsNotNull(customDateTime);
    }

    [Test]
    public void CustomDateTime_GivenNoDateString_CanBeConvertedBack()
    {
        var customDateTime = CustomDateTime.FromString(NoDateGivenString);
        Assert.AreEqual(NoDateGivenString, customDateTime.ToString());
    }

    [Test]
    public void CustomDateTime_GivenValidLeapYearString_IsNotNull()
    {
        var customDateTime = CustomDateTime.FromString(ValidLeapYearString);
        Assert.IsNotNull(customDateTime);
    }

    [Test]
    public void CustomDateTime_GivenValidLeapYearString_CanBeConvertedBack()
    {
        var customDateTime = CustomDateTime.FromString(ValidLeapYearString);
        Assert.AreEqual(ValidLeapYearString, customDateTime.ToString());
    }

    [Test]
    public void CustomDateTime_GivenInvalidLengthDateString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidLengthDateString));
    }

    [Test]
    public void CustomDateTime_GivenInvalidMonthDateString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidMonthDateString));
    }

    [Test]
    public void CustomDateTime_GivenInvalidDayDateString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidDayDateString));
    }

    [Test]
    public void CustomDateTime_GivenEmptyDateString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(EmptyDateString));
    }

    [Test]
    public void CustomDateTime_GivenNullDateString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(NullDateString));
    }

    [Test]
    public void CustomDateTime_GivenNonNumericDateString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(NonNumericDateString));
    }

    [Test]
    public void CustomDateTime_GivenInvalidDateStringWithLeadingDash_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidDateStringWithLeadingDash));
    }

    [Test]
    public void CustomDateTime_GivenInvalidDateStringWithoutDayWithLeadingDash_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidDateStringWithoutDayWithLeadingDash));
    }

    [Test]
    public void CustomDateTime_GivenInvalidDateStringWithOnlyYearWithLeadingDash_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidDateStringWithOnlyYearWithLeadingDash));
    }

    [Test]
    public void CustomDateTime_GivenInvalidDateStringWithTrailingDash_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidDateStringWithTrailingDash));
    }

    [Test]
    public void CustomDateTime_GivenInvalidDateStringWithoutDayWithTrailingDash_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidDateStringWithoutDayWithTrailingDash));
    }

    [Test]
    public void CustomDateTime_GivenInvalidDateStringWithOnlyYearWithTrailingDash_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidDateStringWithOnlyYearWithTrailingDash));
    }

    [Test]
    public void CustomDateTime_GivenInvalidLeapYearString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CustomDateTime.FromString(InvalidLeapYearString));
    }
} 