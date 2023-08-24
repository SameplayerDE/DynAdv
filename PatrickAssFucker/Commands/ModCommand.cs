using System.Reflection;

namespace PatrickAssFucker.Commands;

public class ModCommand : ICommand
{
    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            //ZEIGE HILFE ODER LISTE
            return;
        }

        if (args.Length == 1)
        {
            var arg = args[0];
            if (arg.Equals("reload", StringComparison.OrdinalIgnoreCase))
            {
                //RELOAD MODS OR LOAD
                string modsPath = "Assets/Mods";
                foreach (string dll in Directory.GetFiles(modsPath, "*.dll"))
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                    foreach (Type type in loadedAssembly.GetTypes())
                    {
                        if (type.GetInterface(nameof(IMod)) != null)
                        {
                            IMod modInstance = (IMod)Activator.CreateInstance(type);
                            modInstance.Init();
                            modInstance.Load();
                        }
                    }
                }
                return;
            }
            if (arg.Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                //LIST MODS
                return;
            }
            return;
        }
    }
}