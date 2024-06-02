namespace SoundPad.UI;

public class ConsoleColorChanger
{
    public void SetForegroundColor(ConsoleColor color)
    {
        Console.ForegroundColor = color;
    }

    public void SetBackgroundColor(ConsoleColor color)
    {
        Console.BackgroundColor = color;
    }

    public void ResetColors()
    {
        Console.ResetColor();
    }

    public void WriteColoredText(string text, ConsoleColor backgroundColor = ConsoleColor.Black,
        ConsoleColor foregroundColor = ConsoleColor.Yellow)
    {
        var originalForegroundColor = Console.ForegroundColor;
        var originalBackgroundColor = Console.BackgroundColor;

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.WriteLine(text);

        Console.ForegroundColor = originalForegroundColor;
        Console.BackgroundColor = originalBackgroundColor;
    }
}