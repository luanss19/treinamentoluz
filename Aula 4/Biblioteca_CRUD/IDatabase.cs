using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_CRUD
{
    public interface IDatabase
    {
        DatabaseType DatabaseType { get; }
        void CloseDBConnection();
        List<Livro> GetTable(string TableName);
        int ExecuteQuery(string Query);
        int Update(string Name, string Author, int Pages, int Id, string table);
        int Insert(string Name, string Author, int Pages, string table);
        void Delete(int id, string table);
        int ExecuteScalar(string Query);
    }
}
