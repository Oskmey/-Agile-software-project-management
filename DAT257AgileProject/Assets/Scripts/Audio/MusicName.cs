using System;

public enum MusicName
{
    MenuTheme,
    StoreTheme,
    WorldTheme
}

public static class MusicNameExtensions
{
    public static string ToReadableString(this MusicName musicName)
    {
        switch (musicName)
        {
            case MusicName.MenuTheme:
                return "Menu Theme";
            case MusicName.StoreTheme:
                return "Store Theme";
            case MusicName.WorldTheme:
                return "World Theme";
            default:
                throw new ArgumentOutOfRangeException(nameof(musicName), musicName, $"Music name not found {musicName}");
        }
    }
}
