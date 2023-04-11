[Serializable]
public class Cabage : Vegetable
{
  public override string Sort {get; set;}
  public override decimal Price { get; set; }

  public Cabage() : this("Undefiend",1)
  {}
  public Cabage(string sort, decimal price = 0, double mass = MASS) : base("Cabage", mass)
  {
    Sort = sort;
    Price = price * (decimal)msc;
    SetVitamins("A:0.005", "B:0.06", "C:15", "E:0.1");
    SetNutrition(0.3, 0.6, 4.6, 24);
    PrintInfo = consolePrintInfo;
 }

  public override void consolePrintInfo()
  {
    Console.WriteLine($"Cabage sort: {Sort}({Mass:f3} g)\n");
    Console.WriteLine($"Price: {Price:C3}\n");
    PrintVitamins();
    Console.WriteLine("\nNutrition:");
    PrintNutrition(); 
  }

}

