[Serializable]
public class Banana: Fruit
{
  public string Sort { get; set; }
  public decimal Price { get; set; }
  
  public Banana() : this("Undefiend", 0)
  {}
  public Banana(string sort, decimal price = 0, double mass = MASS) : base("banana", mass)
  {
    Sort = sort;
    Price = price *(decimal)msc;
    SetVitamins( "A:0.02", "B:0.09", "C:10", "E:0.4");
    SetNutrition(0.2, 1.5, 21.8, 95);
    // PrintInfo = consolePrintInfo;
  }

  public override void consolePrintInfo()
  {
    Console.WriteLine($"Banana sort: {Sort}({Mass:f2} g)\n");
    Console.WriteLine($"Price: {Price:C2}\n");
    PrintVitamins();
    Console.WriteLine("\nNutrition:");
    PrintNutrition(); 
  }

}


