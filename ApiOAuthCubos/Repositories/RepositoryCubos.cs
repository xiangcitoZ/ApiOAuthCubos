using ApiOAuthCubos.Data;
using ApiOAuthCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthCubos.Repositories
{
    public class RepositoryCubos
    {
        private CuboContext context;

        public RepositoryCubos(CuboContext context) 
        {
            this.context = context;
        }

        public async Task<UsuariosCubo> ExisteUsuario(string email, string password)
        {
            return await this.context.UsuarioCubo.FirstOrDefaultAsync
                (x => x.Email == email && x.Password == password);
                
        }

        private async Task<int> GetMaxUserAsync()
        {
            if (this.context.UsuarioCubo.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.UsuarioCubo.MaxAsync(x => x.IdUsuario) + 1;
            }
        }

        public async Task RegisterAsync(UsuariosCubo user)
        {
            UsuariosCubo newUser = new UsuariosCubo()
            {
                IdUsuario = await this.GetMaxUserAsync(),
                Nombre = user.Nombre,
                Email = user.Email,
                Password = user.Password,
                Imagen = user.Imagen
            };

            this.context.Add(newUser);
            await this.context.SaveChangesAsync();
        }

        //PARTE DE TIENDA

        private async Task<int> GetMaxPedidoAsync()
        {
            if (this.context.CompraCubos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.CompraCubos.MaxAsync(x => x.IdPedido) + 1;
            }
        }

        public async Task<List<Cubos>> GetProductosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

       

        public async Task<Cubos> FindProductoAsync(int id)
        {
            return await this.context.Cubos.FirstOrDefaultAsync(x => x.IdCubo == id);
        }

        public async Task<List<string>> GetMarcasAsync()
        {
            var consulta = (from datos in this.context.Cubos

                            select datos.Marca).Distinct();
            return await consulta.ToListAsync();
            
        }

        public async Task<List<Cubos>> GetCubosMarcasAsync(string marca)
        {

            return await this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
        }

        public async Task InsertarCubo(int idcubo,
           string nombre, string marca,string imagen, int precio)
        {
            Cubos pj = new Cubos();
            pj.IdCubo = idcubo;
            pj.Nombre = nombre;
            pj.Marca = marca;
            pj.Imagen = imagen;
            pj.Precio = precio;
            this.context.Cubos.Add(pj);
            await this.context.SaveChangesAsync();
        }


        public async Task<Cubos> FindCubosMarcasAsync(string marca)
        {

            return await this.context.Cubos.FirstOrDefaultAsync(x => x.Marca == marca);
        }

        //public async Task<List<VistaPedido>> GetPedidos(int idUser)
        //{
        //    return await this.context.Vistapedidos.Where(x => x.IdUsuario == idUser).ToListAsync();
        //}

        public async Task RegistrarPedidoAsync(CompraCubos pedido)
        {
            CompraCubos newPedido = new CompraCubos()
            {
                IdPedido = await this.GetMaxPedidoAsync(),
                IdCubo = pedido.IdCubo,
                IdUsuario = pedido.IdUsuario,
                FechaPedido = pedido.FechaPedido
            };

            this.context.Add(newPedido);
            await this.context.SaveChangesAsync();
        }


    }
}
