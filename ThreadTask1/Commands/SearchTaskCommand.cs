using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ThreadTask1.Commands
{
    public class SearchTaskCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ViewModel Model;

        public SearchTaskCommand(ViewModel model)
        {
            Model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var state = parameter as string;
            if (state == "1")
            {
                if (!string.IsNullOrEmpty(Model.Path))
                {
                    Model.SearchFilesTask = new Task(SearchFilesWithForbiddenWords);
                    Model.SearchFilesTask.Start();
                }
            }
            else if(state == "2")
            {
                if(Model.SearchFilesTask != null)
                {
                    
                }
            }
        }
        public void SearchFilesWithForbiddenWords()
        {
            foreach (string file in Directory.EnumerateFiles(Model.Path, "*.txt", SearchOption.AllDirectories))
            {
                foreach (var item in Model.ForbiddenWords)
                {
                    if (File.ReadAllText(file).Contains(item))
                    {
                        Thread.Sleep(500);
                        App.Current.Dispatcher.BeginInvoke(new Action(() => Model.FoundForbiddenPaths.Add(file)));
                        break;
                    }
                }
            }
        }
    }
}
