using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IQueryService
    {
        IList<object[]> getListAnalisisAmbulatorioDia(string desde, string hasta);
        IList<object[]> getListAnalisisInternacionDia(string desde, string hasta);
        IList<object[]> getListAnalisisAmbulatorioMes(string desde, string hasta);
        IList<object[]> getListAnalisisInternacionMes(string desde, string hasta);
        List<string> getListProfesionales();
        IList<object[]> getListCantidadPacientesInternadosMes(string desde, string hasta);
        IList<object[]> getListCantidadPacientesAmbulatorioMes(string desde, string hasta);
        long? getDeterminacionesInternacion(string fecha);
        long? getDeterminacionesAmbulatorio(string fecha);
        IList<object[]> getListPracticasInternacion(string periodo);
        long? getDeterminacionesInternacion(string fecha, string codigo);
        IList<object[]> getListPracticasAmbulatorio(string periodo);
        long? getDeterminacionesAmbulatorio(string fecha, string codigo);
        IList<object[]> getListObrasSocialesInternadosMes(string periodo);
        long? getListPracticasObrasSocialesInternadosMes(string periodo, string codos);
        IList<object[]> getListObrasSocialesAmbulatorioMes(string periodo);
        long? getListPracticasObrasSocialesAmbulatorioMes(string periodo, string codos);

    }
}
