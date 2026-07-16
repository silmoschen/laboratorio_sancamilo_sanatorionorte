using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.rest.Models;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IReportsService
    {        
        List<Reports> getProtocoloResult(List<AnalisisSolicitudes> entities);
        List<Reports> getProtocoloResult(string id, IList<AnalisisSolicitudesItems> filter);
        List<Reports> getProtocoloResultInternacion(List<AnalisisSolicitudesInternacion> entities);
        List<Reports> getProtocoloResultInternacion(string id, IList<AnalisisSolicitudesInternacionItems> filter);
        List<Reports> getFacturacionPeriodo(IList<FacturacionDetalleFact> entities);
        List<Reports> getCantidadPacienteInternadosMes(DateTime desde, DateTime hasta);
        List<Reports> getCantidadPacienteAmbulatorioMes(DateTime desde, DateTime hasta);
        List<Reports> getListCantidadPracticasInternacion(DateTime desde, DateTime hasta);
        List<Reports> getListCantidadPracticasAmbulatorio(DateTime desde, DateTime hasta);
        List<Reports> getListObrasSocialesInternacion(DateTime desde, DateTime hasta);
        List<Reports> getListObrasSocialesAmbulatorio(DateTime desde, DateTime hasta);
    }
}
