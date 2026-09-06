using System.Collections.Generic;
using System.Web.Http;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.Controllers
{
    public class RestRolesController : ApiController
    {
        IRolesService entityService = (IRolesService)SpringContext.Instance.GetObject("RolesService");

        [HttpGet, ActionName("getall")]
        public IList<Roles> Get(int key1, int key2)
        {
            return entityService.getAll(0, 0);
        }

    }
}
