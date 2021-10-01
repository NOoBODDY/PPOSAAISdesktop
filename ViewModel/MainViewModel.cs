using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientDB.Model;

namespace ClientDB.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        Student selectedStudent;
        Account authorised;

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

        public MainViewModel()
        {
            Authorised = new Account { Name = "Леха", Post = "Лепеха" };

            Students = new ObservableCollection<Student>
            {
                new Student {FirstName = "goloshapov", SurName = "D", LastName = "", Group = "4941", Faculty = "4",ProfileTicket = "123456", StudyForm = "B", YearOfJoining = "2019"},
                new Student {FirstName = "gukov", SurName = "U", LastName = "", Group = "4941", Faculty = "4",ProfileTicket = "123457", StudyForm = "B", YearOfJoining = "2019"},
                new Student {FirstName = "ostashov", SurName = "A", LastName = "", Group = "4941", Faculty = "4",ProfileTicket = "123458", StudyForm = "B", YearOfJoining = "2019"}
            };
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Students.Add(new Student { FirstName = "gorbunov", SurName = "N", LastName = "", Group = "4941", Faculty = "4", ProfileTicket = "123459", StudyForm = "B", YearOfJoining = "2019" });
                  }));
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

                    }));
            }
        }


    }
}
