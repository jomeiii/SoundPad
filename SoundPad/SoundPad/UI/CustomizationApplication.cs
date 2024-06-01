namespace SoundPad.UI;

public class CustomizationApplication
{
    private static readonly ConsoleColorChanger ColorChanger = new();

    public static void Open()
    {
        Console.Clear();
        Console.WriteLine("Choose an option:");
        ColorChanger.WriteColoredText("1. Change foreground color", ConsoleColor.Yellow, ConsoleColor.Black);
        ColorChanger.WriteColoredText("2. Change background color", ConsoleColor.Yellow, ConsoleColor.Black);
        ColorChanger.WriteColoredText("3. Reset colors", ConsoleColor.Yellow, ConsoleColor.Black);
        ColorChanger.WriteColoredText("4. Write colored text", ConsoleColor.Yellow, ConsoleColor.Black);
        ColorChanger.WriteColoredText("5. Exit", ConsoleColor.Yellow, ConsoleColor.Black);
        Console.Write("Enter your choice: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter the foreground color (e.g., Red, Green, Blue): ");
                if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor foregroundColor))
                {
                    ColorChanger.SetForegroundColor(foregroundColor);
                    ColorChanger.WriteColoredText("Foreground color changed.", foregroundColor,
                        ConsoleColor.Black);
                }
                else
                {
                    ColorChanger.WriteColoredText("Invalid color.", ConsoleColor.Red, ConsoleColor.Black);
                }

                break;

            case "2":
                Console.Write("Enter the background color (e.g., Red, Green, Blue): ");
                if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor backgroundColor))
                {
                    ColorChanger.SetBackgroundColor(backgroundColor);
                    ColorChanger.WriteColoredText("Background color changed.", ConsoleColor.White,
                        backgroundColor);
                }
                else
                {
                    ColorChanger.WriteColoredText("Invalid color.", ConsoleColor.Red, ConsoleColor.Black);
                }

                break;

            case "3":
                ColorChanger.ResetColors();
                ColorChanger.WriteColoredText("Colors reset to default.", ConsoleColor.White,
                    ConsoleColor.Black);
                break;

            case "4":
                Console.Write("Enter the text: ");
                var text = Console.ReadLine();
                Console.Write("Enter the foreground color (e.g., Red, Green, Blue): ");
                if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor fgColor))
                {
                    Console.Write("Enter the background color (e.g., Red, Green, Blue): ");
                    if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor bgColor))
                        ColorChanger.WriteColoredText(text, fgColor, bgColor);
                    else
                        ColorChanger.WriteColoredText("Invalid background color.", ConsoleColor.Red,
                            ConsoleColor.Black);
                }
                else
                {
                    ColorChanger.WriteColoredText("Invalid foreground color.", ConsoleColor.Red,
                        ConsoleColor.Black);
                }

                break;

            default:
                ColorChanger.WriteColoredText("Invalid command.", ConsoleColor.Red, ConsoleColor.Black);
                break;
        }
    }
}