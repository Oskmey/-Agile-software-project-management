using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AccessoryRarity
{
    Common = 50,
    Uncommon = 30,
    Rare = 15,
    Epic = 10,
    Legendary = 5
}

public static class AccessoryRarityExtensions
{
    public static string ToReadableString(this AccessoryRarity rarity)
    {
        switch (rarity)
        {
            case AccessoryRarity.Common:
                return "Common";
            case AccessoryRarity.Uncommon:
                return "Uncommon";
            case AccessoryRarity.Rare:
                return "Rare";
            case AccessoryRarity.Epic:
                return "Epic";
            case AccessoryRarity.Legendary:
                return "Legendary";
            default:
                return "Unknown";
        }
    }
}