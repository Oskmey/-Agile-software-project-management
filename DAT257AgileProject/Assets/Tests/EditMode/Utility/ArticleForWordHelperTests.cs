using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class ArticleForWordHelperTests
{
    private const string A = "a";
    private const string AN = "an";

    private const string validAWord = "boat";
    private const string validAnWord = "apple";
    private const string validAWordCaps = "Boat";
    private const string validAnWordCaps = "Apple";
    private const string validAWordAllCaps = "BOAT";
    private const string validAnWordAllCaps = "APPLE";

    private const string invalidNumbersOnlyWord = "1234";
    private const string invalidEmptyWord = "";
    private const string invalidNullWord = null;
    private const string invalidWhitespaceOnlyWord = " ";

    [Test]
    public void GetArticle_ValidAWord_ReturnsA()
    {
        string article = ArticleForWordHelper.GetArticle(validAWord);
        Assert.AreEqual(A, article);
    }

    [Test]
    public void GetArticle_ValidAnWord_ReturnsAn()
    {
        string article = ArticleForWordHelper.GetArticle(validAnWord);
        Assert.AreEqual(AN, article);
    }

    [Test]
    public void GetArticle_ValidAWordCaps_ReturnsA()
    {
        string article = ArticleForWordHelper.GetArticle(validAWordCaps);
        Assert.AreEqual(A, article);
    }

    [Test]
    public void GetArticle_ValidAnWordCaps_ReturnsAn()
    {
        string article = ArticleForWordHelper.GetArticle(validAnWordCaps);
        Assert.AreEqual(AN, article);
    }

    [Test]
    public void GetArticle_ValidAWordAllCaps_ReturnsA()
    {
        string article = ArticleForWordHelper.GetArticle(validAWordAllCaps);
        Assert.AreEqual(A, article);
    }

    [Test]
    public void GetArticle_ValidAnWordAllCaps_ReturnsAn()
    {
        string article = ArticleForWordHelper.GetArticle(validAnWordAllCaps);
        Assert.AreEqual(AN, article);
    }

    [Test]
    public void GetArticle_InvalidNumbersOnlyWord_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ArticleForWordHelper.GetArticle(invalidNumbersOnlyWord));
    }

    [Test]
    public void GetArticle_InvalidEmptyWord_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ArticleForWordHelper.GetArticle(invalidEmptyWord));
    }

    [Test]
    public void GetArticle_InvalidNullWord_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ArticleForWordHelper.GetArticle(invalidNullWord));
    }

    [Test]
    public void GetArticle_InvalidWhitespaceOnlyWord_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ArticleForWordHelper.GetArticle(invalidWhitespaceOnlyWord));
    }
}
