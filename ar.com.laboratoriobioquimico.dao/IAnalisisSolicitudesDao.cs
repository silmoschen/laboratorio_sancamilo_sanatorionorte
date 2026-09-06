using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAnalisisSolicitudesDao : IGenericDao<AnalisisSolicitudes, string>
    {
        IList<AnalisisSolicitudes> getAll(int? pageNumber, int? pageSize, Pacientes paciente, string desde, string hasta, IList<string> orderByDesc);
        long getMaxPage(int pageSize);
        long getTotalRegistros();      
        IList<AnalisisSolicitudes> getListWithResults(int size);
        List<object[]> getListSolicitudesResults(int size);
        List<object[]> getSolicitudResults(string nrosolicitud);
        List<object[]> getListSolicitudesPaciente(string codpac);
        long getTotalRegistros(Pacientes paciente);
    }
}
