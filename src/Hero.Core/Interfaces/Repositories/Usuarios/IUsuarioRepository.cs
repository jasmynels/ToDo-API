using Core.Entities.Usuarios;

namespace Core.Interfaces.Repositories.Usuarios
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        bool EmailExiste(string email);
        Usuario GetUserByEmail(string email);
    }
}
