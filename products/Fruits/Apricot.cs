[Serializable]
public class Apricot : Fruit
{
  public string Sort { get; set; }
  public decimal Price { get; set; }
  
  public Apricot() : this("Undefiend", 0)
  {}
  public Apricot(string sort, decimal price = 0, double mass = MASS) : base("Apricot", mass)
  {
    Sort = sort;
    Price = price *(decimal)msc;
    SetVitamins( "A:0.267", "B:0.09", "C:10", "E:1.1");
    SetNutrition(0.39, 1.42, 11.42, 48.57);
    PrintInfo = consolePrintInfo;
 }

  public override void consolePrintInfo()
  {
    Console.WriteLine($"Apricot sort: {Sort}({Mass:f2} g)\n");
    Console.WriteLine($"Price: {Price:C2}\n");
    PrintVitamins();
    Console.WriteLine("\nNutrition:");
    PrintNutrition(); 
  }

}


