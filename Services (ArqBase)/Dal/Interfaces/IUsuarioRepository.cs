using System.Collections.Generic;
using Services.DomainModel;

namespace Services.Bll
{
    internal interface IUsuarioRepository
    {
        void RegistrarUsuario(Usuario usuario);
        Usuario GetByCredentials(string user, string password);

        public List<Usuario> GetAll();
    }
}