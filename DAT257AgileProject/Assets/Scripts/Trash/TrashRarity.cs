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
}