using System;
using System.Linq;

public class ArticleForWordHelper
{
    private const string vowels = "aeiou";

    public static string GetArticle(string word)
    {
        ValidateWord(word);

        char firstLetter = char.ToLower(word[0]);
        if (vowels.Contains(firstLetter))
        {
            return "an";
        }
        else
        {
            return "a";
        }
    }

    private static void ValidateWord(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentNullException("Word cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(word))
        {
            throw new ArgumentException("Word cannot be whitespace only.");
        }

        if (word.All(char.IsDigit))
        {
            throw new ArgumentException("Word cannot be numbers only.");
        }
    }
}