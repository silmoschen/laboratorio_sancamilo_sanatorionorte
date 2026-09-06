using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class PacientesService: IPacientesService
    {
        private IPacientesDao entityDao { get; set; }
        private IPacientesMailsDao pacientemailDao { get; set; }
        static Utiles utiles = new Utiles();

        public Pacientes find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(Pacientes entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(Pacientes entity)
        {
            return entityDao.remove(entity);
        }

        public IList<Pacientes> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<Pacientes> getList(string find, int pageNumber, int pageSize)
        {
            return entityDao.getList(find, pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return entityDao.getMaxPage(pageSize);
        }

        public long getTotalRegistros()
        {
            return entityDao.getTotalRegistros();
        }

        public void updateBatch(IList<Pacientes> entities)
        {
            foreach (Pacientes c in entities)
                if (entityDao.get(c.codpac) == null) entityDao.save(c); else entityDao.merge(c);

        }
        public Pacientes findByNrodoc(string nrodoc)
        {
            return entityDao.getByDocumento(nrodoc);
        }

        public PacientesMails findPacienteMail(Pacientes entity)
        {
            return pacientemailDao.get(entity);
        }

        public void saveMail(PacientesMails entity)
        {
            PacientesMails c = findPacienteMail(entity.paciente);
            if (c == null)
            {
                c = new PacientesMails();
                c.id = utiles.guiid();
                c.paciente = entity.paciente;
                c.email = entity.email;
                pacientemailDao.save(c);
            }
            else
            {
                c.email = entity.email;
                pacientemailDao.update(c);
            }
        }
    }
}