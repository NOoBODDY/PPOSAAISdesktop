using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientDB.Model;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ClientDB.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        static string stdUrl = "/api/students";
        string URL;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

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

        public MainViewModel(string url)
        {
            URL = url;
            Students = new ObservableCollection<Student>();
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      //Students.Add(new Student { FirstName = "gorbunov", SurName = "N", LastName = "", Group = "4941", Faculty = "4", ProfileTicket = "123459", StudyForm = "B", YearOfJoining = "2019" });
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
    }
}
