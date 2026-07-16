namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class Users
    {
        public virtual long Id { get; set; }        
        public virtual string Usuario { get; set; }        
        public virtual string Pass { get; set; }                
        public virtual string Nombre { get; set; }
        public virtual string Observaciones { get; set; }
        public virtual bool Baja { get; set; }       
        public virtual Roles Rol { get; set; }
        
        public virtual string Pass1 { get; set; }
        public virtual string Pass2 { get; set; }

        public virtual string Entidad { get; set; }
        public virtual string EntidadPie { get; set; }
    }
}