using System;
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

        public IDatabase getDatabase(DatabaseType dbtype)
        {
            IDatabase database;

                switch (dbtype)
                {
                    case DatabaseType.MySQL:
                        database = new ConnectionDB_Mysql();
                        break;
                    case DatabaseType.Postgres:
                        database = new ConnectionDB_Postgres();
                        break;
                    default:
                        throw new DatabaseDriverNotImplementedException("Banco de Dados ainda não implementado");
               }

            return database;
        }
    }
}
