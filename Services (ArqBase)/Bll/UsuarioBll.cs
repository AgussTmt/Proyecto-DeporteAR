using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel;
using Services.Facade;
using Services__ArqBase_.Bll;
using Services__ArqBase_.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public static class UsuarioBll
    {
        private static IUsuarioRepository _usuarioRepository;

        private static readonly MailService _mailService = new GmailService();

        private static ILogger _logger;

        static UsuarioBll()
        {
            _logger = LoggerService.GetLogger();
            _usuarioRepository = new UsuarioRepository();
        }

        public static Usuario ValidarCredenciales(string user, string password)
        {
            password = CryptographyService.HashMd5(password);

            Usuario usuario = _usuarioRepository.GetByCredentials(user, password);

            if (usuario == null)
            {
                //Escribir nuestra regla de negocio como exception

                throw new Exception("Usuario o contraseña incorrectos.");
            }
            else if (!usuario.Habilitado)
            {
                //Escribir nuestra regla de negocio como exception
                throw new Exception("Usuario no habilitado.");
            }

            return usuario;
        }

        public static void RegistrarUsuario(Usuario usuario)
        {
            //Hacer validaciones previas antes de registrar el usuario
            if (usuario == null)
            {
                //Escribir nuestra regla de negocio como exception
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
            }

            _usuarioRepository.RegistrarUsuario(usuario);

            if(usuario.IdUsuario == Guid.Empty)
            {
                //Escribir nuestra regla de negocio como exception
                throw new Exception("El usuario no pudo ser registrado.");
            }    
        }

        public static List<Usuario> TraerUsuarios()
        {
            List<Usuario> usuarios = _usuarioRepository.GetAll();
            return usuarios;
        }

        public static Usuario GetById(Guid id)
        {
            Usuario usuario = _usuarioRepository.GetById(id);
            return usuario;
        }


        public static bool SolicitarRecuperacion(string email)
        {
            Usuario usuario = _usuarioRepository.GetByEmail(email);

            if (usuario == null)
            { 
                return true;
            }

            Random random = new Random();
            string codigo = random.Next(100000, 999999).ToString();

            DateTime expiracion = DateTime.Now.AddMinutes(15);

            _usuarioRepository.SaveRecoveryCode(usuario, codigo, expiracion);


            string asunto = "DeporteAR - Código de Recuperación de Contraseña";
            string cuerpo = $"Hola {usuario.Nombre},\n\n" +
                              $"Tu código para recuperar la contraseña es: {codigo}\n\n" +
                              "Este código expirará en 15 minutos.\n\n" +
                              "Si no solicitaste esto, ignora este correo.";

            List<string> destinatarios = new List<string> { usuario.Email };

            try
            {
                _mailService.sendMail(asunto, cuerpo, destinatarios);
                return true;
            }
            catch (Exception ex)
            {

                _logger.Error($"Error enviando email a {usuario.Email} del usuario {usuario.Nombre}; {DateTime.Now} "
                    + "\n" + 
                    $"Error :{ex.Message}");
                return false;
            }
        }


        public static bool ResetearPassword(string email, string codigo, string nuevoPassword)
        {
            Usuario usuario = _usuarioRepository.GetByEmail(email);
            if (usuario == null)
            {
                return false;
            }

            // 2. Verificamos el código Y la expiración
            if (usuario.CodigoRecuperacion != codigo || DateTime.Now > usuario.CodigoExpiracion)
            {
                return false;
            }

            string passwordHasheada = CryptographyService.HashMd5(nuevoPassword);

            _usuarioRepository.UpdatePassword(usuario, passwordHasheada);

            _usuarioRepository.CleanRecoveryCode(usuario);

            return true;
        }


        public static Usuario GetByEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email);
        }

    }
}
