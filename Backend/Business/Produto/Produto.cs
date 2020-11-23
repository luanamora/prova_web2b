using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Produto
{
    public class Produto
    {
        public bool findData(String query)
        {
            PostgresContext context = new PostgresContext();
            NpgsqlConnection conn = context.openConnection();
            NpgsqlDataReader datareader = context.runQuery(query, conn);
            bool hasData = datareader.Read();
            datareader.Close();
            context.closeConnection(conn);
            return hasData;
        }
    }
}
