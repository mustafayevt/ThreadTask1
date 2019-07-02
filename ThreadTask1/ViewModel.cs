using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadTask1.Commands;

namespace ThreadTask1
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string FilteredFilesPath = "FilesWithForbiddenWord";
        public void OnNotifyPropertyChanged(string param)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(param));
            
        }
        public ViewModel()
        {
            FolderBrowseCommand = new FolderBrowseCommand(this);
            AddForbiddenWordCommand = new AddForbiddenWordCommand(this);
            AddForbiddenWordFromFileCommand = new AddForbiddenWordFromFileCommand(this);
            SearchTaskCommand = new SearchTaskCommand(this);

            token = cancellationTokenSource.Token;

            ForbiddenWords = new ObservableCollection<string>() { "pox" };
            if (!System.IO.Directory.Exists(FilteredFilesPath))
                System.IO.Directory.CreateDirectory(FilteredFilesPath);
        }
        public ObservableCollection<string> ForbiddenWords { get; set; }
        public FolderBrowseCommand FolderBrowseCommand { get; set; }
        public AddForbiddenWordCommand AddForbiddenWordCommand { get; set; }
        public AddForbiddenWordFromFileCommand AddForbiddenWordFromFileCommand { get; set; }
        public SearchTaskCommand SearchTaskCommand { get; set; }
        public Task SearchFilesTask { get; set; }
        public Task CopyFilesTask { get; set; }

        public CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public CancellationToken token;
        //

        public ObservableCollection<string> FoundForbiddenPaths { get; set; } = new ObservableCollection<string>();

        public string NewForbiddenWord { get; set; }
        public string Path { get; set; }
    }
}
