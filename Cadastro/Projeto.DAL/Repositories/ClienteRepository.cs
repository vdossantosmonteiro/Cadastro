using System;
using System.Collections.Generic;
using System.Text;
using Projeto.DAL.Entities;
using Projeto.DAL.Contracts;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace Projeto.DAL.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private string connectionString;
        public ClienteRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
       
        public void Inserir(Cliente obj)
        {
            var query = "insert into Cliente(Nome, Email, DataCriacao) " +
                "values(@Nome, @Email, getdate())";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, obj);   
            }
        }

        public void Atualizar(Cliente obj)
        {
            var query = "update Cliente set Nome = @Nome, Email = @Email, DataCriacao = @DataCriacao" +
                "where IdCliente = @IdCliente";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, obj);
            }
        }

        public void Deletar(int id)
        {
            var query = "delete from Cliente where IdCliente = @IdCliente";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, new { IdCliente = id });
            }
        }

        public List<Cliente> ObterDados()
        {
            var query = "select*from Cliente";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                return conn.Query<Cliente>(query).ToList();
            }
        }

        public Cliente ObterPorId(int id)
        {
            var query = "select*from Cliente where IdCliente = @IdCliente";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                return conn.Query<Cliente>(query, new { IdCliente = id }).SingleOrDefault();
            }
        }
    }
}
