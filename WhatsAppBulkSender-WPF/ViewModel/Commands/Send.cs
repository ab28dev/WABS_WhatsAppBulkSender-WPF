using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WhatsAppBulkSender_WPF.ViewModel.Commands
{
    public class Send : ICommand
    {
        public MainWindowVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Send(MainWindowVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            // This isnt working due to some binding error or idk
            string s = parameter as string;

            return !string.IsNullOrWhiteSpace(s);

            //return true;
        }

        public void Execute(object parameter)
        {
            VM.SendButtonFunctionality();
        }
    }
}
