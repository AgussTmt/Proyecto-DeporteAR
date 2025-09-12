using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel;

namespace Services.DomainModel
{
    public class Usuario
    {       
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        private string password;

        /// <summary>
        /// Para gestionar el patrón composite debemos utilizar una lista de Component
        /// </summary>
        ///
        public List<Component> Privilegios { get; set; }

        /// <summary>
        /// Generar recursividad sobre el composite para obtener el menú de opciones
        /// </summary>
        public List<Patente> Patentes
        {
            get
            {
                List<Patente> patentes = new List<Patente>();
                RecorrerFamilias(patentes, Privilegios, true);
                return patentes;
            }
        }

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
        /// Recorre las familias y patentes de un usuario
        /// </summary>
        /// <param name="patentes">Lista de patentes</param>
        /// <param name="componentes">Lista de componentes que se recorren</param>
        private void RecorrerFamilias(List<Patente> patentes, List<Component> componentes, bool habilitadoPadre)
        {
            foreach (var componente in componentes)
            {
               
                    bool esHabilitado = componente.Habilitado && habilitadoPadre;
                    if (componente is Patente patente)
                    {
                        patente.Habilitado = esHabilitado;
                        if (!patentes.Exists(p => p.Id == patente.Id))
                            patentes.Add(patente);
                    }
                    else if (componente is Familia familia)
                    {
                        RecorrerFamilias(patentes, familia.GetHijos(), esHabilitado);
                    }

            }
        }



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

        public bool Habilitado { get; set; }

        public Usuario(string nombre, string email, string password, bool habilitado = true)
        {
            Nombre = nombre;
            Email = email;
            Password = password;
            Habilitado = habilitado;
        }

        public Usuario(Guid idUsuario, string nombre, string email, string password, bool habilitado = true) : this(nombre, email, password, habilitado)
        {
            IdUsuario = idUsuario;
        }

        public Usuario(Guid idUsuario, string nombre, string email, bool habilitado = true)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Email = email;
            Habilitado = habilitado;   
        }
    }
}

