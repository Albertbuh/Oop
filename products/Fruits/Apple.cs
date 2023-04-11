[Serializable]
public class Apple: Fruit
{
  public string Sort { get; set; }
  public decimal Price { get; set; }
  
  public Apple() : this("Undefiend", 0)
  {}
  public Apple(string sort, decimal price = 0, double mass = MASS) : base("apple", mass)
  {
    Sort = sort;
    Price = price *(decimal)msc;
    SetVitamins( "A:0.005", "B:0.05", "C:10", "E:0.2");
    SetNutrition(0.4, 0.4, 9.8, 47);
    PrintInfo = consolePrintInfo;
  }

  public override void consolePrintInfo()
  {
    Console.WriteLine($"apple sort: {Sort}({Mass:f2} g)\n");
    Console.WriteLine($"Price: {Price:C2}\n");
    PrintVitamins();
    Console.WriteLine("\nNutrition:");
    PrintNutrition(); 
  }

  public override void SpecFunc()
  {
    string text = """
       <element attr = "content">
           <body style="normal">
                                                   .
                                  .OO
                                .OOOO
                               .OOOO'
                               OOOO'          .-~~~~-.
                               OOO'          /   (o)(o)
                       .OOOOOO `O .OOOOOOO. /      .. |
                   .OOOOOOOOOOOO OOOOOOOOOO/\    \____/
                 .OOOOOOOOOOOOOOOOOOOOOOOO/ \\   ,\_/
                .OOOOOOO%%OOOOOOOOOOOOO(#/\     /.
               .OOOOOO%%%OOOOOOOOOOOOOOO\ \\  \/OO.
              .OOOOO%%%%OOOOOOOOOOOOOOOOO\   \/OOOO.
              OOOOO%%%%OOOOOOOOOOOOOOOOOOO\_\/\OOOOO
              OOOOO%%%OOOOOOOOOOOOOOOOOOOOO\###)OOOO
              OOOOOO%%OOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
              OOOOOOO%OOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
              `OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO'
            .-~~\OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO'
           / _/  `\(#\OOOOOOOOOOOOOOOOOOOOOOOOOOOO'
          / / \  / `~~\OOOOOOOOOOOOOOOOOOOOOOOOOO'
         |/'  `\//  \\ \OOOOOOOOOOOOOOOOOOOOOOOO'
                `-.__\_,\OOOOOOOOOOOOOOOOOOOOO'
               jgs  `OO\#)OOOOOOOOOOOOOOOOOOO'
                      `OOOOOOOOO''OOOOOOOOO'
           </body>
       </element >
       """;
    Console.WriteLine(text);
  }
}

