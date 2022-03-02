using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using ClientDB.Model;
using System.Windows;
using Unity;
using ClientDB.View;
using System.Threading.Tasks;
using System.Threading;

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

        SynchronizationContext Context;

        public loginMV( IUnityContainer unityContainer)
        {
            container = unityContainer;
            Context = SynchronizationContext.Current;
        }

        public string Password
        {
            get
            {
                IPasswordSupplier passwordSupplier = container.Resolve<IPasswordSupplier>();
                return passwordSupplier.GetPassword();
            }
        }

        REST_APIservice api;

        #region commands
        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new RelayCommand(obj =>
                    {
                        api = new REST_APIservice(UserName, Password);
                        api.BadRequest += badRequest;
                        Task task = api.LogInAsync();
                        Task task1 = task.ContinueWith(t=> Context.Post(getConnection, null));
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

        #region Tasks
        void getConnection(object state)
        {
            MainView main = new MainView(api);
            main.Show();
            CloseAction();
        }

        void badRequest(object sender, string message)
        {
            MessageBox.Show(message);
        }
        #endregion
    }
}
