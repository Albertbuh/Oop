using PluginBase;
using System.Reflection;

public class PluginLoader
{
  private static HashSet<string> pluginPaths = new HashSet<string>();

  private static List<ICommand> commands = new List<ICommand>();


  public static void Load(params string[] filepaths)
  {
    pluginPaths.Clear();
    commands.Clear();

    foreach(var filepath in filepaths)
    {
      pluginPaths.Add(filepath);
    }

    if(pluginPaths.Count > 0)
      Execute();
  }

  public static (string Name, string Desc, Action<object[]> Func) GetCommandInfo(int ind = 0)
  { 
    return (Name : commands[ind].Name, Desc : commands[ind].Description, Func: commands[ind].Execute);
  }

  public static Action<object[]>? GetExecute(int ind = 0)
  {
    return ind < commands.Count ? commands[ind].Execute : null;
  }

  public static void Invoke(params object[] args)
  {
    try
    {
      commands[0].Execute(args);
    }
    catch 
    {
      Console.WriteLine("No command in plugin");
    }
  }

  private static void Execute()
  {
    Console.WriteLine("Start execution...");
    Console.WriteLine("Commands:");
     try
    {
      foreach(string path in pluginPaths)
      {
        Assembly pluginAssembly = LoadPlugin(path);
        var assemblyCommands = CreateCommands(pluginAssembly);
        foreach(var com in assemblyCommands)
        {
          if(com is not null)
            commands.Add(com);
        }
      }
      Console.WriteLine("Commands have been loaded.");
    }
    catch(Exception ex)
    {
      Console.WriteLine("Error in assembly plugings (check your filepath one more time):\n" + ex.Message);
    }
  }


  private static Assembly LoadPlugin(string DllPath)
  {
    string? GetFolderPath(string path, string folder_name)
    {
      try
      {
        return Directory.GetDirectories(path).Where((string folder) => folder.Contains(folder_name)).ToList()[0];
      }
      catch
      {
        return null;
      }
    }

    string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    string plug_name = DllPath.Substring(DllPath.LastIndexOf("/")+1) + ".dll";
    string bin = GetFolderPath(Path.Combine(homeDir, DllPath), "bin") ?? "";
    string debug = GetFolderPath(bin, "Debug") ?? "";
    string net = GetFolderPath(debug, "net") ?? "";
    Console.WriteLine(net);
    if(!net.Equals(""))
      DllPath = Directory.GetFiles(net).Where((string file) => file.Contains(plug_name)).ToList()[0];
    Console.WriteLine($"Loading commands from: {DllPath}");
    Console.WriteLine("...");
    PluginLoadContext context = new PluginLoadContext(DllPath);
    return context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(DllPath)));
  }

  private static List<ICommand?> CreateCommands(Assembly assembly)
  {
    List<ICommand?> result = new List<ICommand?>();
    foreach(Type type in assembly.GetTypes())
    {
      if(typeof(ICommand).IsAssignableFrom(type))
      {
        result.Add(Activator.CreateInstance(type) as ICommand);
      }
    }
    return result;
  }
}
