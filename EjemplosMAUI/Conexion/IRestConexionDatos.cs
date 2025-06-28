using EjemplosMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemplosMAUI.Conexion
{
    public interface IRestConexionDatos
    {
        Task<List<Plato>> GetPlatosAsync();
        Task AddPltoAsync(Plato plato);
        Task UpdatePlatoAsync(Plato plato);
        Task DeletePlatoAsync(int id);
    }
}
