using ApiOAuthCubos.Models;
using ApiOAuthCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiOAuthCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Cubos>>> Productos()
        {
            List<Cubos> list = await this.repo.GetProductosAsync();
            return list;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Cubos>> Producto(int id)
        {
            Cubos producto = await this.repo.FindProductoAsync(id);
            return producto;
        }


        [HttpGet]
        [Route("[action]/{marca}")]
        public async Task<ActionResult<List<Cubos>>> Marca(string marca)
        {
            List<Cubos> list = await this.repo.GetCubosMarcasAsync(marca);
            return list;
        }

        [HttpGet]
        [Route("[action]/{marca}")]
        public async Task<ActionResult<Cubos>> EncontrarMarca(string marca)
        {
            Cubos producto = await this.repo.FindCubosMarcasAsync(marca);
            return producto;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<string>>> Marca()
        {
            List<string> list = await this.repo.GetMarcasAsync();
            return list;
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RealizarPedido(CompraCubos pedido)
        {
            await this.repo.RegistrarPedidoAsync(pedido);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult>
         InsertarCubo(Cubos cubo)
        {
            await this.repo.InsertarCubo(cubo.IdCubo, cubo.Nombre, cubo.Marca,
                cubo.Imagen, cubo.Precio);
            return Ok();
        }




    }
}
