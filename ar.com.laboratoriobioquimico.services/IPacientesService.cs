using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IPacientesService
    {
        Pacientes find(string id);
        void persist(Pacientes entity);
        string remove(Pacientes entity);
        IList<Pacientes> getAll(int pageNumber, int pageSize);
        IList<Pacientes> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<Pacientes> entities);
        Pacientes findByNrodoc(string nrodoc);
        PacientesMails findPacienteMail(Pacientes entity);
        void saveMail(PacientesMails entity);
    }
}
