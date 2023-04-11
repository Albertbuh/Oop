///<summary>
/// Parent-class, which  describes the main 
/// features of all products in programm
///</summary>

using Newtonsoft.Json;

public interface IProduct
{
  public String Name { get; set; }
  public double Mass { get; set; }
  public double Calories { get; set; }
  public NutValue GetNutrition { get; set; }
}


[Serializable]
abstract public class Product : IProduct
{

  [NonSerialized]
  public Type type;
///<summary>Name of product</summary>
  public string Name {get; set;}
///<summary>Mass of product</summary>
  protected const double MASS = 100;
  public double Mass { get; set; }
///<value name="msc">Mass coefficent to standart unit(MASS)</value>
  protected double msc { get { return Mass/MASS; } }
///<summary>Collection which stores product's vitamins(A,B,C,D,E,K)</summary>
  public Dictionary<string, double> Vitamins { get; set; } = new Dictionary<string, double>()
  {
    {"A",0},
    {"B",0},
    {"C",0},
    {"D",0},
    {"E",0},
    {"K",0}
  };

///<value name="Calories">Calories in product</value>
  public double Calories { get; set; } = 0;
  public NutValue Nuts;

///<summary>Delegates to Print information</summary>
  public delegate void PrintHandler();
  [JsonIgnore]
  [field:NonSerialized]
  public PrintHandler PrintInfo {get; set;}  
  [JsonIgnore]
  [field:NonSerialized]
  public PrintHandler PrintVitamins {get; set;}
  [JsonIgnore]
  [field:NonSerialized]
  public PrintHandler PrintNutrition {get; set;}


///<value name="GetNutrition">Returns fats, proteins and carbs</value>
  public NutValue GetNutrition { get {return Nuts;} set {} }
  // public Dictionary<string, double> GetVitamins { get => Vitamins; set {} }

///<summary>
///Constructor of Product class 
///</summary>
  public Product(string name = "Unknown product", double mass = MASS)
  {
    Name = name;
    Mass = mass;
    PrintInfo = consolePrintInfo;
    PrintVitamins = consolePrintVitamins;
    PrintNutrition = consolePrintNutrition;
    type = this.GetType();
  }

///<summary>
/// Method to set vitamins to product
///</summary>
///<param name="vitamins">Strings in format: VitaminName:Amount </params>
///<example>
/// Exampla of Method calling
///<code>
/// SetVitamins("A:0.005", "B:0.06", "C:15", "E:0.1");
///</code>
///</example>
  public void SetVitamins(params string[] vitamins)
  {
    foreach(var vitamin in vitamins)
    {
      string[] tmp = vitamin.Split(':');
      string type = tmp[0].ToUpper();
      double amount = Convert.ToDouble(tmp[1]);
      Vitamins[type] = amount * msc;
    }
  }

///<summary>
/// Method to set Nutrition to product, 
/// all params has to be counted for MASS value before Calling method
///</summary>
///<param name="fats">Just double variable which represents Fats(for MASS)</params>
///<param name="protein">Just double variable which represents Protein(for MASS)</params>
///<param name="carbs">Just double variable which represents Carbohydrates(for MASS)</params>
///<param name="calories">Calories number which product with MASS contains</params>
  public void SetNutrition(double fats, double protein, double carbs, double calories = 0)
  {
    Nuts.fats = fats * msc;
    Nuts.protein = protein * msc;
    Nuts.carbs = carbs * msc;
    Calories = calories * msc;
  }

///<summary>
/// Console output methods 
///</summary>
  public void consolePrintVitamins()
  {
    Console.WriteLine("Vitamins:");
    foreach (var item in Vitamins)
       Console.WriteLine($"\tVitamin: {item.Key} -> Amount: {item.Value,8:f3} mg");
    Console.WriteLine();
  }
  public void consolePrintNutrition() 
  {
    Nuts.PrintNutrition();
    Console.WriteLine($"Calories: {Calories}");
  }
  public virtual void consolePrintInfo() => Console.WriteLine("This is product");


///<summary>
///Special function of product, no reason, no sense, it just exists
///</summary>
  public virtual void SpecFunc() => Console.WriteLine("This product hasn't special fucntion");
}


///<summary>
/// Class to create products which has special functions
///</summary>
public class SpecialProduct : Product
{
  private Product product;
  public SpecialProduct(string index)
  {
    product = this[index];
  }
  
  public override void SpecFunc() =>  product.SpecFunc();

  public Product this[string index]
  {
    get {
        switch (index)
            {
              case "onion" or "1":
                return new Onion();
              case "apple" or "2":
                return new Apple();
              case "chicken" or "3":
                return new Chicken();
              default:
                throw new ArgumentException("This product has no spec function");
            }
    } 
  }
}

///<summary>
///Structure which represents nutrients
///</summary>
[Serializable]
public struct NutValue
{
  public double fats;
  public double protein;
  public double carbs;

///<value name="Sum">return summary of all nutrients </value>
  public double Sum { get {return fats + protein + carbs;} private set {Sum = 0;}}

  public static NutValue operator +(NutValue nut1, NutValue nut2)
  {
    return new NutValue { 
      fats = nut1.fats+nut2.fats,
      protein = nut1.protein+nut2.protein,
      carbs = nut1.carbs + nut2.carbs,
    };
  }

  public NutValue(double fats = 0, double protein = 0, double carbs = 0)
  {
    this.fats = fats;
    this.protein = protein;
    this.carbs = carbs;
  }

  public NutValue(NutValue temp, double k = 1)
  {
    this.fats = temp.fats*k;
    Console.WriteLine(this.fats);
    this.protein = temp.protein*k;
    Console.WriteLine(this.protein);
    this.carbs = temp.carbs*k;
    Console.WriteLine(this.carbs);
  }

///<summary>
/// Console method to print nutrients 
///</summary>
 public void PrintNutrition() 
  {
    Console.WriteLine("Nutritoinal values:");
    Console.WriteLine($"\tFats:  {fats,8:f3} g \n\tProts: {protein,8:f3} g \n\tCarbs: {carbs,8:f3} g");
    Console.WriteLine();
  }
}
