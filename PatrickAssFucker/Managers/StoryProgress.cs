namespace PatrickAssFucker.Managers;

public enum ProgressType
{
    KeyEvents,
    Decisions,
}

public class StoryProgress
{
    private readonly Dictionary<ProgressType, Dictionary<string, bool>> _progress = new();
    
    public static StoryProgress Instance { get; } = new();

    static StoryProgress()
    {
        
    }

    private StoryProgress()
    {
        
    }
    
    public bool CheckCondition(ProgressType type, string condition)
    {
        if (_progress.TryGetValue(type, out var dict))
        {
            return dict.TryGetValue(condition, out var result) && result;
        }

        return false;
    }

    public void SetCondition(ProgressType type, string condition, bool value)
    {
        if (!_progress.ContainsKey(type))
        {
            _progress[type] = new Dictionary<string, bool>();
        }
        _progress[type][condition] = value;
    }
}