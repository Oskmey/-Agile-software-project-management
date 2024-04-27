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
public static List<float> ToList(this FishingSpotRarities rarities)
{
    List<float> tempList = new List<float>();

    foreach(RarityPercentageData data in rarities.trashRarityPercentages)
    {
        tempList.Add(data.percentage);

    }

    for (int i = 1; i < tempList.Count ; i++)
    {
        tempList[i] = tempList[i] + tempList[i - 1];
    }
    return tempList;
}

}