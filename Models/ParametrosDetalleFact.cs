using System.Collections.Generic;

namespace laboratoriobioquimico.Models
{
    public class ParametrosDetalleFact
    {
        public virtual string periodo { get; set; }
        public virtual List<string> obrassociales { get; set; }
        public virtual List<string> profesionales { get; set; }

        public ParametrosDetalleFact()
        {
            obrassociales = new List<string>();
            profesionales = new List<string>();
        }
    }
}