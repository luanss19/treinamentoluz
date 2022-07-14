using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using Npgsql;
using MySqlConnector;

namespace Biblioteca_CRUD
{
    public class MainWindowVM : INotifyPropertyChanged
    {


        public ObservableCollection<Livro> listaLivros { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IDatabase database { get; set; }
        public DatabseFactory DBFactory { get; private set; }
        public ICommand Add { get; private set; }

        public ICommand Remove { get; private set; }

        public ICommand Edit { get; private set; }
        public ICommand Postgres { get; private set; }
        public ICommand MySQL { get; private set; }

        public Livro LivroSelecionado { get; set; }

        private readonly ObservableCollection<ButtonModel> _dataButtons = new ObservableCollection<ButtonModel>();
        public ObservableCollection<ButtonModel> DataButtons { get { return _dataButtons; } }

        public MainWindowVM()
        {
            try
            {
                DBFactory = new DatabseFactory();
                //database = new DBInjecaoDependencia(DBFactory.getDatabase(DatabaseType.Postgres), "Server=localhost;Port=54320;User id=postgres;Password=4312;Database=db_biblioteca_postgres");
                database = DBFactory.getDatabase(DatabaseType.Postgres);
                listaLivros = new ObservableCollection<Livro>(database.GetTable("tab_books"));
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (NpgsqlException ee)
            {
                Console.WriteLine(ee.ToString());
            }


            foreach (DatabaseType type in Enum.GetValues(typeof(DatabaseType)).Cast<DatabaseType>().ToList())
            {
                DataButtons.Add(new ButtonModel("Banco " + type, CreateCustomCommand(type)));
            }
            IniciaComandos();
        }

        private ICommand CreateCustomCommand(DatabaseType type)
        {
            ICommand Command = new RelayCommand((object _) => {
                try
                {
                    listaLivros.Clear();
                    database = DBFactory.getDatabase(type);
                    foreach (Livro livro in database.GetTable("tab_books"))
                    {
                        listaLivros.Add(livro);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            });

            return Command;
        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) => {

                Livro novoLivro = new Livro();

                CadastroEdicaoLivro telacadastroedicao = new CadastroEdicaoLivro();
                telacadastroedicao.DataContext = novoLivro;
                telacadastroedicao.ShowDialog();
                if (telacadastroedicao.DialogResult == true)
                {
                    try
                    {
                        database.Insert(novoLivro.Name, novoLivro.Author, novoLivro.Pages, "tab_books");
                        listaLivros.Add(novoLivro);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch (NpgsqlException ee)
                    {
                        Console.WriteLine(ee.ToString());
                    }
                }
            });

            Remove = new RelayCommand((object _) => {
                try
                {
                    database.Delete(LivroSelecionado.Id, "tab_books");
                    listaLivros.Remove(LivroSelecionado);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                catch (NpgsqlException ee)
                {
                    Console.WriteLine(ee.ToString());
                }


            });

            Edit = new RelayCommand((object _) => {

                Livro livroEditar = (Livro)LivroSelecionado.Clone();

                CadastroEdicaoLivro telacadastroedicao = new CadastroEdicaoLivro();
                telacadastroedicao.DataContext = livroEditar;
                telacadastroedicao.ShowDialog();

                if (telacadastroedicao.DialogResult == true)
                {
                    try
                    {
                        database.Update(livroEditar.Name, livroEditar.Author, livroEditar.Pages, livroEditar.Id, "tab_books");
                        LivroSelecionado.Name = livroEditar.Name;
                        LivroSelecionado.Author = livroEditar.Author;
                        LivroSelecionado.Pages = livroEditar.Pages;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch (NpgsqlException ee)
                    {
                        Console.WriteLine(ee.ToString());
                    }
                }

            });
        }

        public void Notifica(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }


       
    }
}
