using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class Parametros
    {
        public virtual long Id { get; set; }
        public virtual string Parametro1 { get; set; }
        public virtual string Parametro2 { get; set; }
        public virtual string Parametro3 { get; set; }
        public virtual string Parametro4 { get; set; }
        public virtual string Parametro5 { get; set; }
        public virtual string Parametro6 { get; set; }
        public virtual string Parametro7 { get; set; }
        public virtual string Parametro8 { get; set; }
        public virtual string Parametro9 { get; set; }
        public virtual string Parametro10 { get; set; }
        public virtual string Parametro11 { get; set; }
        [AllowHtml]
        [UIHint("tinymce_full")]  
        public virtual string Texto1 { get; set; }
        public virtual string Texto2 { get; set; }
        public virtual string Texto3 { get; set; }
        public virtual string Texto4 { get; set; }
        public virtual string Texto5 { get; set; }
        public virtual string Texto6 { get; set; }
        public virtual string Texto7 { get; set; }
        public virtual string Texto8 { get; set; }
        public virtual string Texto9 { get; set; }
        public virtual string Texto10 { get; set; }
        public virtual string Titulo1 { get; set; }
        public virtual string Titulo2 { get; set; }
        public virtual string Titulo3 { get; set; }
        public virtual string Titulo4 { get; set; }
        public virtual string Titulo5 { get; set; }
        public virtual string Titulo6 { get; set; }
        public virtual string Titulo7 { get; set; }
        public virtual string Titulo8 { get; set; }
        public virtual string Titulo9 { get; set; }
        public virtual string Titulo10 { get; set; }
    }
}