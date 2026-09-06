using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAnalisisSolicitudesInternacionDao : IGenericDao<AnalisisSolicitudesInternacion, string>
    {
        IList<AnalisisSolicitudesInternacion> getAll(int? pageNumber, int? pageSize, PacientesInternacion paciente, string desde, string hasta, IList<string> orderByDesc);
        long getMaxPage(int pageSize);
        long getTotalRegistros();      
        IList<AnalisisSolicitudesInternacion> getListWithResults(int size);
        List<object[]> getListSolicitudesResults(int size);
        List<object[]> getSolicitudResults(string nrosolicitud);
        List<object[]> getListSolicitudesPaciente(string codpac);
    }
}
