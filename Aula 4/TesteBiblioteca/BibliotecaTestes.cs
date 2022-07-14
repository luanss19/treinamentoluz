using Biblioteca_CRUD;
using Moq;using System;
namespace TesteBiblioteca
{
    public class ConexaoBancoTestes
    {
        [Test]
        public void InsertComDadosV�lidos()
        {
            // Arrange
            string Name = "Nome Livro";
            string Author = "Autor Livro";
            string Table = "tab_livrosTESTE";
            int Pages = 200;
            Mock<IDatabase> database = new Mock<IDatabase>();

            // Act
            database.Setup(x => x.Insert(Name, Author, Pages, Table));

            // Assert
            database.Verify();

        }
        [Test]
        public void InsertComBancoCaido()
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

        [Test]
        public void UpdateComDadosV�lidos()
        {
            // Arrange
            string Name = "Nome Livro";
            string Author = "Autor Livro";
            string Table = "tab_livrosTESTE";
            int Pages = 200;
            int Id = 1;
            Mock<IDatabase> database = new Mock<IDatabase>();

            // Act
            database.Setup(x => x.Update(Name, Author, Pages, Id, Table));

            // Assert
            database.Verify();

        }
        [Test]
        public void UpdateComBancoCaido()
        {
            // Arrange
            string Name = "Nome Livro";
            string Author = "Autor Livro";
            string Table = "tab_livrosTESTE";
            int Pages = 200;
            int Id = 1;
            Mock<IDatabase> database = new Mock<IDatabase>();

            // Act
            database.Setup(x => x.CloseDBConnection());
            database.Setup(x => x.Update(Name, Author, Pages, Id, Table));

            // Assert
            database.Verify();

        }

        [Test]
        public void DeleteComDadosV�lidos()
        {
            // Arrange
            string Table = "tab_livrosTESTE";
            int Id = 1;
            Mock<IDatabase> database = new Mock<IDatabase>();

            // Act
            database.Setup(x => x.Delete(Id, Table));


            // Assert
            database.Verify();

        }
        [Test]
        public void DeleteComBancoCaido()
        {
            // Arrange
            string Table = "tab_livrosTESTE";
            int Id = 1;
            Mock<IDatabase> database = new Mock<IDatabase>();

            // Act
            database.Setup(x => x.CloseDBConnection());
            database.Setup(x => x.Delete(Id, Table));

            // Assert
            database.Verify();

        }
    }
}