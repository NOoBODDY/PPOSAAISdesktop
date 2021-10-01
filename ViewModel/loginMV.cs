using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientDB.Model;
using System.Windows;

namespace ClientDB.ViewModel
{
    class loginMV : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        string username;
        string password;
        public string UserName 
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("UserName");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public loginMV()
        {
            UserName = "UserName";
            Password = Password;
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new RelayCommand(obj =>
                    {
                        MessageBox.Show(UserName + " " + Password);
                    }));
            }
        }


    }
}
