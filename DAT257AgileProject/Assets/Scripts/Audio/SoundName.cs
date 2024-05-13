using System;

public enum SoundName
{
    BeginFishing,
    ExclamationMarkAppears,
    TrashCaught,
    ItemBought,
    RecycleNoise,
    SliderInteraction
}

public static class SoundNameExtensions
{
    public static string ToReadableString(this SoundName soundName)
    {
        switch (soundName)
        {
            case SoundName.BeginFishing:
                return "Begin Fishing";
            case SoundName.ExclamationMarkAppears:
                return "Exclamation Mark Appears";
            case SoundName.TrashCaught:
                return "Trash Caught";
            case SoundName.ItemBought:
                return "Item Bought";
            case SoundName.RecycleNoise:
                return "Recycle Noise";
            case SoundName.SliderInteraction:
                return "Slider Interaction";
            default:
                throw new ArgumentOutOfRangeException(nameof(soundName), soundName, $"¨Sound name not found {soundName}");
        }
    }
}
