using System;

public enum TrashType
{
    TrashBag,
    PETBottle,
    ElectricScooter,
    CigaretteButt
}

public static class TrashTypeExtensions
{
    public static string ToReadableString(this TrashType trashType)
    {
        switch (trashType)
        {
            case TrashType.TrashBag:
                return "Trash Bag";
            case TrashType.PETBottle:
                return "PET Bottle";
            case TrashType.ElectricScooter:
                return "Electric Scooter";
            case TrashType.CigaretteButt:
                return "Cigarette Butts";
            default:
                throw new ArgumentOutOfRangeException(nameof(trashType), trashType, $"Trash type not found {trashType}");
        }
    }
}