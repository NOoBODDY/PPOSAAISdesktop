using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientDB.Model;
using System.Windows;
using Unity;
using ClientDB.View;


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
        public string UserName 
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("UserName");
            }
        }

        private IUnityContainer container;

        public loginMV( IUnityContainer unityContainer)
        {
            UserName = "UserName";
            container = unityContainer;
            
        }

        public string Password
        {
            get
            {
                IPasswordSupplier passwordSupplier = container.Resolve<IPasswordSupplier>();
                return passwordSupplier.GetPassword();
            }
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
