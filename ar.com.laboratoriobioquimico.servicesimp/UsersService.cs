using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class UsersService : IUsersService
    {
        private IUsersDao entityDao { get; set; }
        private IRolesDao rolDao { get; set; }

        public Users find(long id)
        {
            return entityDao.get(id);
        }

        public void persist(Users entity)
        {
            entityDao.persist(entity);
        }

        public void update(Users entity)
        {
            entityDao.update(entity);
        }

        public string remove(Users entity)
        {
            return entityDao.remove(entity);
        }

        public IList<Users> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<Users> getList(string find, int pageNumber, int pageSize)
        {
            return entityDao.getList(find, pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return entityDao.getMaxPage(pageSize);
        }

        public IList<Roles> getListRoles()
        {
            return rolDao.getAll(0, 5000);
        }

        public Roles findRolById(long id)
        {
            return rolDao.get(id);
        }       

        public Users getUser(string user, string pass)
        {
            Users us = null;

            us = entityDao.findUser(user, pass);
            if (us != null)
                if (us.Baja) us = null;

            return us;
        }

        public Users findUser(string user)
        {
            return entityDao.findUser(user);
        }

        public long getTotalRegistros()
        {
            return entityDao.getTotalRegistros();
        }

    }
}