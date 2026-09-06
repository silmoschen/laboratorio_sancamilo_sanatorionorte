using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IUsersDao : IGenericDao<Users, long>
    {
        IList<Users> getAll(int pageNumber, int pageSize);
        IList<Users> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);        
        Users findUser(string us, string pas);
        Users findUser(string us);
        IList<Users> getListUsers(string campo, string valor, string orden);
        long getTotalRegistros();
    }
}
