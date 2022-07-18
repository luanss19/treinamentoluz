using Biblioteca_CRUD;
using Moq;
using MySqlConnector;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;

namespace TesteBiblioteca
{
    public class TestesDeFuncoesDadosInvalidos
    {
        [Test]
        public void GetTableComDadosInválidosPostgress()
        {
            // Arrange
            string? Table = null;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.Postgres, @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres");

            // Act


            // Assert
            Assert.Catch<ArgumentNullException>(() => database.GetTable(Table));

        }

        [Test]
        public void GetTableComDadosInválidosMySQL()
        {
            // Arrange
            string? Table = null;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.MySQL, "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql");
            // Act/Assert
            Assert.Catch<ArgumentNullException>(() => database.GetTable(Table));

        }

        [Test]
        public void InsertComDadosInválidosPostgress()
        {
            // Arrange
            string? Name = null;
            string Author = "";
            string? Table = null;
            int Pages = 200;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.Postgres, @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres");
            // Act/Assert
            Assert.Catch<ArgumentNullException>(() => database.Insert(Name, Author, Pages, Table));

        }

        [Test]
        public void InsertComDadosInválidosMySQL()
        {
            // Arrange
            string? Name = null;
            string Author = "";
            string? Table = null;
            int Pages = 200;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.MySQL, "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql");
            // Act/Assert
            Assert.Catch<ArgumentNullException>(() => database.Insert(Name, Author, Pages, Table));

        }

        [Test]
        public void UpdateComDadosInválidosPostgress()
        {
            // Arrange
            string? Name = null;
            string Author = "";
            string? Table = null;
            int Id = 1;
            int Pages = 200;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.Postgres, @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres");
            // Act/Assert
            Assert.Catch<ArgumentNullException>(() => database.Update(Name, Author, Pages, Id, Table));

        }

        [Test]
        public void UpdateComDadosInválidosMySQL()
        {
            // Arrange
            string? Name = null;
            string Author = "";
            string? Table = null;
            int Id = 1;
            int Pages = 200;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.MySQL, "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql");
            // Act/Assert
            Assert.Catch<ArgumentNullException>(() => database.Update(Name, Author, Pages, Id, Table));

        }


        [Test]
        public void DeleteComDadosInválidosPostgress()
        {
            // Arrange
            string? Table = null;
            int Id = 1;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.Postgres, @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres");

            // Assert/ Act
            Assert.Catch<ArgumentNullException>(() => database.Delete(Id, Table));

        }

        [Test]
        public void DeleteComDadosInválidosMySQL()
        {
            // Arrange
            string? Table = null;
            int Id = 1;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.MySQL, "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql");

            // Assert/ Act
            Assert.Catch<ArgumentNullException>(() => database.Delete(Id, Table));

        }

    }

    public class TestesDeIntegracao
    {


        [Test]
        public void InsertDadosVálidosPostgress()
        {
            // Arrange
            string Name = "Nome";
            string Author = "AutorTeste";
            string Table = "tab_books";
            int Pages = 200;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.Postgres, @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres");

            // Act
            int noRows = database.Insert(Name, Author, Pages, Table);
            if(noRows != 0)
            {
                database.ExecuteQuery("DELETE FROM tab_books WHERE col_name = '" + Name + "' AND col_author = '" + Author + "' ");
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void DeleteDadosVálidosPostgress()
        {
            // Arrange
            string Name = "DadoTeste";
            string Author = "DadoTeste";
            string Table = "tab_books";
            int Pages = 10;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.Postgres, @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres");

            // Act
            database.Insert(Name, Author, Pages, Table);
            int noRows = database.ExecuteQuery("DELETE FROM tab_books WHERE col_name = '" + Name + "' AND col_author = '" + Author + "' ");

            if (noRows != 0)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void InsertDadosVálidosMySQL()
        {
            // Arrange
            string Name = "Nome";
            string Author = "AutorTeste";
            string Table = "tab_books";
            int Pages = 200;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.MySQL, "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql");

            // Act
            int noRows = database.Insert(Name, Author, Pages, Table);
            if (noRows != 0)
            {
                database.ExecuteQuery("DELETE FROM tab_books WHERE column_name = '" + Name + "' AND column_author = '" + Author + "' ");
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void DeleteDadosVálidosMySQL()
        {
            // Arrange
            string Name = "DadoTeste";
            string Author = "DadoTeste";
            string Table = "tab_books";
            int Pages = 10;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.MySQL, "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql");

            // Act
            database.Insert(Name, Author, Pages, Table);
            int noRows = database.ExecuteQuery("DELETE FROM tab_books WHERE column_name = '" + Name + "' AND column_author = '" + Author + "' ");

            if (noRows != 0)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void UpdateDadosVálidosMySQL()
        {
            // Arrange
            string Name = "DadoTeste";
            string Author = "DadoTeste";
            string Table = "tab_books";
            int Pages = 10;

            string NewName = "name";
            string NewAuthor = "author";
            int NewPages = 120;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.MySQL, "Server=localhost;Port=33061;UID=root;Password=4312;Database=db_biblioteca_mysql");

            // Act
            database.Insert(Name, Author, Pages, Table);
            int id = database.ExecuteScalar("SELECT column_book_id FROM tab_books WHERE column_name = '" + Name + "' AND column_author = '" + Author + "' ");
            
            int noRows = database.Update(NewName,NewAuthor,NewPages, id, Table);

            if (noRows != 0)
            {
                database.ExecuteQuery("DELETE FROM tab_books WHERE column_name = '" + NewName + "' AND column_author = '" + NewAuthor + "' ");
                Assert.Pass();
            }
            Assert.Fail();
        }


        [Test]
        public void UpdateDadosVálidosPostgres()
        {
            // Arrange
            string Name = "DadoTeste";
            string Author = "DadoTeste";
            string Table = "tab_books";
            int Pages = 10;

            string NewName = "name";
            string NewAuthor = "author";
            int NewPages = 120;
            DatabseFactory factory = new DatabseFactory();
            IDatabase database = factory.getDatabase(DatabaseType.Postgres, @"Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres");

            // Act
            database.Insert(Name, Author, Pages, Table);
            int id = database.ExecuteScalar("SELECT col_book_id FROM tab_books WHERE col_name = '" + Name + "' AND col_author = '" + Author + "' ");

            int noRows = database.Update(NewName, NewAuthor, NewPages, id, Table);

            if (noRows != 0)
            {
                database.ExecuteQuery("DELETE FROM tab_books WHERE col_name = '" + NewName + "' AND col_author = '" + NewAuthor + "' ");
                Assert.Pass();
            }
            Assert.Fail();
        }

    }

    public class TestesDeMock
    {
        [Test]
        public void DeleteComBancoCaidoMysql()
        {
            // Arrange
            string Table = "tab_livrosTESTE";
            int Id = 1;
            Mock<IDbConnection> connection = new Mock<IDbConnection>();
            connection.Setup(x => x.Open());
            ConnectionDB_Mysql db = new ConnectionDB_Mysql((MySqlConnection)connection.Object);
            // Act

            // Assert
            Assert.Catch<MySqlException>(() => db.Delete(Id, Table));


        }

        [Test]
        public void DeleteComBancoCaidoPostgres()
        {
            // Arrange
            string Table = "tab_livrosTESTE";
            int Id = 1;
            Mock<IDbConnection> connection = new Mock<IDbConnection>();
            connection.Setup(x => x.Open());
            ConnectionDB_Postgres db = new ConnectionDB_Postgres((NpgsqlConnection)connection.Object);
            // Act

            // Assert
            Assert.Catch<NpgsqlException>(() => db.Delete(Id, Table));


        }


        [Test]
        public void InsertComBancoCaidoMysql()
        {
            // Arrange
            string Name = "Nome Livro";
            string Author = "Autor Livro";
            string Table = "tab_livrosTESTE";
            int Pages = 200;
            Mock<IDbConnection> connection = new Mock<IDbConnection>();
            connection.Setup(x => x.Open());
            ConnectionDB_Mysql db = new ConnectionDB_Mysql((MySqlConnection)connection.Object);
            // Act

            // Assert
            Assert.Catch<MySqlException>(() => db.Insert(Name, Author, Pages, Table));


        }

        [Test]
        public void InsertComBancoCaidoPostgres()
        {
            // Arrange
            string Name = "Nome Livro";
            string Author = "Autor Livro";
            string Table = "tab_livrosTESTE";
            int Pages = 200;
            Mock<IDbConnection> connection = new Mock<IDbConnection>();
            connection.Setup(x => x.Open());
            ConnectionDB_Postgres db = new ConnectionDB_Postgres((NpgsqlConnection)connection.Object);
            // Act

            // Assert
            Assert.Catch<NpgsqlException>(() => db.Insert(Name, Author, Pages, Table));


        }

        [Test]
        public void UpdateComBancoCaidoMysql()
        {
            // Arrange
            string Name = "Nome Livro";
            string Author = "Autor Livro";
            string Table = "tab_livrosTESTE";
            int Id = 1;
            int Pages = 200;
            Mock<IDbConnection> connection = new Mock<IDbConnection>();
            connection.Setup(x => x.Open());
            ConnectionDB_Mysql db = new ConnectionDB_Mysql((MySqlConnection)connection.Object);
            // Act

            // Assert
            Assert.Catch<MySqlException>(() => db.Update(Name, Author, Pages, Id, Table));


        }

        [Test]
        public void UpdateComBancoCaidoPostgres()
        {
            // Arrange
            string Name = "Nome Livro";
            string Author = "Autor Livro";
            string Table = "tab_livrosTESTE";
            int Id = 1;
            int Pages = 200;
            Mock<IDbConnection> connection = new Mock<IDbConnection>();
            connection.Setup(x => x.Open());
            ConnectionDB_Postgres db = new ConnectionDB_Postgres((NpgsqlConnection)connection.Object);
            // Act

            // Assert
            Assert.Catch<NpgsqlException>(() => db.Update(Name, Author, Pages, Id, Table));


        }



        [Test]
        public void TesteAntigo()
        {
            // Arrange
            string Name = "Nome Livro";
            string Author = "Autor Livro";
            string Table = "tab_livrosTESTE";
            int Pages = 200;
            Mock<IDatabase> database = new Mock<IDatabase>();

            // Act
            database.Setup(x => x.CloseDBConnection());
            database.Setup(x => x.Insert(Name, Author, Pages, Table));

            // Assert
            database.Verify();

        }


    }
}