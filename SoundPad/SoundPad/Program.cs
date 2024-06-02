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
        Console.Clear();

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


        Console.Clear();
        ColorChanger.WriteColoredText($"Playback started for file: {CurrentSound.Path}");
        ColorChanger.WriteColoredText($"Now playing: {CurrentSound.Name}");

        SoundPlayer.PlaySound(filePath);
    }

    private static void Stop()
    {
        SoundPlayer.StopSound();
        ColorChanger.WriteColoredText($"Playback stopped for file: {CurrentSound.Path}");
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
        ColorChanger.WriteColoredText("Write sounds folder path.");

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
        ColorChanger.WriteColoredText("Write index sound to choose it.");

        foreach (var sound in Sounds) 
            Console.WriteLine($"{sound.Index + 1}. {sound.Name}");

        while (true)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                ColorChanger.WriteColoredText("Invalid input! Try again.");
            }
            else if (!int.TryParse(input, out var index) || index >= Sounds.Count)
            {
                ColorChanger.WriteColoredText("Invalid index! Try again.");
            }
            else
            {
                index--;
                CurrentSound = Sounds[index];

                ColorChanger.WriteColoredText($"Sound {CurrentSound.Index + 1}. {CurrentSound.Name} selected.");
                break;
            }
        }

        return CurrentSound.Path;
    }

    public static void Main(string[] args)
    {
        ColorChanger.WriteColoredText("Sound Pad", ConsoleColor.White, ConsoleColor.DarkCyan);

        while (true)
        {
            ColorChanger.WriteColoredText("Press Ctrl-C or write \"quit\" to quit.");
            ColorChanger.WriteColoredText("Write \"play\" to play sound or \"stop\" to stop.");
            Console.Write("\nWait command: ");

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