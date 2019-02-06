using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core.Adapter.Internal;
using Dapper;
using RESTXI1.DML;
using RESTXI1.Models;

namespace RESTXI1.Services
{
    public class ClientServices
    {
        private SqlConnection _Conn = new SqlConnection();

        public Cliente GetClientById(int id)
        {
            _Conn = GetSqlConneciont();
            _Conn.Open();
            var cliente = _Conn.Query<Cliente>("SELECT * FROM Cliente").Where(f => f.id == id).ToList();
            return cliente.Count() != 0 ? cliente.First() : null;
        }

        public IEnumerable<Cliente> GetClients()
        {
            _Conn = GetSqlConneciont();
            _Conn.Open();
            var clientes = _Conn.Query<Cliente>("SELECT * FROM Cliente").ToList();
            return clientes;
        }
        public async Task<IEnumerable<Cliente>> GetClientsAsync()
        {
            _Conn = GetSqlConneciont();
            _Conn.Open();
            var clientes = await _Conn.QueryAsync<Cliente>("SELECT * FROM Cliente");
            return clientes;
        }

        public void AddClient(Cliente client)
        {
            try
            {
                _Conn = GetSqlConneciont();
                _Conn.Open();
                var strInsert = DMLGenerator.CreateInsertStatement(client);
                var clientes = _Conn.Execute(strInsert, client);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateClient(Cliente client, int id)
        {
            try
            {
                _Conn = GetSqlConneciont();
                _Conn.Open();
                Dictionary<string, string> whereClause = new Dictionary<string, string>();
                whereClause.Add("id", id.ToString());
                var strUpdate = DMLGenerator.CreateUpdateStatement(client, whereClause);
                _Conn.Execute(strUpdate, client);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteClient(int id)
        {
            try
            {
                _Conn = GetSqlConneciont();
                _Conn.Open();
                Dictionary<string, string> whereClause = new Dictionary<string, string>();
                whereClause.Add("id", id.ToString());
                var strUpdate = DMLGenerator.CreateDeleteStatement(new Cliente(), whereClause);
                _Conn.Execute(strUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static SqlConnection GetSqlConneciont()
        {
            return new SqlConnection(@"Data Source=localhost;Initial Catalog=ClientesIX;Integrated Security=True;Pooling=False");
        }
    }
}
