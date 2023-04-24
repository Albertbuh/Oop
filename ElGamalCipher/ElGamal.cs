using PluginBase;
using ElGamalC;
using System.Text;

public class ElGamal : ElGamalCipher, ICommand
{
  public string Name => "El-Gamal cipher";
  public string Description => "El-Gamal crypthography";

  public readonly string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);


  private string? Input(string message)
  {
    Console.Write(message);
    return Console.ReadLine();
  }

  private int[] GetParamsFromUser()
  {
      int p, g = 0, k = 0, x = 0;
       Int32.TryParse(Input("Enter p: "), out p);
       Console.WriteLine("P: {0}", p);
       var roots = GetGRoots(p);
       if(roots != null)
       {
         StringBuilder message = new StringBuilder("Choose g:\n");
         foreach(var root in roots)
         {
           message.AppendLine($"  {root}"); 
         }
         message.Append("Enter: ");
         Int32.TryParse(Input(message.ToString()),out g);
       }
       Int32.TryParse(Input("Enter k (1, p-1) with GCD(k, p-1)=1: "), out k);
       Int32.TryParse(Input("Enter x (1, p-1): "), out x);
       return new int[] {p, g, k, x};
  }
  private void Encrypt()
  {
    Console.Write("Enter file to encrypt: ");
    string? filename = Console.ReadLine();
    if(filename != null && File.Exists(Path.Combine(HomeDirectory, filename)))
    {
       int[] args = GetParamsFromUser(); 
       this.SetParameters(args[0], args[1], args[2], args[3]);
       byte[]? ciphered_bytes = Encryption(Path.Combine(HomeDirectory, filename));
       if(ciphered_bytes != null)
       {
         Console.WriteLine("File has been encrypted...");
         string newfilename = Input("Enter new file name: ") ?? $"filename+_E";
         File.WriteAllBytes(Path.Combine(HomeDirectory, newfilename), ciphered_bytes);
         Console.WriteLine("File saved successfully.");
       }
       else
         Console.WriteLine("File couldn't be saved, file is empty.");
    }
    else
      Console.WriteLine("No such file {0}", Path.Combine(HomeDirectory, filename ?? "popka"));
  }

  private void Decrypt()
  {
    Console.Write("Enter file to decrypt: ");
    string? filename = Console.ReadLine();
    if(filename != null && File.Exists(Path.Combine(HomeDirectory, filename)))
    {
       int[] args = GetParamsFromUser(); 
       this.SetParameters(args[0], args[1], args[2], args[3]);
       byte[]? ciphered_bytes = Decryption(Path.Combine(HomeDirectory, filename));
       if(ciphered_bytes != null)
       {
         Console.WriteLine("File has been decrypted...");
         string newfilename = Input("Enter new file name: ") ?? $"filename+_E";
         File.WriteAllBytes(Path.Combine(HomeDirectory, newfilename), ciphered_bytes);
         Console.WriteLine("File saved successfully.");
       }
       else
         Console.WriteLine("File couldn't be saved, file is empty.");
    }
    else
      Console.WriteLine("No such file {0}", Path.Combine(HomeDirectory, filename ?? "popka"));
  
  }
  public void Execute(params object[] args)
  {
    Console.Write("Choose the action:\n  1. Encrypt file.\n  2. Decrypt file.\nEnter:");
    int ind = 0;
    Int32.TryParse(Console.ReadLine(), out ind);
    switch(ind)
    {
      case 1:
        Encrypt();
        break;
      case 2:
        Decrypt();
        break;
      default:
        Console.WriteLine("Incorrect input");
        break;
    }
  }
}
