[Serializable]
public class Meat : Product
{
  private decimal price = 0;
  public virtual decimal Price{
    get => price;
    set => price = value;
  }

  public Meat() : base("unknown meat")
  {}
  public Meat(string name) : base(name)
  {}
  public Meat(string name, double mass) : base(name, mass)
  {}

  public override void consolePrintInfo()
  {
    Console.WriteLine($"Meat Name: {Name}({Mass:f2} g)\n");
    PrintVitamins();
    PrintNutrition();
    PrintInfo = consolePrintInfo;
  }
}




