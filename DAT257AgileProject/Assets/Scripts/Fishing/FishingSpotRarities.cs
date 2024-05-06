using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable] 
public class FishingSpotRarities
{
    public List<RarityPercentageData> trashRarityPercentages;
}

public static class FishingSpotRaritiesExtensions
{
    public static IReadOnlyList<float> ToList(this FishingSpotRarities rarities)
    {
        ValidateRarities(rarities);
        List<float> tempList = new();

        foreach(RarityPercentageData data in rarities.trashRarityPercentages)
        {
            tempList.Add(data.Percentage);
        }

        for (int i = 1; i < tempList.Count ; i++)
        {
            tempList[i] = tempList[i] + tempList[i - 1];
        }
        return tempList;
    }

    private static void ValidateRarities(FishingSpotRarities rarities)
    {
        ValidateNotNull(rarities);
        ValidateNotEmpty(rarities);
        ValidateNotNullElements(rarities);
        ValidatePercentages(rarities);
        ValidateElementNames(rarities);
    }

    private static void ValidateNotNull(FishingSpotRarities rarities)
    {
        if (rarities == null)
        {
            throw new ArgumentNullException($"{nameof(rarities)} is null");
        }
        if (rarities.trashRarityPercentages == null)
        {
            throw new ArgumentNullException($"{nameof(rarities.trashRarityPercentages)} is null");
        }
    }

    private static void ValidateNotEmpty(FishingSpotRarities rarities)
    {
        if (rarities.trashRarityPercentages.Count == 0)
        {
            throw new ArgumentException($"{nameof(rarities.trashRarityPercentages)} is empty");
        }
    }

    private static void ValidateNotNullElements(FishingSpotRarities rarities)
    {
        if (rarities.trashRarityPercentages.Any(data => data == null))
        {
            throw new ArgumentException($"{nameof(rarities.trashRarityPercentages)} contains null elements");
        }
    }

    private static void ValidatePercentages(FishingSpotRarities rarities)
    {
        if (rarities.trashRarityPercentages.Any(data => data.Percentage < 0))
        {
            throw new ArgumentException($"{nameof(rarities.trashRarityPercentages)} contains negative percentages");
        }
        if (rarities.trashRarityPercentages.Any(data => data.Percentage > 1))
        {
            throw new ArgumentException($"{nameof(rarities.trashRarityPercentages)} contains percentages greater than 1");
        }
    }

    private static void ValidateElementNames(FishingSpotRarities rarities)
    {
        if (rarities.trashRarityPercentages.Any(data => string.IsNullOrWhiteSpace(data.Name)))
        {
            throw new ArgumentException($"{nameof(rarities.trashRarityPercentages)} contains empty or whitespace names");
        }
    }
}
