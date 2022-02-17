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
        static string loginUrl = "/api/login";

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

        public loginMV( IUnityContainer unityContainer, string url)
        {
            container = unityContainer;
            URL = url;
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

                            //TablePage page = new TablePage(UserName,(string) responceObj["token"], null, URL);
                            //page.Show();
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

    }
}
