using System;
using System.Collections.Generic;
using Services.DomainModel;

namespace Services.Bll
{
    internal interface IUsuarioRepository
    {
        void RegistrarUsuario(Usuario usuario);
        Usuario GetByCredentials(string user, string password);

        public List<Usuario> GetAll();

        public Usuario GetById(Guid id);
        Usuario GetByEmail(string email);
        void UpdatePassword(Usuario usuario, string passwordHasheada);
        void CleanRecoveryCode(Usuario usuario);
        void SaveRecoveryCode(Usuario usuario, string codigo, DateTime expiracion);
    }
}