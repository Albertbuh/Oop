[Serializable]
public class Pork : Meat
{
  public override decimal Price { get; set; }
  
  public Pork(decimal price = 0, double mass = MASS) : base("pork", mass)
  {
    Price = price;
    SetVitamins("B:1.61", "E:0.5");
    SetNutrition(21.6, 16, 0, 259);
    PrintInfo = consolePrintInfo;
  }

  public override void consolePrintInfo()
  {
    Console.WriteLine($"Pork({Mass:f2} g):\n");
    PrintVitamins();
    PrintNutrition();
  }
}


