using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System.Web.Http;

namespace laboratoriobioquimico.Controllers
{
    public class RestLoginController : ApiController
    {
        IProfesionalesUsersService entityService = (IProfesionalesUsersService)SpringContext.Instance.GetObject("ProfesionalesUsersService");
 
        [HttpPost, ActionName("login")]
        public ProfesionalesUsers Login([FromBody]ProfesionalesUsers entity)
        {
            return entityService.findUser(entity.user, entity.pass);
        }
    }
}
