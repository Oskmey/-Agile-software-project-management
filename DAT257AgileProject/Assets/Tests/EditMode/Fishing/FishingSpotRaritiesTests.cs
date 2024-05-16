using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class FishingSpotRaritiesTests
{
    private FishingSpotRarities rarities;
    [SetUp]
    public void SetUp()
    {
        rarities = new FishingSpotRarities();
    }

    [Test]
    public void ToList_WhenRaritiesIsNull_ThrowsArgumentNullException()
    {
        rarities = null;
        Assert.Throws<System.ArgumentNullException>(() => rarities.ToList());
    }

    [Test]
    public void ToList_WhenRaritiesTrashRarityPercentagesIsNull_ThrowsArgumentNullException()
    {
        rarities.trashRarityPercentages = null;
        Assert.Throws<System.ArgumentNullException>(() => rarities.ToList());
    }

    [Test]
    public void ToList_WhenRaritiesTrashRarityPercentagesIsEmpty_ThrowsArgumentException()
    {
        rarities.trashRarityPercentages = new List<RarityPercentageData>();
        Assert.Throws<System.ArgumentException>(() => rarities.ToList());
    }

    [Test]
    public void ToList_WhenTrashRarityPercentageContainsNullElement_ThrowsArgumentNullException()
    {
        rarities.trashRarityPercentages = new List<RarityPercentageData> { null };
        Assert.Throws<System.ArgumentNullException>(() => rarities.ToList());
    }
}