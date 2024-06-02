using SoundPad.Config;
using SoundPad.Struct;
using SoundPad.UI;

namespace SoundPad;

public abstract class Program
{
    public static readonly List<string> SoundPaths = new();
    public static List<Sound> Sounds = new();
    public static Sound CurrentSound;

    private static readonly SoundPlayer SoundPlayer = new();
    private static readonly ConsoleColorChanger ColorChanger = new();
    private static readonly ConfigManager ConfigManager = new();

    static Program()
    {
        SetSoundPaths();
        Console.WriteLine("Static constructor executed");
    }


    private static void Play()
    {
        if (SoundPlayer.IsPlaying)
        {
            Console.WriteLine("Already playing. Stop the song that's playing right now");
            return;
        }

        var filePath = ChooseCurrentSound();

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

    private static void SetSoundPaths()
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

        for (var index = 0; index < files.Length; index++)
        {
            var file = files[index];
            SoundPaths.Add(file);
            SetSound(file, index);
            Console.WriteLine(file + " ");
        }
    }

    private static void SetSound(string path, int index)
    {
        Sounds.Add(new Sound(path, index));
    }

    private static void GetConfigManager()
    {
        ColorChanger.WriteColoredText("Write sounds folder path.", ConsoleColor.Yellow,
            ConsoleColor.Black);

        ConfigManager.config.soundPath = Console.ReadLine();
    }

    /// <summary>
    ///     Prompts the user to choose a sound from the available list by its index and returns the path of the selected sound.
    /// </summary>
    /// <returns>The file path of the selected sound.</returns>
    /// <remarks>
    ///     The method displays a list of available sounds with their indices and prompts the user to input the index of the
    ///     desired sound.
    ///     If the input is invalid or the index is out of range, the user is prompted to try again until a valid selection is
    ///     made.
    /// </remarks>
    private static string ChooseCurrentSound()
    {
        ColorChanger.WriteColoredText("Write index sound to choose it.", ConsoleColor.Yellow,
            ConsoleColor.Black);

        foreach (var sound in Sounds) Console.WriteLine($"{sound.Index + 1}. {sound.Name}");

        var input = Console.ReadLine();
        while (true)
            if (string.IsNullOrWhiteSpace(input))
            {
                ColorChanger.WriteColoredText("Invalid input! Try again.", ConsoleColor.Yellow,
                    ConsoleColor.Black);
            }
            else if (!int.TryParse(input, out var index))
            {
                ColorChanger.WriteColoredText("Invalid index! Try again.", ConsoleColor.Yellow,
                    ConsoleColor.Black);
            }
            else
            {
                index--;
                CurrentSound = Sounds[index];

                ColorChanger.WriteColoredText($"Sound {CurrentSound.Index + 1}. {CurrentSound.Name} selected.",
                    ConsoleColor.Yellow,
                    ConsoleColor.Black);
                break;
            }

        return CurrentSound.Path;
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
            var input = Console.ReadLine()?.Trim().ToLower();

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