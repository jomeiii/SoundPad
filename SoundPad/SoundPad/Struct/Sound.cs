namespace SoundPad.Struct;

public struct Sound
{
    private string _name;
    private string _path;
    private int _index;

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

    public int Index
    {
        get => _index;
        set => _index = value;
    }

    public Sound(string path, int index)
    {
        _name = GetName(path);
        _path = path;
        _index = index;
    }

    private string GetName(string path)
    {
        string fileName = System.IO.Path.GetFileName(path);
        return fileName;
    }
}