using System;

public enum TrashCategory
{
    Organic,
    Paper,
    Plastic,
    Glass,
    Metal,
    Electronic
}

public static class TrashCategoryExtensions
{
    public static string ToReadableString(this TrashCategory trashCategory)
    {
        switch (trashCategory)
        {
            case TrashCategory.Organic:
                return "Organic";
            case TrashCategory.Paper:
                return "Paper";
            case TrashCategory.Plastic:
                return "Plastic";
            case TrashCategory.Glass:
                return "Glass";
            case TrashCategory.Metal:
                return "Metal";
            case TrashCategory.Electronic:
                return "Electronic";
            default:
                throw new ArgumentOutOfRangeException(nameof(trashCategory), trashCategory, $"Trash category not found {trashCategory}");
        }
    }
}
