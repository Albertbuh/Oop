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
    /* commands = pluginPaths.SelectMany((string pluginPath)=> 
    {
      Assembly pluginAssembly = LoadPlugin(pluginPath);
      return CreateCommands(pluginAssembly);
    }); */
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

    /* foreach(ICommand command in commands)
    {
      Console.WriteLine($"{command.Name}\t -\t {command.Description}");
    } */

  }


#nullable disable
  private static Assembly LoadPlugin(string relativePath)
  {
    string root = Path.GetFullPath(Path.Combine(
          Path.GetDirectoryName(
            Path.GetDirectoryName( 
              Path.GetDirectoryName(
                Path.GetDirectoryName( 
                  Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))))));
    string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
    // Console.WriteLine(pluginLocation);
    Console.WriteLine($"Loading commands from: {pluginLocation}");
    Console.WriteLine("...");
    PluginLoadContext context = new PluginLoadContext(pluginLocation);
    return context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
  
  }
#nullable restore

  private static List<ICommand?> CreateCommands(Assembly assembly)
  {
    List<ICommand?> result = new List<ICommand?>();
    foreach(Type type in assembly.GetTypes())
    {
      if(typeof(ICommand).IsAssignableFrom(type))
      {
        // ICommand? result = Activator.CreateInstance(type) as ICommand;
        result.Add(Activator.CreateInstance(type) as ICommand);
      }
    }
    return result;
  }
}
