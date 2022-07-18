using System;
using MySqlConnector;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_CRUD
{
    public class DatabseFactory
    {
        public DatabseFactory() { 
        }

        public IDatabase getDatabase(DatabaseType dbtype, string connectionStr)
        {
            IDatabase database;

                switch (dbtype)
                {
                    case DatabaseType.MySQL:
                    database = new ConnectionDB_Mysql(new MySqlConnection(connectionStr));
                        break;
                    case DatabaseType.Postgres:
                    database = new ConnectionDB_Postgres(new NpgsqlConnection(connectionStr));
                        break;
                    default:
                        throw new DatabaseDriverNotImplementedException("Banco de Dados ainda não implementado");
               }

            return database;
        }
    }
}
