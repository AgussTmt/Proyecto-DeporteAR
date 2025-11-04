using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dal.Tools;
using Services.Dal.Interfaces;

namespace Services__ArqBase_.Dal.Implementations
{
    /// <summary>
    /// Provee un repositorio (basado en reflexión) para operaciones de actualización
    /// en tablas de unión (Join Tables).
    /// </summary>
    public class UpdateGenericRepository
    {
        /// <summary>
        /// Invierte el estado 'Habilitado' (de 1 a 0, o de 0 a 1) para un registro específico
        /// en una tabla de unión.
        /// </summary>
        /// <param name="Ent1">La primera entidad de la relación (ej: un Usuario).</param>
        /// <param name="Ent2">La segunda entidad de la relación (ej: una Patente o Familia).</param>
        /// <remarks>
        /// ¡PRECAUCIÓN! Este método utiliza reflexión para construir dinámicamente la consulta SQL.
        /// Asume que la tabla de unión se llama 'Ent1.Name + Ent2.Name' (ej: "UsuarioPatente")
        /// y que las columnas de ID siguen la convención "Id + Ent.Name" (ej: "IdUsuario", "IdPatente").
        /// Es sensible a cambios en los nombres de las clases o convenciones de la base de datos.
        /// </remarks>
        public void UpdateHabilitadoJoin(object Ent1, object Ent2)
        {
            Type typeEnt1 = Ent1.GetType();
            Type typeEnt2 = Ent2.GetType();

            object id1 = typeEnt1.Name == "Usuario" ?
            typeEnt1.GetProperty("IdUsuario").GetValue(Ent1) :
            typeEnt1.GetProperty("Id").GetValue(Ent1);

            object id2 = typeEnt2.GetProperty("Id").GetValue(Ent2);


            string Statement = $"UPDATE {typeEnt1.Name}{typeEnt2.Name} SET Habilitado = 1 - Habilitado WHERE Id{typeEnt1.Name} = @Id1 AND Id{typeEnt2.Name} = @Id2";


            SqlHelper.ExecuteNonQuery(Statement, CommandType.Text, new SqlParameter("@Id1", id1),
            new SqlParameter("@Id2", id2));

        }
    }
}
