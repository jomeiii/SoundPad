using SoundPad.UI;

namespace SoundPad;

public abstract class Program
{
    private static readonly SoundPlayer SoundPlayer = new();
    private static readonly ConsoleColorChanger ColorChanger = new();

    private static void Play()
    {
        if (SoundPlayer.IsPlaying)
        {
            Console.WriteLine("Already playing");
            return;
        }

        var filePath = @"D:\Projects\CSharpProjects\SoundPad\SoundPad\SoundPad\Sounds\sound.wav";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Invalid file path");
            return;
        }

        SoundPlayer.PlaySound(filePath);
    }

    private static void Stop()
    {
        SoundPlayer.StopSound();
        Console.WriteLine("Playback stopped");
    }

    private static void Main(string[] args)
    {
        ColorChanger.WriteColoredText("Sound Pad", ConsoleColor.DarkCyan, ConsoleColor.White);
        ColorChanger.WriteColoredText("Press Ctrl-C to quit.", ConsoleColor.Yellow, ConsoleColor.Black);
        ColorChanger.WriteColoredText("Write \"play\" to play sound or \"stop\" to stop.", ConsoleColor.Yellow,
            ConsoleColor.Black);

        while (true)
        {
            Console.Write("Wait command: ");
            var input = Console.ReadLine().Trim().ToLower();

            if (input == "play")
            {
                Play();
            }
            else if (input == "stop")
            {
                Stop();
            }
            else if (input == "quit" || input == "q")
            {
                Stop();
                break;
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        }
    }
}