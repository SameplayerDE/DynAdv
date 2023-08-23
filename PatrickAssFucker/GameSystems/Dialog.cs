namespace PatrickAssFucker.GameSystems;

public class Dialog
{
    public Func<string>? Output;
    public Func<string>? Input;
    public Action? Action;
    public Func<bool>? IsAvailable;
    public List<Dialog> Options = new();
    public bool Return;
    
    public void Add(Dialog dialog)
    {
        Options.Add(dialog);
    }
}