using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_CRUD
{
    internal class ConnectionDB_Mysql : IDatabase
    {
        private static string connectionString = "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql";

        private static MySqlConnection connection;
        private static DataTable dataTable;
        private static List<Livro> ListaLivrosTemporaria;

        DatabaseType IDatabase.DatabaseType => DatabaseType.MySQL;

        public ConnectionDB_Mysql()
        {
            dataTable = new DataTable();
            ListaLivrosTemporaria = new List<Livro>();
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetDBConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
        public void CloseDBConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        public void ExecuteQuery(string MYSQLQuery)
        {
            MySqlCommand cmd_Command = new MySqlCommand(MYSQLQuery, GetDBConnection());
            try
            {
                cmd_Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public List<Livro> GetTable(string MYSQL_Text)
        {
            MySqlConnection cn_connection = GetDBConnection();


            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM " + MYSQL_Text, cn_connection))
            {
                adapter.Fill(dataTable);
                try
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Livro livro = new Livro()
                        {
                            Id = (int)row["column_book_id"],
                            Name = (string)row["column_name"],
                            Author = (string)row["column_author"],
                            Pages = (int)row["column_pages"]
                        };
                        ListaLivrosTemporaria.Add(livro);
                    }
                }
                finally
                {
                    dataTable.Dispose();
                    CloseDBConnection();
                }
            }


            return ListaLivrosTemporaria;
        }

        public void Insert(string Name, string Author, int Pages, string table)
        {
            MySqlCommand cmd_Command = new MySqlCommand("call proc_insert ('" + Name + "', '" + Author + "'," + Pages + ") ", GetDBConnection());

            try
            {
                cmd_Command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            CloseDBConnection();
        }
        public void Delete(int id, string table)
        {
            MySqlCommand cmd_Command = new MySqlCommand("call proc_delete (" + id + ") ", GetDBConnection());

            try
            {
                cmd_Command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            CloseDBConnection();
        }
        public void Update(string Name, string Author, int Pages, int Id, string table)
        {
            MySqlCommand cmd_Command = new MySqlCommand("call proc_update ('" + Name + "', '" + Author + "'," + Pages + ", " + Id + ") ", GetDBConnection());

            try
            {
                cmd_Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            CloseDBConnection();
        }
    }
}
