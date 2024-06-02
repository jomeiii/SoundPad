namespace SoundPad.Struct;

public struct Sound
{
    private string _name;
    private string _path;

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Path
    {
        get => _path;
        set => _path = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Index { get; set; }

    public Sound(string path, int index)
    {
        _name = GetName(path);
        _path = path;
        Index = index;
    }

    private string GetName(string path)
    {
        var fileName = System.IO.Path.GetFileName(path);
        return fileName;
    }
}