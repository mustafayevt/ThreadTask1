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
                    TaskIsRunning = true;
                    Model.SearchFilesTask = new Task(SearchFilesWithForbiddenWords, Model.token);
                    Model.SearchFilesTask.Start();
                }
            }
            else if (state == "2")
            {
                if (Model.SearchFilesTask != null)
                {
                    TaskIsRunning = false;
                }
            }
            else if (state == "3")
            {
                if (Model.SearchFilesTask != null)
                {
                    Model.cancellationTokenSource.Cancel();
                }
            }
        }
        public bool TaskIsRunning { get; set; } = true;
        public int IterationForSearch { get; set; }
        public int IterationForCopy { get; set; }
        public void SearchFilesWithForbiddenWords()
        {
            List<string> files = Directory.GetFiles(Model.Path, "*.txt", SearchOption.AllDirectories).ToList();
            for (int i = IterationForSearch; i < files.Count; i++)
            {
                foreach (var item in Model.ForbiddenWords)
                {
                    while (!TaskIsRunning) { }
                    if (Model.token.IsCancellationRequested)
                    {
                        Thread.CurrentThread.Abort();
                        return;
                    }

                    if (File.ReadAllText(files[i]).Contains(item))
                    {
                        Thread.Sleep(500);
                        App.Current.Dispatcher.BeginInvoke(new Action(() => Model.FoundForbiddenPaths.Add(files[i])));
                        break;
                    }
                }
                IterationForSearch = i;
            }
        }

        public void CopyFilesWithForbiddenWords()
        {
            for (int i = IterationForCopy; i < Model.FoundForbiddenPaths.Count; i++)
            {
                while (!TaskIsRunning) { }
                foreach (var item in Model.ForbiddenWords)
                {
                    string FileContent;
                }
                File.Copy(Model.FoundForbiddenPaths[i], Model.FilteredFilesPath);
                IterationForCopy = i;
            }
        }
    }
}
