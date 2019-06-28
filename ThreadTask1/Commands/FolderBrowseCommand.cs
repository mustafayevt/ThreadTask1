using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ThreadTask1
{
    public class FolderBrowseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ViewModel Model;

        public FolderBrowseCommand(ViewModel model)
        {
            Model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                Model.Path = dialog.SelectedPath;
            }
        }
    }
}
