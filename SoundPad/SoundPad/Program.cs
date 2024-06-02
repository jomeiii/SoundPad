using SoundPad.Config;
using SoundPad.Struct;
using SoundPad.UI;

namespace SoundPad;

public abstract class Program
{
    public static readonly List<string> SoundPaths = new();

    private static readonly SoundPlayer SoundPlayer = new();
    private static readonly ConsoleColorChanger ColorChanger = new();
    private static readonly ConfigManager ConfigManager = new();

    static Program()
    {
        GetSoundPaths();
        Console.WriteLine("Static constructor executed");
    }

    public static Sound CurrentSound { get; private set; }

    private static void Play()
    {
        if (SoundPlayer.IsPlaying)
        {
            Console.WriteLine("Already playing");
            return;
        }

        var index = new Random().Next(0, SoundPaths.Count);
        var filePath = SoundPaths[index];
        CurrentSound = new Sound(filePath, index);
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Invalid file path");
            return;
        }

        ColorChanger.WriteColoredText($"Playback started for file: {CurrentSound.Path}", ConsoleColor.Yellow,
            ConsoleColor.Black);
        ColorChanger.WriteColoredText($"Now playing: {CurrentSound.Name}", ConsoleColor.Yellow,
            ConsoleColor.Black);

        SoundPlayer.PlaySound(filePath);
    }

    private static void Stop()
    {
        SoundPlayer.StopSound();
        ColorChanger.WriteColoredText($"Playback stopped for file: {CurrentSound.Path}", ConsoleColor.Yellow,
            ConsoleColor.Black);
    }

    private static void GetSoundPaths()
    {
        Console.WriteLine($"Checking directory: {ConfigManager.config.soundPath}");

        if (!Directory.Exists(ConfigManager.config.soundPath))
        {
            Console.WriteLine($"Directory does not exist: {ConfigManager.config.soundPath}");
            return;
        }

        Console.WriteLine($"Directory exists: {ConfigManager.config.soundPath}");

        var files = Directory.GetFiles(ConfigManager.config.soundPath);

        if (files.Length == 0)
        {
            Console.WriteLine("No files found in the directory.");
            return;
        }

        Console.WriteLine("Found files:");

        foreach (var file in files)
        {
            SoundPaths.Add(file);
            Console.WriteLine(file + " ");
        }
    }

    private static void GetConfigManager()
    {
        ColorChanger.WriteColoredText("Write sounds folder path.", ConsoleColor.Yellow,
            ConsoleColor.Black);
        
        ConfigManager.config.soundPath = Console.ReadLine();
    }

    public static void Main(string[] args)
    {
        ColorChanger.WriteColoredText("Sound Pad", ConsoleColor.DarkCyan, ConsoleColor.White);
        ColorChanger.WriteColoredText("Press Ctrl-C or write \"quit\" to quit.", ConsoleColor.Yellow,
            ConsoleColor.Black);
        ColorChanger.WriteColoredText("Write \"play\" to play sound or \"stop\" to stop.", ConsoleColor.Yellow,
            ConsoleColor.Black);

        while (true)
        {
            Console.Write("Wait command: ");
            var input = Console.ReadLine().Trim().ToLower();

            if (input is "play" or "p")
            {
                Play();
            }
            else if (input is "stop" or "s")
            {
                Stop();
            }
            else if (input is "quit" or "q")
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