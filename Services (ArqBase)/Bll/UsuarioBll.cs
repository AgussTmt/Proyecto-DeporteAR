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
    /// <summary>
    /// Provee la lógica de negocio (BLL) estática para todas las operaciones
    /// relacionadas con la entidad <see cref="Usuario"/>.
    /// </summary>
    public static class UsuarioBll
    {
        private static IUsuarioRepository _usuarioRepository;

        private static readonly MailService _mailService = new GmailService();

        private static ILogger _logger;

        /// <summary>
        /// Constructor estático para inicializar las dependencias estáticas (Logger y Repositorio)
        /// una sola vez durante el ciclo de vida de la aplicación.
        /// </summary>
        static UsuarioBll()
        {
            _logger = LoggerService.GetLogger();
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Valida las credenciales de un usuario para el inicio de sesión.
        /// </summary>
        /// <param name="user">El nombre de usuario (o email) a validar.</param>
        /// <param name="password">La contraseña en **texto plano** a validar.</param>
        /// <returns>El objeto <see cref="Usuario"/> si la validación es exitosa.</returns>
        /// <exception cref="Exception">Lanza una excepción si el usuario/contraseña son incorrectos
        /// o si el usuario no está habilitado.</exception>
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

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">El objeto <see cref="Usuario"/> a registrar.</param>
        /// <exception cref="ArgumentNullException">Si el usuario es nulo.</exception>
        /// <exception cref="Exception">Si el repositorio no pudo registrar al usuario (ej: el ID sigue vacío).</exception>
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

        /// <summary>
        /// Obtiene una lista de todos los usuarios registrados en el sistema.
        /// </summary>
        /// <returns>Una <see cref="List{Usuario}"/>.</returns>
        public static List<Usuario> TraerUsuarios()
        {
            List<Usuario> usuarios = _usuarioRepository.GetAll();
            return usuarios;
        }

        /// <summary>
        /// Obtiene un usuario específico por su identificador único.
        /// </summary>
        /// <param name="id">El <see cref="Guid"/> del usuario.</param>
        /// <returns>El <see cref="Usuario"/> encontrado o <c>null</c>.</returns>
        public static Usuario GetById(Guid id)
        {
            Usuario usuario = _usuarioRepository.GetById(id);
            return usuario;
        }

        /// <summary>
        /// Inicia el proceso de recuperación de contraseña para un email dado.
        /// Genera un código, lo guarda en la BD y envía un email al usuario.
        /// </summary>
        /// <param name="email">El email del usuario que solicita la recuperación.</param>
        /// <returns>Siempre <c>true</c> si el envío es exitoso (o si el usuario no existe),
        /// <c>false</c> si el servicio de email falla.</returns>
        /// <remarks>
        /// Por seguridad (para evitar enumeración de usuarios), este método
        /// devuelve <c>true</c> incluso si el email no se encuentra en la base de datos,
        /// simulando un envío exitoso.
        /// </remarks>
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


        /// <summary>
        /// Completa el proceso de reseteo de contraseña.
        /// </summary>
        /// <param name="email">El email del usuario.</param>
        /// <param name="codigo">El código de 6 dígitos enviado por correo.</param>
        /// <param name="nuevoPassword">La nueva contraseña en **texto plano**.</param>
        /// <returns><c>true</c> si el reseteo fue exitoso, <c>false</c> si el email
        /// no existe, el código es incorrecto o el código ha expirado.</returns>
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
