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
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtienePorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentas);
    }
    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string _configuration;

        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DefaultConnection");
        }


        public async  Task Crear(TipoCuenta tipoCuenta)
        {
            using (var connection = new SqlConnection(_configuration))
            {
                var id = await connection.QuerySingleAsync<int>("TiposCuentas_Insertar",
                    new
                    {
                        Nombre = tipoCuenta.Nombre,
                        UsuarioId = tipoCuenta.UsuarioId
                    }, commandType: System.Data.CommandType.StoredProcedure);

                tipoCuenta.Id = id;
            }
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using (var connection = new SqlConnection(_configuration))
            {
                var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                                                    @"select 1 from tiposCuentas
                                                                    where nombre=@Nombre and UsuarioId=@UsuarioId",
                                                                    new { nombre,usuarioId});

                return existe == 1;
            }

        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using (var connection = new SqlConnection(_configuration))
            {
                return await connection.QueryAsync<TipoCuenta>(@"select Id,Nombre,Orden 
                                                                from TiposCuentas
                                                                where UsuarioId=@UsuarioId
                                                                order by Orden",
                                                                new { usuarioId});
            }
        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using (var connection = new SqlConnection(_configuration))
            {
                await connection.ExecuteAsync(@"Update TiposCuentas
                                                        set Nombre=@Nombre
                                                        where Id=@Id",
                                                               tipoCuenta);
            }
        }

        public async Task<TipoCuenta> ObtienePorId(int id,int usuarioId)
        {
            using (var connection = new SqlConnection(_configuration))
            {
                return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"select Id,Nombre,Orden
                                                                               from TiposCuentas
                                                                                where Id=@id and UsuarioId=@UsuarioId",
                                                                new {id, usuarioId });
            }
        }

        public async Task Borrar(int id)
        {
            using (var connection = new SqlConnection(_configuration))
            {
                 await connection.ExecuteAsync(@"Update TiposCuentas where Id=@id",
                                                                new { id });
            }
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentas)
        {
            var query = "UPDATE TiposCuentas set Orden=@Orden where Id=@Id";
            using (var connection = new SqlConnection(_configuration))
            {
                await connection.ExecuteAsync(query,tipoCuentas);
            }
        }
    }
}
