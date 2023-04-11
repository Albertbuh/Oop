// namespace Onion;
[Serializable]
public class Onion : Vegetable
{
  public override string Sort {get; set;}
  public override decimal Price { get; set; }

  public Onion() : this("Undefiend",0)
  {}
  public Onion(string sort, decimal price = 0, double mass = MASS) : base("Onion", mass)
  {
    Sort = sort;
    Price = price * (decimal)msc;
    SetVitamins("B:0.07", "C:10", "E:0.2");
    SetNutrition(0.2, 1.4, 8.2, 41);
    PrintInfo = consolePrintInfo;
  }

  public override void consolePrintInfo()
  {
    Console.WriteLine($"Onion sort: {Sort}({Mass:f2} g)\n");
    Console.WriteLine($"Price: {Price:C2}\n");
    PrintVitamins();
    Console.WriteLine("\nNutrition:");
    PrintNutrition(); 
  }

  public override void SpecFunc()
  {
    while(true)
    {
      Console.Write("HAAHAHHAHHAHHHA");
    }
  }
}

