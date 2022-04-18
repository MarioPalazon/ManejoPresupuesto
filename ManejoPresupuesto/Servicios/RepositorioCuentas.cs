using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioCuentas
    {
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta cuenta);
    }

    public class RepositorioCuentas:IRepositorioCuentas
    {
        private readonly string _configuration;

        public RepositorioCuentas(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Cuenta cuenta)
        {
            using (var connection = new SqlConnection(_configuration))
            {
                var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Cuentas (Nombre,TipoCuentaId,Descripcion,Balance)
                                                                Values(@Nombre,@TipoCuentaId,@Descripcion,@Balance)

                                                                 Select SCOPE_IDENTITY();", cuenta);

                cuenta.Id = id;
                
            }
        }

        public async Task<IEnumerable<Cuenta>>Buscar(int usuarioId)
        {
            using (var connection = new SqlConnection(_configuration))
            {
                return await connection.QueryAsync<Cuenta>(@"select 
                                                                    c.Id,
                                                                    c.Nombre,
                                                                    c.Balance,
                                                                    tc.Nombre as TipoCuenta 
                                                                    from Cuentas as c
                                                                    join TiposCuentas as tc on tc.Id=c.TipoCuentaId
                                                                    where tc.UsuarioId=@UsuarioId
                                                                    order by tc.Orden
                                                                    ",new { usuarioId});

                

            }
        }

    }

}
