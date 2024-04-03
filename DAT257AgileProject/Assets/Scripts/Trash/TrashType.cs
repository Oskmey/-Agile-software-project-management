using System;

public enum TrashType
{
    TrashBag,
    PlaceHolderName // remove after adding new trash types
}

public static class TrashTypeExtensions
{
    public static string ToReadableString(this TrashType trashType)
    {
        switch (trashType)
        {
            case TrashType.TrashBag:
                return "Trash Bag";
            case TrashType.PlaceHolderName:
                return "Change this!";
            default:
                throw new ArgumentOutOfRangeException(nameof(trashType), trashType, $"Trash type not found {trashType}");
        }
    }
}