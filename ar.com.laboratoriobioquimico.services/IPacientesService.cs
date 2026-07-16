using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IPacientesService
    {
        Pacientes find(string id);
        void persist(Pacientes entity);
        string remove(Pacientes entity);
        IList<Pacientes> getAll(int pageNumber, int pageSize);
        IList<Pacientes> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<Pacientes> entities);
        Pacientes findByNrodoc(string nrodoc);
        PacientesMails findPacienteMail(Pacientes entity);
        void saveMail(PacientesMails entity);
    }
}
