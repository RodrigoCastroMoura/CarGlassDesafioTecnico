using CarGlassDesafioTecnico.Domain.Entities;
using CarGlassDesafioTecnico.Domain.Repositories.Sql;
using CarGlassDesafioTecnico.Infra.Persistence.Sql.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarGlassDesafioTecnico.Infra.Persistence.Sql.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext context;

        public UsuarioRepository(DataContext context)
        {
             this.context = context;
        }

        public async Task Add(Usuario usuario)
        {
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
 
        }

        public async Task<Usuario> Get(int id)
        {
            return await context.Usuarios.FindAsync(id);
        }


        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await context.Usuarios.ToListAsync();
        }


        public async Task UpdateAsync(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
            }
        }

    }
}
