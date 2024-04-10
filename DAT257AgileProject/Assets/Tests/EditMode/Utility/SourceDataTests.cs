using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class SourceDataTests
{
    private static readonly SourceData[] sources = Resources.LoadAll<SourceData>("ScriptableObjects");

    public static IEnumerable<TestCaseData> SourceDataTestCases
    {
        get
        {
            foreach (var source in sources)
            {
                yield return new TestCaseData(source);
            }
        }
    }

    [Test]
    public void Sources_AreNotNull()
    {
        Assert.IsNotNull(sources);
    }

    [Test]
    public void Sources_AreNotEmpty()
    {
        Assert.IsNotEmpty(sources);
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_HasSourceName(SourceData source)
    {
        Assert.IsNotNull(source.SourceName);
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_HasNonEmptySourceName(SourceData source)
    {
        Assert.IsNotEmpty(source.SourceName);
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_HasDate(SourceData source)
    {
        Assert.IsNotNull(source.Date);
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_HasTitle(SourceData source)
    {
        Assert.IsNotNull(source.Title);
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_HasNonEmptyTitle(SourceData source)
    {
        Assert.IsNotEmpty(source.Title);
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_hasWebsite(SourceData source)
    {
        Assert.IsNotNull(source.Website);
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_hasNonEmptyWebsite(SourceData source)
    {
        Assert.IsNotEmpty(source.Website);
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_hasLink(SourceData source)
    {
        Assert.IsNotNull(source.Link);
    }

    [Test]
    public void EachSource_hasUniqueLink()
    {
        var links = new HashSet<string>();
        foreach (var source in sources)
        {
            Assert.IsTrue(links.Add(source.Link));
        }
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_hasValidLink(SourceData source)
    {
        bool uriCreationResult = Uri.TryCreate(source.Link, UriKind.Absolute, out Uri uriResult);
        bool isHttpUrl = uriResult.Scheme == Uri.UriSchemeHttp;
        bool isHttpsUrl = uriResult.Scheme == Uri.UriSchemeHttps;
        bool isValid = uriCreationResult && (isHttpUrl || isHttpsUrl);

        Assert.IsTrue(isValid, $"Invalid URL: {source.Link}");
    }

    [Test, TestCaseSource(nameof(SourceDataTestCases))]
    public void EachSource_hasRetrievalDate(SourceData source)
    {
        Assert.IsNotNull(source.RetrievalDate);
    }
}
