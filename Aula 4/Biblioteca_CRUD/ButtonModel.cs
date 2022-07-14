using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Biblioteca_CRUD
{
    public class ButtonModel
    {
        public string ButtonName { get; set; }
        public ICommand ButtonCommand { get; set; }

        public ButtonModel(string buttonName, ICommand buttonCommand)
        {
            ButtonName = buttonName;
            ButtonCommand = buttonCommand;
        }
    }
}
