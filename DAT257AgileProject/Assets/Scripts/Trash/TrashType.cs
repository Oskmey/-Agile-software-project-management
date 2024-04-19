using System;

public enum TrashType
{
    TrashBag,
    PETBottle,
    ElectricScooter,
    CigaretteButt,
    CarTire,
    PlasticSpoon
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
                return "Cigarette Butt";
            case TrashType.CarTire:
                return "Car Tire";
            case TrashType.PlasticSpoon:
                return "Plastic Spoon";
            default:
                throw new ArgumentOutOfRangeException(nameof(trashType), trashType, $"Trash type not found {trashType}");
        }
    }
}