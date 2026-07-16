using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class PacientesInternacionService: IPacientesInternacionService
    {
        private IPacientesInternacionDao entityDao { get; set; }
        private IPacientesInternacionMailsDao pacientemailDao { get; set; }
        static Utiles utiles = new Utiles();

        public PacientesInternacion find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(PacientesInternacion entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(PacientesInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public IList<PacientesInternacion> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<PacientesInternacion> getList(String find, int pageNumber, int pageSize)
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

        public void updateBatch(IList<PacientesInternacion> entities)
        {
            foreach (PacientesInternacion c in entities)
                if (entityDao.get(c.codpac) == null) entityDao.save(c); else entityDao.merge(c);

        }

        public PacientesInternacion findByNrodoc(string nrodoc)
        {
            return entityDao.getByDocumento(nrodoc);
        }
        public PacientesInternacionMails findPacienteMail(PacientesInternacion entity)
        {
            return pacientemailDao.get(entity);
        }

        public void saveMail(PacientesInternacionMails entity)
        {
            PacientesInternacionMails c = findPacienteMail(entity.paciente);
            if (c == null)
            {
                c = new PacientesInternacionMails();
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