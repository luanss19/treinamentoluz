using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_CRUD
{
    internal class DBInjecaoDependencia: IDatabase
    {
        private static string connectionString;
        private static DataTable dataTable;
        private static List<Livro> ListaLivrosTemporaria;
        private static IDatabase database;
        private static DbConnection connection;


        DatabaseType IDatabase.DatabaseType => throw new NotImplementedException();

        public DBInjecaoDependencia(IDatabase db, string connectionStr)
        {
            connectionString = connectionStr;
            database = db;
            switch (database.DatabaseType)
            {
                case DatabaseType.MySQL:
                    connection = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.Postgres:
                    connection = new NpgsqlConnection(connectionString);
                    break;
                default:
                    throw new DatabaseDriverNotImplementedException("Banco de Dados ainda não implementado");
            }
            dataTable = new DataTable();
            ListaLivrosTemporaria = new List<Livro>();
        }

        public DbConnection GetDBConnection()
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



        public int ExecuteQuery(string Query)
        {
            DbCommand cmd_Command = new MySqlCommand(Query, (MySqlConnection)connection);
            return cmd_Command.ExecuteNonQuery();

        }

        public List<Livro> GetTable(string Table)
        {
            switch (database.DatabaseType)
            {
                case DatabaseType.MySQL:
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM " + Table, (MySqlConnection)GetDBConnection()))
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
                    break;

                case DatabaseType.Postgres:
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM " + Table, (NpgsqlConnection)GetDBConnection()))
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
                    break;
            }
                    return ListaLivrosTemporaria;

        }

        public int ExecuteScalar(string MYSQLQuery)
        {
            return 1;
        }
        public int Insert(string Name, string Author, int Pages, string table)
        {
            switch (database.DatabaseType)
            {
                case DatabaseType.MySQL:
                    MySqlCommand cmd_Command = new MySqlCommand("call proc_insert ('" + Name + "', '" + Author + "'," + Pages + ") ", (MySqlConnection)GetDBConnection());

                    try
                    {
                        return cmd_Command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        CloseDBConnection();

                    }
                    break;

                case DatabaseType.Postgres:
                    NpgsqlCommand cmd_CommandPG = new NpgsqlCommand("INSERT into " + table + " (col_name, col_author,col_pages) VALUES ('" + Name + "', '" + Author + "','" + Pages + "') ", (NpgsqlConnection)GetDBConnection());

                    try
                    {
                        return cmd_CommandPG.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        CloseDBConnection();

                    }
                    break;
            }
            return -1;
        }
        public void Delete(int id, string table)
        {
            switch (database.DatabaseType)
            {
                case DatabaseType.MySQL:
                    MySqlCommand cmd_Command = new MySqlCommand("call proc_delete (" + id + ") ", (MySqlConnection)GetDBConnection());

                    try
                    {
                        cmd_Command.ExecuteReader();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    CloseDBConnection();
                    break;

                case DatabaseType.Postgres:
                    NpgsqlCommand cmd_CommandPG = new NpgsqlCommand("DELETE FROM " + table + " WHERE col_book_id =  " + id + " ", (NpgsqlConnection)GetDBConnection());

                    try
                    {
                        cmd_CommandPG.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    CloseDBConnection();
                    break;
            }
            
        }
        public int Update(string Name, string Author, int Pages, int Id, string table)
        {
            switch (database.DatabaseType)
            {
                case DatabaseType.MySQL:
                    MySqlCommand cmd_Command = new MySqlCommand("call proc_update ('" + Name + "', '" + Author + "'," + Pages + ", " + Id + ") ", (MySqlConnection)GetDBConnection());

                    return cmd_Command.ExecuteNonQuery();
                    CloseDBConnection();
                    break;

                case DatabaseType.Postgres:
                    NpgsqlCommand cmd_CommandPG = new NpgsqlCommand("UPDATE " + table + " set col_name = '" + Name + "', col_author = '" + Author + "', col_pages = '" + Pages + "' WHERE col_book_id =  '" + Id + "' ", (NpgsqlConnection)GetDBConnection());

                    try
                    {
                        return cmd_CommandPG.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    CloseDBConnection();
                    break;
            }
            return -1;

        }
    }
}
