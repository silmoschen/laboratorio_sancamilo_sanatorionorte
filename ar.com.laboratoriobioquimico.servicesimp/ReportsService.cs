using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.rest.Models;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class ReportsService : IReportsService
    {
        private List<Reports> list = null;

        IAnalisisSolicitudesService solicitudService { get; set; }
        IAnalisisSolicitudesInternacionService solicitudinternacionService { get; set; }
        IParametrosService parametroService { get; set; }
        IQueryService queryService { get; set; }
        INbuService nbuService { get; set; }
        INomenclaturaService nomeclaService { get; set; }
        INbuInternacionService nbuinternacionService { get; set; }
        IObrasSocialesInternacionService obrasocialInternacionService { get; set; }
        IObrasSocialesService obrasocialService { get; set; }

        static Utiles utiles = new Utiles();

        public List<Reports> getProtocoloResult(List<AnalisisSolicitudes> entities)
        {
            string vn = "";

            list = new List<Reports>();

            Parametros parametro = parametroService.find(1);

            foreach (AnalisisSolicitudes protocolo in entities)
            {
                foreach (AnalisisSolicitudesItems linea in protocolo.practicas)
                {
                    if (/*linea.cargado != null &&*/ linea.Select)
                    { // Si tiene resultados
                        vn = "";
                        foreach (AnalisisSolicitudesItemsResultado result in linea.resultadoitem)
                        {
                            //System.Diagnostics.Debug.WriteLine("pasa 1 " + result.ID.items);
                            // Columna 1
                            if (result.plantilla != null)
                            {
                                if (result.plantilla.itemsparalelo == null || result.plantilla.itemsparalelo.Equals(""))
                                {
                                    //System.Diagnostics.Debug.WriteLine("pasa 2 " + result.ID.items);
                                    Reports item = new Reports();
                                    item.Id = Convert.ToInt64(result.solicituditem.solicitud.nrosolicitud);
                                    item.Col1 = result.solicituditem.determinacioncodigo;
                                    item.Col2 = result.solicituditem.determinacion;
                                    item.Col3 = result.plantilla.elemento;
                                    item.Col4 = result.resultado;
                                    item.Col5 = result.valoresn;
                                    item.Titulo10 = result.solicituditem.solicitud.paciente.nombre;
                                    item.Titulo2 = result.solicituditem.solicitud.nrosolicitud;
                                    item.Titulo3 = result.solicituditem.solicitud.fecha1;
                                    if (result.solicituditem.solicitud.profesional != null) item.Titulo4 = result.solicituditem.solicitud.profesional.nombre;
                                    if (parametro != null) item.Titulo1 = parametro.Texto1;

                                    if (result.observaciones != null) item.Col7 = result.observaciones;

                                    item.Col10 = result.ID.items;

                                    vn = result.ID.codigo;

                                    list.Add(item);
                                }
                                else
                                {
                                    if (result.plantilla.itemsparalelo.Equals("00")) // Valores Normales
                                    {
                                        foreach (Reports ip in list)
                                            if (ip.Col1.Equals(vn))
                                            {
                                                ip.Col9 = result.plantilla.elemento + " " + result.plantilla.resultado;
                                            }

                                    }

                                    // Columna 2
                                    foreach (Reports ip in list)
                                    {
                                        if (!result.plantilla.itemsparalelo.Equals("00"))
                                        {
                                            if (ip.Col1.Equals(vn))
                                            {
                                                if (ip.Col10.Equals(result.plantilla.itemsparalelo))
                                                {
                                                    ip.Col5 = result.plantilla.elemento;
                                                    if (result.resultado != null) ip.Col6 = result.resultado;
                                                    if (result.valoresn != null)
                                                        if (ip.Col6 == null) ip.Col6 = result.valoresn;

                                                    if (result.valoresn != null)
                                                        if (ip.Col6.Equals("")) ip.Col6 = result.valoresn;

                                                    if (result.observaciones != null) ip.Col7 = result.observaciones;

                                                    break;
                                                }
                                            }
                                        }

                                    }

                                }
                            }
                        }

                    }
                }
            }

            //System.Diagnostics.Debug.WriteLine("pasa " + list.Count);

            return list;
        }


        public List<Reports> getProtocoloResult(string id, IList<AnalisisSolicitudesItems> filter)
        {
            AnalisisSolicitudes entity = solicitudService.find(id);

            foreach (AnalisisSolicitudesItems c in filter)
            {
                foreach (AnalisisSolicitudesItems t in entity.practicas)
                {
                    if (t.ID.items.Equals(c.ID.items))
                    {
                        t.Select = c.Select;
                        break;
                    }
                }
            }

            List<AnalisisSolicitudes> lista = new List<AnalisisSolicitudes>();
            lista.Add(entity);

            return getProtocoloResult(lista);
        }


        //===========================================================================================================

        public List<Reports> getProtocoloResultInternacion(string id, IList<AnalisisSolicitudesInternacionItems> filter)
        {

            AnalisisSolicitudesInternacion entity = solicitudinternacionService.find(id);

            foreach (AnalisisSolicitudesInternacionItems c in filter)
            {
                foreach (AnalisisSolicitudesInternacionItems t in entity.practicas)
                {
                    if (t.ID.items.Equals(c.ID.items))
                    {
                        t.Select = c.Select;
                        break;
                    }
                }
            }

            List<AnalisisSolicitudesInternacion> lista = new List<AnalisisSolicitudesInternacion>();
            lista.Add(entity);

            return getProtocoloResultInternacion(lista);
        }

        public List<Reports> getProtocoloResultInternacion(List<AnalisisSolicitudesInternacion> entities)
        {
            string vn = "";

            list = new List<Reports>();

            Parametros parametro = parametroService.find(1);

            foreach (AnalisisSolicitudesInternacion protocolo in entities)
            {
                foreach (AnalisisSolicitudesInternacionItems linea in protocolo.practicas)
                {
                    if (/*linea.cargado != null &&*/ linea.Select)
                    { // Si tiene resultados
                        vn = "";
                        foreach (AnalisisSolicitudesInternacionItemsResultado result in linea.resultadoitem)
                        {
                            //System.Diagnostics.Debug.WriteLine("pasa 1 " + result.ID.items);
                            // Columna 1
                            if (result.plantilla != null)
                            {
                                if (result.plantilla.itemsparalelo == null || result.plantilla.itemsparalelo.Equals(""))
                                {
                                    //System.Diagnostics.Debug.WriteLine("pasa 2 " + result.ID.items);
                                    Reports item = new Reports();
                                    item.Id = Convert.ToInt64(result.solicituditem.solicitud.nrosolicitud);
                                    item.Col1 = result.solicituditem.determinacioncodigo;
                                    item.Col2 = result.solicituditem.determinacion;
                                    item.Col3 = result.plantilla.elemento;
                                    item.Col4 = result.resultado;
                                    item.Col5 = result.valoresn;
                                    item.Titulo10 = result.solicituditem.solicitud.paciente.nombre;
                                    item.Titulo2 = result.solicituditem.solicitud.nrosolicitud;
                                    item.Titulo3 = result.solicituditem.solicitud.fecha1;
                                    if (result.solicituditem.solicitud.profesional != null) item.Titulo4 = result.solicituditem.solicitud.profesional.nombre;
                                    if (parametro != null) item.Titulo1 = parametro.Texto1;

                                    if (result.observaciones != null) item.Col7 = result.observaciones;

                                    item.Col10 = result.ID.items;

                                    vn = result.ID.codigo;

                                    list.Add(item);
                                }
                                else
                                {
                                    if (result.plantilla.itemsparalelo.Equals("00")) // Valores Normales
                                    {
                                        foreach (Reports ip in list)
                                            if (ip.Col1.Equals(vn))
                                            {
                                                ip.Col9 = result.plantilla.elemento + " " + result.plantilla.resultado;
                                            }

                                    }

                                    // Columna 2
                                    foreach (Reports ip in list)
                                    {
                                        if (!result.plantilla.itemsparalelo.Equals("00"))
                                        {
                                            if (ip.Col1.Equals(vn))
                                            {
                                                if (ip.Col10.Equals(result.plantilla.itemsparalelo))
                                                {
                                                    ip.Col5 = result.plantilla.elemento;
                                                    if (result.resultado != null) ip.Col6 = result.resultado;
                                                    if (result.valoresn != null)
                                                    {
                                                        if (ip.Col6 == null) ip.Col6 = result.valoresn;
                                                        if (ip.Col6 == "") ip.Col6 = result.valoresn;
                                                    }

                                                    if (result.observaciones != null) ip.Col7 = result.observaciones;

                                                    break;
                                                }
                                            }
                                        }

                                    }

                                }
                            }
                        }

                    }
                }
            }

            //System.Diagnostics.Debug.WriteLine("pasa " + list.Count);

            return list;
        }

        //=======================================================================================================

        public List<Reports> getFacturacionPeriodo(IList<FacturacionDetalleFact> entities)
        {
            list = new List<Reports>();

            foreach (FacturacionDetalleFact c in entities)
            {
                Reports item = new Reports();
                item.Col1 = c.obrasocial;
                item.Col2 = c.codos;
                item.Monto1 = c.monto;
                item.Titulo1 = "Facturación Obras Sociales - Período: " + c.periodo;

                list.Add(item);
            }

            return list;
        }

        //==========================================================================================================

        public List<Reports> getCantidadPacienteInternadosMes(DateTime desde, DateTime hasta)
        {
            list = new List<Reports>();

            var result = queryService.getListCantidadPacientesInternadosMes(utiles.fechaAAAAMMDD(desde), utiles.fechaAAAAMMDD(hasta));

            foreach (object[] row in result)
            {
                Reports item = new Reports();
                item.Col1 = row[1].ToString();
                item.Long1 = Convert.ToInt32(row[0].ToString());
                item.Long2 = queryService.getDeterminacionesInternacion(row[2].ToString()).Value;

                item.Titulo1 = "Estadística de Pacientes Ingresados - INTERNACIÓN - Período: " + desde.ToString("dd/MM/yyyy") + " - " + hasta.ToString("dd/MM/yyyy");

                list.Add(item);
            }

            return list;
        }

        public List<Reports> getCantidadPacienteAmbulatorioMes(DateTime desde, DateTime hasta)
        {
            list = new List<Reports>();

            var result = queryService.getListCantidadPacientesAmbulatorioMes(utiles.fechaAAAAMMDD(desde), utiles.fechaAAAAMMDD(hasta));

            foreach (object[] row in result)
            {
                Reports item = new Reports();
                item.Col1 = row[1].ToString();
                item.Long1 = Convert.ToInt32(row[0].ToString());
                item.Long2 = queryService.getDeterminacionesAmbulatorio(row[2].ToString()).Value;

                item.Titulo1 = "Estadística de Pacientes Ingresados - AMBULATORIO - Período: " + desde.ToString("dd/MM/yyyy") + " - " + hasta.ToString("dd/MM/yyyy");

                list.Add(item);
            }

            return list;
        }

        //==========================================================================================================

        public List<Reports> getListCantidadPracticasInternacion(DateTime desde, DateTime hasta)
        {
            list = new List<Reports>();

            var result = queryService.getListCantidadPacientesInternadosMes(utiles.fechaAAAAMMDD(desde), utiles.fechaAAAAMMDD(hasta));

            foreach (object[] row in result)
            {
                var rs = queryService.getListPracticasInternacion(row[2].ToString());

                foreach (object[] r in rs)
                {                    
                    Reports item = new Reports();
                    item.Col1 = row[1].ToString();
                    item.Long1 = Convert.ToInt32(row[0].ToString());
                    item.Long2 = queryService.getDeterminacionesInternacion(row[2].ToString(), r[0].ToString()).Value;
                    item.Col2 = r[0].ToString();

                    Nomenclatura i = null;
                    NbuInternacion c = null;

                    Nbu n = nbuService.find(item.Col2);
                    if (n != null) item.Col3 = n.descrip;
                    if (n == null)
                    {
                        i = nomeclaService.find(item.Col2);
                        if (i != null) item.Col3 = i.descrip;                      
                    }
                    if (n == null && i == null)
                    {
                        c = nbuinternacionService.find(item.Col2);
                        if (c != null) item.Col3 = c.descrip;
                    }

                    item.Titulo1 = "Estadística de Determinaciones - INTERNACIÓN - Período: " + desde.ToString("dd/MM/yyyy") + " - " + hasta.ToString("dd/MM/yyyy");

                    list.Add(item);
                }
            }

            return list;
        }

        public List<Reports> getListCantidadPracticasAmbulatorio(DateTime desde, DateTime hasta)
        {
            list = new List<Reports>();

            var result = queryService.getListCantidadPacientesAmbulatorioMes(utiles.fechaAAAAMMDD(desde), utiles.fechaAAAAMMDD(hasta));

            foreach (object[] row in result)
            {
                var rs = queryService.getListPracticasAmbulatorio(row[2].ToString());

                foreach (object[] r in rs)
                {

                    Reports item = new Reports();
                    item.Col1 = row[1].ToString();
                    item.Long1 = Convert.ToInt32(row[0].ToString());
                    item.Long2 = queryService.getDeterminacionesAmbulatorio(row[2].ToString(), r[0].ToString()).Value;
                    item.Col2 = r[0].ToString();

                    Nomenclatura i = null;
                    NbuInternacion c = null;

                    Nbu n = nbuService.find(item.Col2);
                    if (n != null) item.Col3 = n.descrip;
                    if (n == null)
                    {
                        i = nomeclaService.find(item.Col2);
                        if (i != null) item.Col3 = i.descrip;
                    }
                    if (n == null && i == null)
                    {
                        c = nbuinternacionService.find(item.Col2);
                        if (c != null) item.Col3 = c.descrip;
                    }

                    item.Titulo1 = "Estadística de Determinaciones - AMBULATORIO - Período: " + desde.ToString("dd/MM/yyyy") + " - " + hasta.ToString("dd/MM/yyyy");

                    list.Add(item);
                }
            }

            return list;
        }

        public List<Reports> getListObrasSocialesInternacion(DateTime desde, DateTime hasta)
        {
            list = new List<Reports>();

            var result = queryService.getListCantidadPacientesInternadosMes(utiles.fechaAAAAMMDD(desde), utiles.fechaAAAAMMDD(hasta));

            foreach (object[] row in result)
            {
                var rs = queryService.getListObrasSocialesInternadosMes(row[2].ToString());

                foreach (object[] r in rs)
                {
                    Reports item = new Reports();
                    item.Col1 = row[1].ToString();                  
                    item.Col2 = r[0].ToString();

                    ObrasSocialesInternacion c = obrasocialInternacionService.find(item.Col2);
                    if (c != null) item.Col3 = c.nombre;

                    item.Long1 = queryService.getListPracticasObrasSocialesInternadosMes(row[2].ToString(), item.Col2).Value;

                    item.Titulo1 = "Estadística de Det. por Obra Social - INTERNACIÓN - Período: " + desde.ToString("dd/MM/yyyy") + " - " + hasta.ToString("dd/MM/yyyy");

                    list.Add(item);
                }
            }

            return list;
        }

        public List<Reports> getListObrasSocialesAmbulatorio(DateTime desde, DateTime hasta)
        {
            list = new List<Reports>();

            var result = queryService.getListCantidadPacientesAmbulatorioMes(utiles.fechaAAAAMMDD(desde), utiles.fechaAAAAMMDD(hasta));

            foreach (object[] row in result)
            {
                var rs = queryService.getListObrasSocialesAmbulatorioMes(row[2].ToString());

                foreach (object[] r in rs)
                {
                    Reports item = new Reports();
                    item.Col1 = row[1].ToString();
                    item.Col2 = r[0].ToString();

                    ObrasSociales c = obrasocialService.find(item.Col2);
                    if (c != null) item.Col3 = c.nombre;

                    item.Long1 = queryService.getListPracticasObrasSocialesAmbulatorioMes(row[2].ToString(), item.Col2).Value;

                    item.Titulo1 = "Estadística de Det. por Obra Social - AMBULATORIO - Período: " + desde.ToString("dd/MM/yyyy") + " - " + hasta.ToString("dd/MM/yyyy");

                    list.Add(item);
                }
            }

            return list;
        }
    }
}