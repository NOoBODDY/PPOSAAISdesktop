using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientDB.Model;
using System.Windows;
using Unity;
using ClientDB.View;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

//TODO 
//(1)добавить проверку на админа, когда будет функционал



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
        string URL;
        Window window;
        static string loginUrl = "/api/login";


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

        public loginMV( IUnityContainer unityContainer, string url , Window view)
        {
            UserName = "UserName";
            container = unityContainer;
            URL = url;
            window = view;
        }

        public string Password
        {
            get
            {
                IPasswordSupplier passwordSupplier = container.Resolve<IPasswordSupplier>();
                return passwordSupplier.GetPassword();
            }
        }

        public int PassLength
        {
            get
            {
                IPasswordSupplier passwordSupplier = container.Resolve<IPasswordSupplier>();
                return passwordSupplier.PasswordLength();
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
                        MessageBox.Show(UserName + " " + Password); //удалить
                        WebRequest webRequest = WebRequest.Create(URL + loginUrl);
                        webRequest.Method = "POST";
                        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes("username=" + UserName + "&" + "password=" + Password);
                        webRequest.ContentType = "application/x-www-form-urlencoded";
                        webRequest.ContentLength = byteArray.Length;
                        //записываем данные в поток запроса
                        using (Stream dataStream = webRequest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);
                        }
                        WebResponse webResponse = webRequest.GetResponse();

                        string response;
                        using (Stream stream = webResponse.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                response = reader.ReadToEnd();
                            }
                        }
                        webResponse.Close();

                        JObject responceObj = JObject.Parse(response);

                        if ((string)responceObj["status"] == "success")
                        {
                            //var handler = new JwtSecurityTokenHandler();
                            //var jsonToken = handler.ReadToken( (string) responceObj["token"]);
                            //var tokenS = jsonToken as JwtSecurityToken;

                            //TODO (1)

                            TablePage page = new TablePage(UserName,(string) responceObj["token"], null);
                            page.Show();
                            window.Close();
                        }
                        else
                        {
                            MessageBox.Show("Иди нахуй");
                        }

                    }));
            }
        }


    }
}
