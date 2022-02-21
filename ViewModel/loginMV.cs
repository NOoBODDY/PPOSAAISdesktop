using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using ClientDB.Model;
using System.Windows;
using Unity;
using ClientDB.View;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

//TODO 
//(1)добавить проверку на админа, когда будет функционал



namespace ClientDB.ViewModel
{
    class loginMV : INotifyPropertyChanged
    {
        #region interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
        string username;

        public Action CloseAction { get; set; }


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

        APIservice api;

        #region commands
        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new RelayCommand(obj =>
                    {
                        api = new APIservice(UserName, Password);

                        if (api.LogIn() == "OK") // 
                        {

                            MainView main = new MainView(api);
                            main.Show();
                            CloseAction();
                        }
                        else
                        {
                            MessageBox.Show("Иди нахуй");
                        }

                    }));
            }
        }

        RelayCommand closeCommand;

        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                                    (closeCommand = new RelayCommand(obj => { CloseAction();}));
            }
        }
        #endregion
    }
}
