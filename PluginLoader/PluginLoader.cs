using PluginBase;
using System.Reflection;

public class PluginLoader
{
  private static HashSet<string> pluginPaths = new HashSet<string>();


#nullable disable
  private static IEnumerable<ICommand> commands;
#nullable restore


  public static void Load(params string[] filepaths)
  {
    foreach(var filepath in filepaths)
    {
      pluginPaths.Add(filepath);
    }

    if(pluginPaths.Count > 0)
      Execute();
  }

  public static (string Name, string Desc, Action<object[]> Func) GetCommandInfo()
  { 
    foreach(var command in commands)
      return (Name : command.Name, Desc : command.Description, Func: command.Execute);
    throw new InvalidOperationException("Commands is empty");
  }

  public static Action<object[]>? GetExecute()
  {
    foreach(var command in commands)
      return command.Execute;
    return null;
  }

  public static void Invoke(params object[] args)
  {
    foreach(var command in commands)
      command.Execute(args);
  }

  private static void Execute()
  {
    Console.WriteLine("Start execution...");
    Console.WriteLine("Commands:");
    commands = pluginPaths.SelectMany((string pluginPath)=> 
    {
      Assembly pluginAssembly = LoadPlugin(pluginPath);
      return CreateCommands(pluginAssembly);
    });

    foreach(ICommand command in commands)
    {
      Console.WriteLine($"{command.Name}\t -\t {command.Description}");
    }

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
    PluginLoadContext context = new PluginLoadContext(pluginLocation);
    return context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
  
  }
#nullable restore

  private static IEnumerable<ICommand> CreateCommands(Assembly assembly)
  {
    int count = 0;
    foreach(Type type in assembly.GetTypes())
    {
      if(typeof(ICommand).IsAssignableFrom(type))
      {
        ICommand? result = Activator.CreateInstance(type) as ICommand;
        if(result != null)
        {
          count++;
          yield return result;
        }
      }
    }
  }
}
