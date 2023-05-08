using ApiOAuthCubos.Models;
using ApiOAuthCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiOAuthCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private RepositoryCubos repo;

        public UsuariosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }
        
        
        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult<UsuariosCubo>> PerfilUsuario()
        {
           
            Claim claim = HttpContext.User.Claims
                .SingleOrDefault(x => x.Type == "USERDATA");
            string jsonUsuario =
                claim.Value;
            UsuariosCubo usuario = JsonConvert.DeserializeObject<UsuariosCubo>
                (jsonUsuario);
            return usuario;
        }



    }
}
