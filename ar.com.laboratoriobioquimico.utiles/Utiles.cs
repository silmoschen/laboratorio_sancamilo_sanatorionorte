using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles
{
    public class Utiles
    {
        public Boolean validarEmail(String email)
        {
            Boolean r = false;
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    r = true;
                }
                else
                {
                    r = false;
                }
            }
            else
            {
                r = false;
            }

            return r;
        }
        
        public string fechaCompleta()
        {
            DateTime date = DateTime.Now;
            string f = date.ToString("dd-MM-yyyy");
            return "Hoy es " + date.ToString("D", new CultureInfo("es-ES"));
        }

        public string fechaCompleta(DateTime date)
        {
            return date.ToString("D", new CultureInfo("es-ES"));
        }

        public string fechaAAAA_MM_DD(DateTime date)
        {
            string res = "";

            string f = date.ToString();
            
            res = f.Substring(6, 4) + "-" + f.Substring(3, 2) + "-" + f.Substring(0, 2);
            
            return res;
        }

        public string fechaAAAAMMDD(DateTime date)
        {
            string res = "";

            string f = date.ToString("dd/MM/yyy");

            res = f.Substring(6, 4) + f.Substring(3, 2) + f.Substring(0, 2);

            return res;
        }

        public DateTime getPrimerFechaMes(DateTime fechatemp)
        {
            return new DateTime(fechatemp.Year, fechatemp.Month, 1);
        }

        public DateTime getUltimaFechaMes(DateTime fechatemp)
        {
            int anio = fechatemp.Year;
            int mes = fechatemp.Month + 1;
            if (mes < 1) mes = 1;
            if (mes > 12)
            {
                mes = 1;
                anio = anio + 1;
            }
            return new DateTime(anio, mes, 1).AddDays(-1);
        }

        public string sanearURL(string c)
        {
            string r = c;
            r = r.Replace("#", "%23");
            r = r.Replace("?", "%3f");
            r = r.Replace("&", "%26");
            return r;            
        }

        public string guiid()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }

        public string version()
        {
            string fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            fecha = fecha.Replace("/", "");
            fecha = fecha.Replace(":", "");
            fecha = fecha.Replace(" ", "");
            return fecha;
        }

    }
}