[Serializable]
public class Chicken : Meat
{
  public override decimal Price { get; set; }
  
  public Chicken()
  {}
  public Chicken(decimal price = 0, double mass = MASS) : base("chicken", mass)
  {
    Price = price;
    SetVitamins("A:0.016", "B:0.2", "C:2.3", "E:0.2", "D:0.0002");
    SetNutrition(3.1, 21.4, 0, 119);
    PrintInfo = consolePrintInfo;
  }


  public override void consolePrintInfo()
  {
    Console.WriteLine($"Chicken({Mass:f2} g):\n");
    Console.WriteLine($"Price: {Price:c}");
    PrintVitamins();
    PrintNutrition();
  }


  public override void SpecFunc()
  {
    int hula = 0;
                    {double A=0
                ,B=0,i,j;var z=new double[7040];var b=
             new char[1760];while(hula < 1000){memset(b,' ',1760);
            memset(z,0.0f, 7040);for(j=0;6.28>j;j+=0.04)for(
           i=0;6.28>i;i+=0.015){double c = Math.Sin(i);double d
          =Math.Cos(j);double e=Math.Sin(A);double f=Math.Sin(j);
         double g=Math.Cos(A);            double h=d+2;double D=1
         /(c*h*e+f*g+5);double            l=Math.Cos(i);double m=
         Math.Cos(B);double n             =Math.Sin(B);double t=
         c*h*g-f*e;int x=(int             )(40+30*D*(l*h*m-t*n));
         int y=(int)(12+15*D*             (l*h*n + t*m));int o=x
          +80*y;int N=(int)(8*           ((f*e-c*d*g)*m-c*d*e-f*
           g-l*d*n));if (22>y&&y>0&&x>0&&80>x&&D>z[o]){z[o]=D;b[o]
            =".,-~:;=!*#$@"[N>0?N:0];}}Console.Clear();nl(b);Console.
             Write(b);A+=0.04;B+=0.02;hula++;}}static void memset<T>
                (T[] buf,T val,int bufsz){if (buf==null)buf=
                   new T[bufsz];for(int i=0;i<bufsz;i++)buf
                     [i] = val;}static void nl(char[]b){
                        for(int i=80;1760>i;i+=80){b
                               [i]='\n';}}

  }
    
}


