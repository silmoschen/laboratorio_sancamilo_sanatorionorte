using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IAnalisisSolicitudesInternacionService
    {
        AnalisisSolicitudesInternacion find(string id);
        void persist(AnalisisSolicitudesInternacion entity);
        void update(AnalisisSolicitudesInternacion entity);
        void merge(AnalisisSolicitudesInternacion entity);
        string remove(AnalisisSolicitudesInternacion entity);
        IList<AnalisisSolicitudesInternacion> getAll(int? pageNumber, int? pageSize, IList<string> orderByDesc);
        long getMaxPage(int pageSize);
        long getTotalRegistros();        

        void updateBatch(AnalisisSolicitudesInternacion entity);
        void deleteBatch(IList<AnalisisSolicitudesInternacion> entities);

        IList<AnalisisSolicitudesInternacion> getListWithResults(int size);

        IList<Solicitudes> getListSolicitudesResults(int size);

        IList<AnalisisSolicitudesInternacionItemsResultado> getSolicitudResults(string nrosolicitud);

        IList<AnalisisSolicitudesInternacion> getListProtocolosPaciente(PacientesInternacion paciente, IList<string> orderByDesc);

        NbuinosInternacion getEquivalenciaPlantilla(string codigo);

        void saveNbuinos(List<NbuinosInternacion> entities);
        IList<AnalisisSolicitudesInternacion> getListSolicitudesPaciente(string nrodoc);
        void saveSendMail(AnalisisSolicitudesInternacionSendResults entity);
        IList<AnalisisSolicitudesInternacionSendResults> getListSendResult(string nrosolicitud);
        IList<AnalisisSolicitudesInternacion> getListProtocolosFecha(string desde, string hasta, IList<string> orderByDesc);

        void saveSenddcm4che(AnalisisSolicitudesInternacionSenddcm4che entity);
        IList<AnalisisSolicitudesInternacionSenddcm4che> getListSenddcm4che(string nrosolicitud);

        string getRutaSincro();
    }
}
