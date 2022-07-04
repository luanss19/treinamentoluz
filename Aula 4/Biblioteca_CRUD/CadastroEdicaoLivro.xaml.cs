using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biblioteca_CRUD
{
    /// <summary>
    /// Lógica interna para CadastroEdicaoLivro.xaml
    /// </summary>
    public partial class CadastroEdicaoLivro : Window
    {
        public CadastroEdicaoLivro()
        {
            InitializeComponent();
        }

        private void btnConfirma(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
