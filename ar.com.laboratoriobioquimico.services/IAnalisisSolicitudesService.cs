using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IAnalisisSolicitudesService
    {
        AnalisisSolicitudes find(string id);
        void persist(AnalisisSolicitudes entity);
        void update(AnalisisSolicitudes entity);
        void merge(AnalisisSolicitudes entity);
        string remove(AnalisisSolicitudes entity);
        IList<AnalisisSolicitudes> getAll(int? pageNumber, int? pageSize, IList<string> orderByDesc);
        long getMaxPage(int pageSize);
        long getTotalRegistros();        

        void updateBatch(AnalisisSolicitudes entity);
        void deleteBatch(IList<AnalisisSolicitudes> entities);

        IList<AnalisisSolicitudes> getListWithResults(int size);

        IList<Solicitudes> getListSolicitudesResults(int size);

        //IList<AnalisisSolicitudesInternacionItemsResultado> getSolicitudResults(string nrosolicitud);
        IList<AnalisisSolicitudesItemsResultado> getSolicitudResults(string nrosolicitud);

        IList<AnalisisSolicitudes> getListProtocolosPaciente(Pacientes paciente, IList<string> orderByDesc);

        Nbuinos getEquivalenciaPlantilla(string codigo);

        void saveNbuinos(List<Nbuinos> entities);
        IList<AnalisisSolicitudes> getListSolicitudesPaciente(string nrodoc);
        void saveSendMail(AnalisisSolicitudesSendResults entity);
        IList<AnalisisSolicitudesSendResults> getListSendResult(string nrosolicitud);
        IList<AnalisisSolicitudes> getListProtocolosFecha(string desde, string hasta, IList<string> orderByDesc);
        string getRutaSincro();
        IList<AnalisisSolicitudes> getAll(int? pageNumber, int? pageSize, Pacientes paciente, IList<string> orderByDesc);
        long getTotalRegistros(Pacientes paciente);
    }
}
