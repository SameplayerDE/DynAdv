namespace PatrickAssFucker.Entities;

public enum DefinedTags
{
    Durability,
    Saturation
}

public class ItemMeta
{
    public string? Displayname;
    public Dictionary<string, object> Tags = new Dictionary<string, object>();

    public void AddTag(string key, object value)
    {
        Tags.Add(key, value);
    }
    
    public T GetTag<T>(string key)
    {
        if (Tags.TryGetValue(key, out var value))
        {
            return (T)value;
        }
        return default(T);
    }

    public bool HasTag(string key)
    {
        return Tags.ContainsKey(key);
    }
    
}