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
        List<Livro> GetTable(string PSQL_Text);
        void ExecuteQuery(string Query);
        void Update(string Name, string Author, int Pages, int Id, string table);
        void Insert(string Name, string Author, int Pages, string table);
        void Delete(int id, string table);
    }
}
