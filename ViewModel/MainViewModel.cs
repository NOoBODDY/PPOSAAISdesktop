using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientDB.Model;
using ClientDB.View;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ClientDB.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        static string stdUrl = "/api/students";
        string URL;
        #region //interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
        #region //propetries
        Student selectedStudent;
        Account authorised;
        public Action CloseAction { get; set; }
        public Action MinimizeAction { get; set; }
        public Action MaximizeAction { get; set; }

        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<string> Filters { get; set; }

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        public Account Authorised
        {
            get { return authorised; }
            set
            {
                authorised = value;
                OnPropertyChanged("Authorised");
            }
        }

        #endregion
        public MainViewModel()
        {
            
            Authorised = new Account { Name = "Алексей Осташов", Post = "Олух" };
            Students = new ObservableCollection<Student>();
            Students.Add(new Student { FirstName = "Алексей", SurName = "Осташов", LastName = "Сергеевич", Group = "4941", ProfileTicket = "123456" });
            Students.Add(new Student { FirstName = "Дмитрий", SurName = "Голощапов", LastName = "Алексеевич", Group = "4941", ProfileTicket = "123457" });
            Students.Add(new Student { FirstName = "Юрий", SurName = "Гуков", LastName = "Игоревич", Group = "4941", ProfileTicket = "123458" });
            Students.Add(new Student { FirstName = "Никита", SurName = "Горбунов", LastName = "Сергеевич", Group = "4941", ProfileTicket = "123459" });;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("Images/alecksei.jpg", UriKind.Relative);
            bi.EndInit();
            Authorised.Avatar = bi;
 
        }


        public MainViewModel(string url)
        {
            URL = url;
            Students = new ObservableCollection<Student>();
        }

        #region //commands
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      StudentWindow window = new StudentWindow();
                      window.ShowDialog();
                  }));
            }
        }

        private RelayCommand getAllCommand;
        public RelayCommand GetAllCommand
        {
            get
            {
                return getAllCommand ??
                    (getAllCommand = new RelayCommand(obj =>
                    {
                        WebRequest webRequest = WebRequest.Create(URL + stdUrl + "/getAll");
                        webRequest.Method = "GET";
                        webRequest.Headers.Add("Authorization", "Bearer_" + Authorised.JWT);
                        webRequest.ContentType = "application/x-www-form-urlencoded";
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

                        JArray StdArray = JArray.Parse(response);
                        foreach ( JObject i in StdArray)
                        {
                            Students.Add(new Student(i));
                        }

                    }));
            }
        }

        private RelayCommand maximizeCommand;
        public RelayCommand MaximizeCommand
        {
            get
            {
                return maximizeCommand ??
                    (maximizeCommand = new RelayCommand(obj => { MaximizeAction(); }));
            }
        }
        private RelayCommand minimizeCommand;
        public RelayCommand MinimizeCommand
        {
            get
            {
                return minimizeCommand ??
                    (minimizeCommand = new RelayCommand(obj => { MinimizeAction(); }));
            }
        }
        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                    (closeCommand = new RelayCommand(obj => { CloseAction(); }));
            }
        }

        RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand(obj =>
                    {

                    }));
            }
        }
        RelayCommand delCommand;
        public RelayCommand DelCommand
        {
            get
            {
                return delCommand ??
                    (delCommand = new RelayCommand(obj =>
                    {

                    }));
            }
        }
        #endregion
    }
}
