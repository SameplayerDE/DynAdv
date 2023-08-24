namespace PatrickAssFucker;

public interface IMod
{
    public string Author { get; }
    public string Name { get; }
    public string Version { get; }
    
    public void Init();
    public void Load();
    public void Unload();
}