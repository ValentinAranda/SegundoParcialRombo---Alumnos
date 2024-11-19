using SegundoParcialRombo.Entidades;

namespace SegundoParcialRombo.Datos
{

    public class RepositorioRombo
    {
        private List<Rombo> rombos;
        private string? nombreArchivo = "Rombos.txt";
        private string? rutaProyecto = Environment.CurrentDirectory;
        private string? rutaCompletaArchivo;

        public void AgregarRombo(Rombo rombo)
        {
            rombos.Add(rombo);
        }

        public void EliminarRombo(Rombo rombo)
        {
            rombos.Remove(rombo);
        }

        public bool Existe(Rombo rombo)
        {
            return rombos.Any(e => e.DiagonalMenor == rombo.DiagonalMenor &&
                e.DiagonalMayor == rombo.DiagonalMayor && e.Tipocontorno == rombo.Tipocontorno);
        }

        public List<Rombo>? Filtrar(Contorno contornoSeleccionado)
        {
            return rombos.Where(e => e.Tipocontorno == contornoSeleccionado).ToList();
        }

        public int GetCantidad(Contorno? contornoSeleccionado=null)
        {
            if(contornoSeleccionado == null)
                return rombos.Count;
            return rombos.Count(e=>e.Tipocontorno==contornoSeleccionado);
        }

        public List<Rombo> ObtenerRombo()
        {
            return new List<Rombo>(rombos);
        }

        public List<Rombo>? OrdenarArriba()
        {
            return rombos.OrderBy(e => e.CalcularArea()).ToList();
            //Usé el area como método para ordenarlos porque no me dejaba usar "Lado"
        }

        public List<Rombo>? OrdenarAbajo()
        {
            return rombos.OrderByDescending(e => e.CalcularArea()).ToList();
            //mismo que arriba
        }

        public bool Existe(int Dma, int Dme)
        {
            return rombos.Any(e => e.DiagonalMayor == Dma &&
            e.DiagonalMenor == Dme);
        }
        public void AlmacenarDatos()
        {
            rutaCompletaArchivo = Path.Combine(rutaProyecto, nombreArchivo);
            using (var escritor = new StreamWriter(rutaCompletaArchivo))
            {
                foreach (var rombo in rombos)
                {
                    string linea = ConstruirLinea(rombo);
                    escritor.WriteLine(linea);
                }
            }
        }
        private string ConstruirLinea(Rombo rombo)
        {
            return $"{rombo.DiagonalMayor}|{rombo.DiagonalMenor}|{rombo.Tipocontorno.GetHashCode()}|";
        }
        private List<Rombo> LeerDatos()
        {
            var listaRombos=new List<Rombo>();
            rutaCompletaArchivo = Path.Combine(rutaProyecto, nombreArchivo);
            if (!File.Exists(rutaCompletaArchivo))
            {
                return listaRombos;
            }
            using (var lector = new StreamReader(rutaCompletaArchivo))
            {
                while (!lector.EndOfStream)
                {
                    string? linea=lector.ReadLine();
                    Rombo? rombo = ConstruirRombo(linea);
                    listaRombos.Add(rombo!);
                }
            }
            return listaRombos;
        }
        private Rombo? ConstruirRombo(string? linea)
        {
            var campos = linea!.Split('|');
            var Dma = int.Parse(campos[0]);
            var Dme = int.Parse(campos[1]);
            var tipoContorno = (Contorno)int.Parse(campos[2]);
            return new Rombo(Dma,Dme,tipoContorno);
        }
    }
}
