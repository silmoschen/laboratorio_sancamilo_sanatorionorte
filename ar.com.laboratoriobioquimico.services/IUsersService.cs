using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IUsersService
    {
        Users find(long id);
        void persist(Users entity);
        void update(Users entity);
        string remove(Users entity);
        IList<Users> getAll(int pageNumber, int pageSize);
        IList<Users> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
       
        IList<Roles> getListRoles();
        Roles findRolById(long id);

        Users getUser(string user, string pass);
        Users findUser(string user);

        long getTotalRegistros();
    }
}
