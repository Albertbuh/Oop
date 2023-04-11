[Serializable]
public abstract class Fruit : Product
{

  public Fruit() : this("unknown fruit")
  {}
  public Fruit(string name) : this(name, MASS)
  {}
  public Fruit(string name, double mass) : base(name, mass)
  {  
    PrintInfo = consolePrintInfo;
  }
 
  public override void consolePrintInfo()
  {
    Console.WriteLine($"Fruit Name: {Name}({Mass:f2} g)\n");
    PrintVitamins();
    PrintNutrition();
  }
}




