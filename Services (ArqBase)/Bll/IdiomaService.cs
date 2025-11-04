using Services.Dal;
using Services.DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    /// <summary>
    /// Capa de servicio Singleton (BLL) para gestionar la lógica de negocio de la internacionalización (i18n).
    /// Actúa como intermediario con el <see cref="IdiomaRepository"/>.
    /// </summary>
    public sealed class IdiomaService
	{
		private readonly static IdiomaService _instance = new IdiomaService();

        /// <summary>
        /// Obtiene la instancia única (Singleton) del servicio de idiomas.
        /// </summary>
        public static IdiomaService Current
		{
			get
			{
				return _instance;
			}
		}
        /// <summary>
        /// Constructor privado para implementar el patrón Singleton.
        /// </summary>
        private IdiomaService()
		{
            //Implent here the initialization of your singleton
        }

        /// <summary>
        /// Intenta traducir una clave (palabra) al idioma de la cultura actual.
        /// </summary>
        /// <param name="word">La clave (key) que se desea traducir (ej: 'WelcomeMessage').</param>
        /// <returns>El string traducido, si se encuentra.
        /// El mismo<paramref name="word"/>(la clave) si no se encuentra.
        /// </returns>
        /// <remarks>
        /// ¡Comportamiento importante! Si la traducción falla porque la clave no existe
        /// (<see cref="WordNotFoundException"/>), este método automáticamente
        /// llamará a <c>IdiomaRepository.Current.AgregarDataKey(word)</c> para
        /// añadir la clave faltante al archivo de idioma, facilitando el desarrollo.
        /// </remarks>
        public string Traducir(string word)
		{
			try
			{
                return IdiomaRepository.Current.Traducir(word);
            }
            catch (WordNotFoundException ex) 
			{

				IdiomaRepository.Current.AgregarDataKey(word);
                Console.WriteLine(ex.Message);

				return word;
			}
		}

    }

}
