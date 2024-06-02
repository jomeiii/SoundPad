namespace SoundPad.Config;

public class ConfigManager
{
    private static readonly string ConfigPath =
        @"D:\\Projects\\CSharpProjects\\SoundPad\\SoundPad\\SoundPad\\config.txt";

    public Config config;

    public ConfigManager()
    {
        using StreamReader sr = new(ConfigPath);

        while (sr.ReadLine() is { } line)
        {
            config = ParseConfig(sr, line);

            var delimiter = sr.ReadLine();
            if (delimiter == ";")
                break;
            if (delimiter != ",") break;
        }
    }

    private static Config ParseConfig(StreamReader sr, string firstLine)
    {
        var soundPath = firstLine.Replace("SoundPath: ", "");

        return new Config(soundPath);
    }
}