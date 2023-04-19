using PluginBase;

public class Hello : ICommand
{
  public string Name => "Hello";
  public string Description => "Send hello message"; 

  public void Execute(params object[] args)
  {
    Console.WriteLine("Hello my darling");
  }
}
