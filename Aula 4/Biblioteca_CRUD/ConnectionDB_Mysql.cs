using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_CRUD
{
    public class ConnectionDB_Mysql : IDatabase
    {
        //private static string connectionString = "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql";

        private MySqlConnection connection;
        private static DataTable dataTable;
        private static List<Livro> ListaLivrosTemporaria;

        DatabaseType IDatabase.DatabaseType => DatabaseType.MySQL;

        public ConnectionDB_Mysql(MySqlConnection connection)
        {
            dataTable = new DataTable();
            ListaLivrosTemporaria = new List<Livro>();
            this.connection = connection;
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
        public int ExecuteQuery(string MYSQLQuery)
        {
            if (MYSQLQuery != "" & MYSQLQuery != null)
            {
                MySqlCommand cmd_Command = new MySqlCommand(MYSQLQuery, GetDBConnection());
                try
                {
                    return cmd_Command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    CloseDBConnection();
                }
            }
            else throw new ArgumentNullException("Agrumento inválido");

        }

        public int ExecuteScalar(string MYSQLQuery)
        {
            if (MYSQLQuery != "" & MYSQLQuery != null)
            {
                MySqlCommand cmd_Command = new MySqlCommand(MYSQLQuery, GetDBConnection());
                try
                {
                    return (int)cmd_Command.ExecuteScalar();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    CloseDBConnection();
                }
            }
            else throw new ArgumentNullException("Agrumento inválido");

        }

        public List<Livro> GetTable(string MYSQL_Text)
        {

            if (MYSQL_Text != "" & MYSQL_Text != null)
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
            else throw new ArgumentNullException("Agrumento inválido");
        }

        public int Insert(string Name, string Author, int Pages, string table)
        {
            if (Name != "" & Name != null & Author != "" & Author != null & table != "" & table != null)
            {
                MySqlCommand cmd_Command = new MySqlCommand("call proc_insert ('" + Name + "', '" + Author + "'," + Pages + ") ", GetDBConnection());

                try
                {
                    return cmd_Command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    CloseDBConnection();

                }
            }
            else throw new ArgumentNullException("Agrumento inválido");
        }
        public void Delete(int id, string table)
        {
            if (table != "" & table != null)
            {
                MySqlCommand cmd_Command = new MySqlCommand("call proc_delete (" + id + ") ", GetDBConnection());

                try
                {
                    cmd_Command.ExecuteReader();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    CloseDBConnection();

                }
            }
            else throw new ArgumentNullException("Agrumento inválido");
        }
        public int Update(string Name, string Author, int Pages, int Id, string table)
        {

            if (Name != "" & Name != null & Author != "" & Author != null & table != "" & table != null)
            {
                MySqlCommand cmd_Command = new MySqlCommand("call proc_update ('" + Name + "', '" + Author + "'," + Pages + ", " + Id + ") ", GetDBConnection());

                try
                {
                    return cmd_Command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    CloseDBConnection();

                }
            }
            else throw new ArgumentNullException("Agrumento inválido");

        }
    }
}
