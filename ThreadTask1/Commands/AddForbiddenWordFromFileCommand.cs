using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ThreadTask1
{
    public class AddForbiddenWordFromFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ViewModel Model;

        public AddForbiddenWordFromFileCommand(ViewModel model)
        {
            Model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Doc Files|*.doc;*.docx";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Task task = new Task((Action)delegate
            {

                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                object miss = System.Reflection.Missing.Value;
                object path = fileDialog.FileName;
                object readOnly = true;
                Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                for (int i = 0; i < docs.Paragraphs.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(docs.Paragraphs[i + 1].Range.Text.ToString()))
                    {
                        string nWord = docs.Paragraphs[i + 1].Range.Text.ToString();
                        nWord = nWord.Replace("\r", "");
                        var splitted = nWord.Split(' ');
                        App.Current.Dispatcher.BeginInvoke(
  new Action(() => splitted.ToList().ForEach(x=> Model.ForbiddenWords.Add(x.Replace(" ","")))));
                    }
                }
                docs.Close();
                word.Quit();

            });
                task.Start();
            }

        }
    }
}
