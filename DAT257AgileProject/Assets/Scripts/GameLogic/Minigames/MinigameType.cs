using System;

public enum MinigameType
{
    ArrowBoxMinigame,
    AnotherMinigame
}

public static class MinigameTypeExtensions
{
    public static string ToReadableString(this MinigameType minigameType)
    {
        switch (minigameType)
        {
            case MinigameType.ArrowBoxMinigame:
                return "Arrow Box Minigame";
            case MinigameType.AnotherMinigame:
                return "Another Minigame";
            default:
                throw new ArgumentOutOfRangeException(nameof(minigameType), minigameType, $"Minigame type not found {minigameType}");
        }
    }
}
