using System;
using System.Collections.Generic;
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
}
