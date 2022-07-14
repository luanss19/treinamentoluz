using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace Biblioteca_CRUD
{
    public class ConnectionDB_Postgres : IDatabase
    {
        private static string connectionString = @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres";

        private static NpgsqlConnection connection;
        private static DataTable dataTable;
        private static List<Livro> ListaLivrosTemporaria;

        DatabaseType IDatabase.DatabaseType => DatabaseType.Postgres;

        public ConnectionDB_Postgres()
        {
            dataTable = new DataTable();
            ListaLivrosTemporaria = new List<Livro>();
            connection = new NpgsqlConnection(connectionString);
        }

        public NpgsqlConnection GetDBConnection()
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
        public List<Livro> GetTable(string PSQL_Text)
        {
            NpgsqlConnection cn_connection = GetDBConnection();


                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM " + PSQL_Text, cn_connection))
                {
                    adapter.Fill(dataTable);
                    try
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            Livro livro = new Livro()
                            {
                                Id = (int)row["col_book_id"],
                                Name = (string)row["col_name"],
                                Author = (string)row["col_author"],
                                Pages = (int)row["col_pages"]
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

        public  void ExecuteQuery(string PSQLQuery)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand(PSQLQuery, GetDBConnection());

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


        public  void Update(string Name, string Author, int Pages, int Id, string table)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand("UPDATE " + table + " set col_name = '" + Name + "', col_author = '" + Author + "', col_pages = '" + Pages + "' WHERE col_book_id =  '" + Id + "' ", GetDBConnection());

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

        public  void Insert(string Name, string Author, int Pages, string table)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand("INSERT into " + table + " (col_name, col_author,col_pages) VALUES ('" + Name + "', '" + Author + "','" + Pages + "') ", GetDBConnection());

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

        public  void Delete(int id, string table)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand("DELETE FROM " + table + " WHERE col_book_id =  " + id + " ", GetDBConnection());

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
