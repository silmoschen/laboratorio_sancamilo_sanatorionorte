using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles
{
    public class PaginadorGenerico<T> where T : class
    {
        public int PaginaActual { get; set; }
        public int RegistrosPorPagina { get; set; }
        public long TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public IList<T> Resultado { get; set; }
    }
}