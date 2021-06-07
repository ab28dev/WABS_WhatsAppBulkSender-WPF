using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhatsAppBulkSender_WPF.Model;
using WhatsAppBulkSender_WPF.ViewModel.Commands;

namespace WhatsAppBulkSender_WPF.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public string baseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
        

        private Message message;

        public Message Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private Contacts contacts;

        public Contacts Contacts
        {
            get { return contacts; }
            set
            {
                contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        public Import Import { get; set; }

        public Send Send { get; set; }


        public MainWindowVM()
        {
            Message = new Message
            {
                TextMessage = ""
            };
            Contacts = new Contacts
            {
                Contact = ""
            };

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Message = new Message
                {
                    TextMessage = "Enter your message in this box"
                };
                Contacts = new Contacts
                {
                    Contact = "Import/Paste Contacts in this Box"
                };

            }

            Import = new Import(this);
            Send = new Send(this);

        }

        public void ImportButtonFunctionality()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string importedFileName = "";
            if (dialog.ShowDialog() == true)
            {
                importedFileName = dialog.FileName;
                //MessageBox.Show(importedFileName);
                Contacts = new Contacts
                {
                    Contact = File.ReadAllText(importedFileName)
                };
            }

            string inputFileName = "INPUTcontacts.txt";
            string inputFilePath = "{0}{1}";

            string fullPath = string.Format(inputFilePath, baseDirectoryPath, inputFileName);
            try
            {
                File.Copy(importedFileName, inputFileName, true);
                MessageBox.Show("Contacts Imported Successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SaveMessageinFile()
        {
            string messageFileName = "Message.txt";
            string messageFilePath = "{0}{1}";

            string fullmessagePath = string.Format(messageFilePath, baseDirectoryPath, messageFileName);
            try
            {
                File.WriteAllText(fullmessagePath, Message.TextMessage);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SendButtonFunctionality()
        {
            SaveMessageinFile();

            // TO-DO: Check if both the textbox is not empty.
            // To be implemented

            //MessageBox.Show(baseDirectoryPath);

            string exeFileName = "WhatsAppBulkSender-Python.exe";
            string exeFilePath = "{0}{1}";

            string fullexePath = string.Format(exeFilePath, baseDirectoryPath, exeFileName);
            try
            {
                Process.Start(fullexePath);
                MessageBox.Show("Wait for the script to start \nThen scan the QR code from Whatsapp mobile app to login. \nSee your Messages Get Delivered....");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
