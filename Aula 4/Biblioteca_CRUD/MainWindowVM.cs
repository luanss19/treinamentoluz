using System;
using static Biblioteca_CRUD.ConnectionDB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;

namespace Biblioteca_CRUD
{
    public class MainWindowVM
    {


        public ObservableCollection<Livro> listaLivros { get; set; }

        public ICommand Add { get; private set; }

        public ICommand Remove { get; private set; }

        public ICommand Edit { get; private set; }

        public Livro LivroSelecionado { get; set; }

        public MainWindowVM()
        {
            listaLivros = new ObservableCollection<Livro>();
            InitialTable();
            IniciaComandos();
        }





        public void InitialTable()
        {
            listaLivros = ConnectionDB.GetTable("tab_books");
            ConnectionDB.CloseDBConnection();
        }
        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) => {

                Livro novoLivro = new Livro();

                CadastroEdicaoLivro telacadastroedicao = new CadastroEdicaoLivro();
                telacadastroedicao.DataContext = novoLivro;
                telacadastroedicao.ShowDialog();
                ConnectionDB.Insert(novoLivro.Name, novoLivro.Author, novoLivro.Pages, "tab_books");
                ConnectionDB.CloseDBConnection();
                listaLivros.Add(novoLivro);


            });

            Remove = new RelayCommand((object _) => {
                ConnectionDB.Delete(LivroSelecionado.Id, "tab_books");
                ConnectionDB.CloseDBConnection();
                listaLivros.Remove(LivroSelecionado);

            });

            Edit = new RelayCommand((object _) => {

                Livro livroEditar = (Livro)LivroSelecionado.Clone();

                CadastroEdicaoLivro telacadastroedicao = new CadastroEdicaoLivro();
                telacadastroedicao.DataContext = livroEditar;
                telacadastroedicao.ShowDialog();

                if (telacadastroedicao.DialogResult == true)
                {
                    ConnectionDB.Update(livroEditar.Name, livroEditar.Author, livroEditar.Pages, livroEditar.Id, "tab_books");
                    LivroSelecionado.Name = livroEditar.Name;
                    LivroSelecionado.Author = livroEditar.Author;
                    LivroSelecionado.Pages = livroEditar.Pages;
                    ConnectionDB.CloseDBConnection();

                }

            });
        }
    }
}
