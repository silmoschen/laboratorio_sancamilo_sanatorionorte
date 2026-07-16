namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.rest.Models
{
    public class FacturacionDetalleFact
    {
        public virtual string codpac { get; set; }
        public virtual string nombre { get; set; }
        public virtual string codanalisis { get; set; }
        public virtual string profiva { get; set; }
        public virtual string osiva { get; set; }
        public virtual string ref1 { get; set; }
        public virtual string retiva { get; set; }
        public virtual double monto { get; set; }
        public virtual double iva { get; set; }
        public virtual double exento { get; set; }
        public virtual string nroauditoria { get; set; }
        public virtual string nroafiliado { get; set; }
        public virtual string nroautorizacion { get; set; }
        public virtual string fecha { get; set; }
        public virtual string obrasocial { get; set; }
        public virtual string profesional { get; set; }

        public virtual string periodo { get; set; }
        public virtual string idprof { get; set; }
        public virtual string codos { get; set; }
        public virtual string orden { get; set; }
        public virtual string items { get; set; }
    }
}