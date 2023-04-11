[Serializable]
public class Vegetable : Product
{
  public virtual string Sort{
    get => "no sort";
    set => this.Sort = value; 
  }
  private decimal price = 0;
  public virtual decimal Price{
    get => price;
    set => price = value;
  }

  public Vegetable() : this("unknown veg")
  {}
  public Vegetable(string name) : this(name, MASS)
  {}
  public Vegetable(string name, double mass) : base(name, mass)
  {
    PrintInfo = consolePrintInfo;
  }

  public override void consolePrintInfo()
  {
    Console.WriteLine($"Vegetable Name: {Name}({Mass:f2} g)\n");
    PrintVitamins();
    PrintNutrition();
  }
}



