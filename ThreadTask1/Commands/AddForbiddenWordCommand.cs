using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThreadTask1
{
    public class AddForbiddenWordCommand : ICommand
    {
        ViewModel Model;

        public AddForbiddenWordCommand(ViewModel model)
        {
            Model = model;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(!string.IsNullOrEmpty(Model.NewForbiddenWord))
            {
                Model.ForbiddenWords.Add(Model.NewForbiddenWord);
                Model.NewForbiddenWord = "";
                Model.OnNotifyPropertyChanged(nameof(Model.NewForbiddenWord));
            }
        }
    }
}
