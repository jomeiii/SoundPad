using SoundPad.Struct;
using SoundPad.UI;

namespace SoundPad
{
    public abstract class Program
    {
        public static readonly string FolderPath = @"D:\Projects\CSharpProjects\SoundPad\SoundPad\SoundPad\Sounds";
        public static readonly List<string> SoundPaths = new();

        private static readonly SoundPlayer SoundPlayer = new SoundPlayer();
        private static readonly ConsoleColorChanger ColorChanger = new ConsoleColorChanger();

        public static Sound CurrentSound { get; private set; } = new();

        static Program()
        {
            GetSoundPaths();
            Console.WriteLine("Static constructor executed");
        }

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

            ColorChanger.WriteColoredText(text: $"Playback started for file: {CurrentSound.Path}", ConsoleColor.Yellow,
                ConsoleColor.Black);
            ColorChanger.WriteColoredText(text: $"Now playing: {CurrentSound.Name}", ConsoleColor.Yellow,
                ConsoleColor.Black);
            
            SoundPlayer.PlaySound(filePath);
        }

        private static void Stop()
        {
            SoundPlayer.StopSound();
            ColorChanger.WriteColoredText(text: $"Playback stopped for file: {CurrentSound.Path}", ConsoleColor.Yellow,
                ConsoleColor.Black);
        }

        private static void GetSoundPaths()
        {
            Console.WriteLine($"Checking directory: {FolderPath}");

            if (!Directory.Exists(FolderPath))
            {
                Console.WriteLine($"Directory does not exist: {FolderPath}");
                return;
            }

            Console.WriteLine($"Directory exists: {FolderPath}");

            string[] files = Directory.GetFiles(FolderPath);

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


        public static void Main(string[] args)
        {
            ColorChanger.WriteColoredText("Sound Pad", ConsoleColor.DarkCyan, ConsoleColor.White);
            ColorChanger.WriteColoredText("Press Ctrl-C to quit.", ConsoleColor.Yellow, ConsoleColor.Black);
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
}