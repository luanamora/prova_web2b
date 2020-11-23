using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Produto
{
    public class PostgresContext
    {
        public NpgsqlConnection openConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=msspsql09pass;Database=produtodb;");
            conn.Open();
            return conn;
        }

        public void closeConnection(NpgsqlConnection conn)
        {
            conn.Close();
        }

        public NpgsqlDataReader runQuery(String query, NpgsqlConnection conn)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            try
            {
                NpgsqlDataReader rd = cmd.ExecuteReader();
                return rd;
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("Erro " + e);
            }
            return null;
        }
    }
}
