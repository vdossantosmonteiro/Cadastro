using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Collections.ObjectModel;

namespace Projeto.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {

        private readonly string connectionString;
        public ClienteRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BancoWpf"].ConnectionString;
        }

        public void Insert(Cliente obj)
        {
            var query = "insert into Cliente(Nome, Email, DataCriacao) " +
               "values(@Nome, @Email, @DataCriacao)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Delete(int id)
        {
            var query = "delete from Cliente where IdCliente = @IdCliente";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new {IdCliente = id });
            }
        }

        

        public ObservableCollection<Cliente> SelectAll()
        {
            var query = "Select * from Cliente";

            using(var connection = new SqlConnection(connectionString))
            {
                return new ObservableCollection<Cliente>(connection.Query<Cliente>(query));
            }
        }

        public Cliente SelectById(int id)
        {
            var query = "select * from Cliente where IdCliente = @IdCliente";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Cliente>(query, new { IdCliente = id }).SingleOrDefault();
            }
        }

        public void Update(Cliente obj)
        {
            var query = "update Cliente set Nome = @Nome, Email = @Email, DataCriacao = @DataCriacao " +
                "where IdCliente = @IdCliente";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }
    }
}
