public enum TrashRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public static class TrashRarityExtensions
{
    public static string ToReadableString(this TrashRarity rarity)
    {
        switch (rarity)
        {
            case TrashRarity.Common:
                return "Common";
            case TrashRarity.Uncommon:
                return "Uncommon";
            case TrashRarity.Rare:
                return "Rare";
            case TrashRarity.Epic:
                return "Epic";
            case TrashRarity.Legendary:
                return "Legendary";
            default:
                return "Unknown";
        }
    }

    // Method which gives equal chance to get any rarity
    // It is a placeholder for a more complex logic
    // But could also be used for manual testing
    public static TrashRarity GetRandomRarity()
    {
        var values = System.Enum.GetValues(typeof(TrashRarity));
        return (TrashRarity)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}