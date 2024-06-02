namespace SoundPad.Config
{
    public class ConfigManager
    {
        public Config config;

        private static readonly string ConfigPath =
            @"D:\\Projects\\CSharpProjects\\SoundPad\\SoundPad\\SoundPad\\config.txt";

        public ConfigManager()
        {
            using StreamReader sr = new(ConfigPath);

            while (sr.ReadLine() is { } line)
            {
                config = ParseConfig(sr, line);
                
                string delimiter = sr.ReadLine();
                if (delimiter == ";")
                    break;
                if (delimiter != ",")
                {
                    break;
                }
            }
        }

        private static Config ParseConfig(StreamReader sr, string firstLine)
        {
            string soundPath = firstLine.Replace("SoundPath: ", "");

            return new Config(soundPath: soundPath);
        }
    }
}