namespace PluginBase
{

  public interface ICommand
  {
    string Name { get; }
    string Description { get; }

    void Execute(params object[] args);
  }
}

