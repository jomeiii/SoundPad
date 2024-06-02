namespace SoundPad.UI;

public class CustomizationApplication
{
    private static readonly ConsoleColorChanger ColorChanger = new();

    public static void Open()
    {
        Console.Clear();
        Console.WriteLine("Choose an option:");
        ColorChanger.WriteColoredText("1. Change foreground color");
        ColorChanger.WriteColoredText("2. Change background color");
        ColorChanger.WriteColoredText("3. Reset colors");
        ColorChanger.WriteColoredText("4. Write colored text");
        ColorChanger.WriteColoredText("5. Exit");
        Console.Write("Enter your choice: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter the foreground color (e.g., Red, Green, Blue): ");
                if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor foregroundColor))
                {
                    ColorChanger.SetForegroundColor(foregroundColor);
                    ColorChanger.WriteColoredText("Foreground color changed.",
                        ConsoleColor.Black, foregroundColor);
                }
                else
                {
                    ColorChanger.WriteColoredText("Invalid color.", ConsoleColor.Black, ConsoleColor.Red);
                }

                break;

            case "2":
                Console.Write("Enter the background color (e.g., Red, Green, Blue): ");
                if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor backgroundColor))
                {
                    ColorChanger.SetBackgroundColor(backgroundColor);
                    ColorChanger.WriteColoredText("Background color changed.",
                        backgroundColor, ConsoleColor.White);
                }
                else
                {
                    ColorChanger.WriteColoredText("Invalid color.", ConsoleColor.Black, ConsoleColor.Red);
                }

                break;

            case "3":
                ColorChanger.ResetColors();
                ColorChanger.WriteColoredText("Colors reset to default.",
                    ConsoleColor.Black, ConsoleColor.White);
                break;

            case "4":
                Console.Write("Enter the text: ");
                var text = Console.ReadLine();
                Console.Write("Enter the foreground color (e.g., Red, Green, Blue): ");
                if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor fgColor))
                {
                    Console.Write("Enter the background color (e.g., Red, Green, Blue): ");
                    if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor bgColor))
                        ColorChanger.WriteColoredText(text, bgColor, fgColor);
                    else
                        ColorChanger.WriteColoredText("Invalid background color.",
                            ConsoleColor.Black, ConsoleColor.Red);
                }
                else
                {
                    ColorChanger.WriteColoredText("Invalid foreground color.",
                        ConsoleColor.Black, ConsoleColor.Red);
                }

                break;

            default:
                ColorChanger.WriteColoredText("Invalid command.", ConsoleColor.Black, ConsoleColor.Red);
                break;
        }
    }
}