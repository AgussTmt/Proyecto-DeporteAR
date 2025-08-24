using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Category Categoria { get; set; }

        /// <summary>
        /// Quién quiere este dato?
        /// </summary>
        private decimal _precio;

        public decimal Precio
        {
            get { return _precio; }
            set {                
                _precio = value; 
                _encriptedPrice = CryptographyService.Encrypt(value.ToString("0.00"));
            }
        }

        /// <summary>
        /// Este lo quiere la DAL
        /// </summary>
        private string _encriptedPrice;

        public string EncriptedPrice
        {
            get { return _encriptedPrice; }
            private set { _encriptedPrice = value; }
        }

        public Product(string encriptedPrice)
        {
            _precio = CryptographyService.Decrypt(encriptedPrice) != null ? Convert.ToDecimal(CryptographyService.Decrypt(encriptedPrice)) : 0;
            _encriptedPrice = encriptedPrice;
        }
    }
}
