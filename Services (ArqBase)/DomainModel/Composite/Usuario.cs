using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel;

namespace Services.DomainModel
{

    /// <summary>
    /// Representa la entidad principal del Usuario en el sistema.
    /// Contiene la información de perfil, credenciales y la jerarquía de permisos (Privilegios).
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public Guid IdUsuario { get; set; }

        /// <summary>
        /// Nombre y apellido del usuario.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Email del usuario, generalmente usado para el login.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Campo privado que almacena el hash de la contraseña.
        /// </summary>
        private string password;

        /// <summary>
        /// Código temporal (token) utilizado para el proceso de recuperación de contraseña.
        /// </summary>
        public string CodigoRecuperacion { get; set; }

        /// <summary>
        /// Fecha y hora de expiración del <see cref="CodigoRecuperacion"/>.
        /// </summary>
        public DateTime? CodigoExpiracion { get; set; }

        /// <summary>
        /// Lista raíz de componentes (Patrón Composite) asignados directamente al usuario.
        /// Puede contener tanto <see cref="Familia"/> (roles) como <see cref="Patente"/> (permisos individuales).
        /// </summary>
        public List<Component> Privilegios { get; set; }

        /// <summary>
        /// Indica si la cuenta de usuario está habilitada para iniciar sesión.
        /// </summary>
        public bool Habilitado { get; set; }

        /// <summary>
        /// Obtiene una lista "plana" y única de todas las <see cref="Patente"/> que posee el usuario,
        /// resueltas recursivamente desde la jerarquía de <see cref="Privilegios"/>.
        /// </summary>
        /// <remarks>
        /// Esta propiedad es calculada y recorre todo el árbol de permisos cada vez que se accede.
        /// </remarks>
        public List<Patente> Patentes
        {
            get
            {
                List<Patente> patentes = new List<Patente>();
                RecorrerFamilias(patentes, Privilegios, true);
                return patentes;
            }
        }

        /// <summary>
        /// Propiedad de ayuda (calculada) que devuelve un string con las DataKey
        /// de todas las patentes habilitadas, separadas por comas.
        /// </summary>
        public string PatentesAsignadas
        {
            get
            {
                var patentes = Patentes; 
                var nombresPatentes = patentes.Where(p => p.Habilitado)
                    .Select(p => p.DataKey);
                return string.Join(", ", nombresPatentes);
            }
        }


        /// <summary>
        /// Propiedad de ayuda (calculada) que devuelve un string con los Nombres
        /// de todas las <see cref="Familia"/> (roles) directamente asignadas y habilitadas, separadas por comas.
        /// </summary>
        /// <remarks>
        /// A diferencia de 'Patentes', esta propiedad no es recursiva.
        /// </remarks>
        public string RolesAsignados
        {
            get
            {
                var familias = Privilegios
                .OfType<Familia>()            
                .Where(f => f.Habilitado)  
                .ToList();


                var nombresFamilias = familias.Select(f => f.Nombre);

                return string.Join(", ", nombresFamilias);
            }
        }


        /// <summary>
        /// Motor recursivo (privado) para "aplanar" la jerarquía de componentes (Composite)
        /// en una lista única de patentes, calculando el estado 'Habilitado' efectivo.
        /// </summary>
        /// <param name="patentes">Lista de patentes (pasada por referencia) que se va poblando.</param>
        /// <param name="componentes">La lista de componentes (hijos) del nivel actual a recorrer.</param>
        /// <param name="habilitadoPadre">El estado de habilitación del componente padre,
        /// usado para deshabilitar en cascada.</param>
        private void RecorrerFamilias(List<Patente> patentes, List<Component> componentes, bool habilitadoPadre)
        {
            foreach (var componente in componentes)
            {
               
                    bool esHabilitado = componente.Habilitado && habilitadoPadre;
                    if (componente is Patente patente)
                    {

                          var patenteExistente = patentes.FirstOrDefault(p => p.Id == patente.Id);

                    if (patenteExistente == null)
                    {
                        
                        var nuevaPatente = new Patente();
                        nuevaPatente.DataKey = patente.DataKey;
                        nuevaPatente.Id = patente.Id;
                        nuevaPatente.TipoAcceso = patente.TipoAcceso;
                        nuevaPatente.Habilitado = esHabilitado;
                        patentes.Add(nuevaPatente);
                    }
                    else
                    {
                        
                        patenteExistente.Habilitado = patenteExistente.Habilitado || esHabilitado;
                    }
                }
                    else if (componente is Familia familia)
                    {
                        RecorrerFamilias(patentes, familia.GetHijos(), esHabilitado);
                    }

            }
        }


        /// <summary>
        /// Obtiene el hash MD5 de la contraseña.
        /// El 'set' (setter) toma un string en texto plano y lo convierte
        /// automáticamente a su hash MD5.
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = CryptographyService.HashMd5(value);
            }
        }


        /// <summary>
        /// Constructor para un nuevo usuario. El ID se generará en otro lado.
        /// Hashea automáticamente la contraseña.
        /// </summary>
        public Usuario(string nombre, string email, string password, bool habilitado = true)
        {
            Nombre = nombre;
            Email = email;
            Password = password;
            Habilitado = habilitado;
        }

        /// <summary>
        /// Constructor para un usuario completo.
        /// </summary>
        public Usuario(Guid idUsuario, string nombre, string email, string password, bool habilitado = true) : this(nombre, email, password, habilitado)
        {
            IdUsuario = idUsuario;
        }


        /// <summary>
        /// Constructor para mapear un usuario sin información de contraseña (ej: actualización de perfil).
        /// </summary>
        public Usuario(Guid idUsuario, string nombre, string email, bool habilitado = true)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Email = email;
            Habilitado = habilitado;   
        }

        /// <summary>
        /// Constructor para mapear un usuario durante el proceso de recuperación de contraseña.
        /// </summary>
        public Usuario(Guid idUsuario, string nombre, string email, string codigoRecuperacion, DateTime codigoExpiracion, bool habilitado = true)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Email = email;
            Habilitado = habilitado;
            CodigoRecuperacion = codigoRecuperacion;
            CodigoExpiracion = codigoExpiracion;
        }


    }
}

