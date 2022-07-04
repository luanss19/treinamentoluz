using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_CRUD
{
    public class Livro : ICloneable, INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string author;
        private int pages;

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Livro()
        {

        }

        public Livro(string nome, string author, int pages, int newId)
        {
            this.id = newId;
            this.name = nome;
            this.author = author;
            this.pages = pages;
        }

        public int Id
        {
            get { return id; }
            set { id = value;
                Notifica("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value;
                Notifica("Name");

            }
        }

        public string Author 
        { 
            get { return author; }
            set { author = value;
                Notifica("Author");
            }
        }

        public int Pages
        {
            get { return pages; }
            set { pages = value;
                Notifica("Pages");
            }
        }

        public void Notifica(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }
    }
}
