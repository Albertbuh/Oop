[Serializable]
public class Carrot : Vegetable
{
  public override string Sort {get; set;}
  public override decimal Price { get; set; }

  public Carrot() : this("Undefiend",0)
  {}
  public Carrot(string sort, decimal price = 0, double mass = MASS) : base("Carrot", mass)
  {
    Sort = sort;
    Price = price * (decimal)msc;
    SetVitamins("A:2", "B:0.13", "C:5", "E:0.4");
    SetNutrition(0.1, 1.3, 6.9, 35);
    PrintInfo = consolePrintInfo;
  }

  public override void consolePrintInfo()
  {
    Console.WriteLine($"Carrot sort: {Sort}({Mass:f2} g)\n");
    Console.WriteLine($"Price: {Price:C2}\n");
    PrintVitamins();
    Console.WriteLine("\nNutrition:");
    PrintNutrition(); 
  }
}
