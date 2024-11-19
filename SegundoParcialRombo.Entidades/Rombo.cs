namespace SegundoParcialRombo.Entidades
{
 public class Rombo
 {
   public int DiagonalMayor {get; set;}
   public int DiagonalMenor {get; set;}

   public Contorno Tipocontorno {get; set;}

   public Rombo(int diagonalmayor, int diagonalmenor, Contorno tipoContorno)
   {
     if (diagonalmayor <= 0 || diagonalmenor <= 0)
            {
                throw new ArgumentException("Las diagonales deben ser mayores que cero.");
            }

             double DiagonalMayor = diagonalmayor;
             double DiagonalMenor = diagonalmenor;
            
          }

        public Rombo()
        {
        }

        public double Lado
        {
            get
            {
                return Math.Sqrt((Math.Pow(DiagonalMayor, 2) + Math.Pow(DiagonalMenor, 2)) / 4);
            }
        } 
   public double CalcularPerimetro()
   {
    return (DiagonalMayor + DiagonalMenor)/2;
   }
   
   public double CalcularArea()
   {
     return 4 * Lado;
   }

   public override string ToString()
        {
            return $"Rombo [Diagonal Mayor: {DiagonalMayor}, Diagonal Menor: {DiagonalMenor}, Contorno : {Tipocontorno}]";
        }



   

 }
}