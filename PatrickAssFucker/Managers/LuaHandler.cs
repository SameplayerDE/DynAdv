namespace PatrickAssFucker.Managers;

using NLua;

public class LuaHandler
{
    private Lua _lua;

    public LuaHandler()
    {
        _lua = new Lua();
    }

    public void RegisterObject(object obj, string name)
    {
        _lua[name] = obj;
    }

    public void DoFile(string path)
    {
        _lua.DoFile(path);
    }
}