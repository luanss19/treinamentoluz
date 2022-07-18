using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySqlConnector;
using Npgsql;

namespace Biblioteca_CRUD
{
    public class ConnectionDB_Postgres : IDatabase
    {
        //private static string connectionString = @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres";

        private NpgsqlConnection connection;
        private static DataTable dataTable;
        private static List<Livro> ListaLivrosTemporaria;

        DatabaseType IDatabase.DatabaseType => DatabaseType.Postgres;

        public ConnectionDB_Postgres(NpgsqlConnection connection)
        {
            dataTable = new DataTable();
            ListaLivrosTemporaria = new List<Livro>();
            this.connection = connection;
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
            if (PSQL_Text != "" & PSQL_Text != null)
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
            else throw new ArgumentNullException("Agrumento inválido");
                
        }
        public  int ExecuteQuery(string PSQLQuery)
        {
            if (PSQLQuery != "" & PSQLQuery != null)

            {
                NpgsqlCommand cmd_Command = new NpgsqlCommand(PSQLQuery, GetDBConnection());

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

        public int ExecuteScalar(string PGQuery)
        {
            if (PGQuery != "" & PGQuery != null)
            {
                NpgsqlCommand cmd_Command = new NpgsqlCommand(PGQuery, GetDBConnection());
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

        public  int Update(string Name, string Author, int Pages, int Id, string table)
        {
            if (Name != "" & Name != null & Author != "" & Author != null & table != "" & table != null)
            {
                NpgsqlCommand cmd_Command = new NpgsqlCommand("UPDATE " + table + " set col_name = '" + Name + "', col_author = '" + Author + "', col_pages = '" + Pages + "' WHERE col_book_id =  '" + Id + "' ", GetDBConnection());

                try
                {
                    return cmd_Command.ExecuteNonQuery();
                }
                catch (NpgsqlException ee)
                {
                    Console.WriteLine(ee.Message);
                    throw ee;
                }
                finally
                {
                    CloseDBConnection();

                }
            }
            else throw new ArgumentNullException("Agrumento inválido");
        }

        public int Insert(string Name, string Author, int Pages, string table)
        {
            
            if (Name != "" & Name != null & Author != "" & Author != null & table != "" & table != null)
            {
                NpgsqlCommand cmd_Command = new NpgsqlCommand("INSERT into " + table + " (col_name, col_author,col_pages) VALUES ('" + Name + "', '" + Author + "','" + Pages + "') ", GetDBConnection());

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

        public  void Delete(int id, string table)
        {
            if (table != "" & table != null)
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
                finally
                {
                    CloseDBConnection();
                }
            }
            else throw new ArgumentNullException("Agrumento inválido");
        }

    }
}
