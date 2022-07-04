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
    internal class ConnectionDB
    {
        public static string connectionString = @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=";

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        public static NpgsqlConnection GetDBConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        public static ObservableCollection<Livro> GetTable(string PSQL_Text)
        {
            ObservableCollection<Livro> listaLivrosTemporaria = new ObservableCollection<Livro>();
            NpgsqlConnection cn_connection = GetDBConnection();
            DataTable dataTable = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM " + PSQL_Text , cn_connection);
            adapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                Livro livro = new Livro()
                {
                    Id = (int)row["col_book_id"],
                    Name = (string)row["col_name"],
                    Author = (string)row["col_author"],
                    Pages = (int)row["col_pages"]
                };
                listaLivrosTemporaria.Add(livro);
            }
            CloseDBConnection();
            return listaLivrosTemporaria;
        }

        public static void ExecutePSQLQuery(string PSQLQuery)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand(PSQLQuery, GetDBConnection());
            cmd_Command.ExecuteNonQuery();
        }

        //todo criar update, delete, insert aqui e passar os campos a serem alterados por parametros

        public static void CloseDBConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public static void Update(string Name, string Author, int Pages, int Id, string table)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand("UPDATE " + table + " set col_name = '" + Name + "', col_author = '" + Author + "', col_pages = '" + Pages + "' WHERE col_book_id =  '" + Id + "' ", GetDBConnection());

            cmd_Command.ExecuteNonQuery();
        }

        public static void Insert(string Name, string Author, int Pages, string table)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand("INSERT into " + table + " (col_name, col_author,col_pages) VALUES ('" + Name + "', '" + Author + "','" + Pages + "') ", GetDBConnection());

            cmd_Command.ExecuteNonQuery();

        }

        public static void Delete(int id, string table)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand("DELETE FROM " + table + " WHERE col_book_id =  " + id + " ", GetDBConnection());

            cmd_Command.ExecuteNonQuery();

        }

    }
}
