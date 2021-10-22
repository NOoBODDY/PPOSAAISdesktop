using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace ClientDB.Model
{
    class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        string firstName;
        string surName;
        string lastName;
        string group;
        string faculty;
        string profileTicket;
        string studyForm;
        string yearOfJoining;
        bool paymentForCurrentSemester;

        public Student(JObject StdObject)
        {
            FirstName = (string) StdObject["firstName"];
            SurName = (string)StdObject["surName"];
            LastName = (string)StdObject["lastName"];
            Group = (string)StdObject["group"];
            ProfileTicket = (string)StdObject["profileTicket"];
            PaymentForCurrentSemester = (bool)StdObject["paymentForCurrentSemester"];

            if (FirstName == null || SurName == null || LastName == null || Group == null || ProfileTicket == null || PaymentForCurrentSemester == null)
            {
                throw new InvalidEnumArgumentException("JSON havent parsed");
            }
        }


        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string SurName
        {
            get { return surName; }
            set
            {
                surName = value;
                OnPropertyChanged("SurName");
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Group
        {
            get { return group; }
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
        }

        public string Faculty
        {
            get { return faculty; }
            set
            {
                faculty = value;
                OnPropertyChanged("Faculty");
            }
        }
        public string ProfileTicket
        {
            get { return profileTicket; }
            set
            {
                profileTicket = value;
                OnPropertyChanged("ProfileTicket");
            }
        }
        public string StudyForm
        {
            get { return studyForm; }
            set
            {
                studyForm = value;
                OnPropertyChanged("StudyForm");
            }
        }
        public string YearOfJoining
        {
            get { return yearOfJoining; }
            set
            {
                yearOfJoining = value;
                OnPropertyChanged("YearOfJoining");
            }
        }
        public bool PaymentForCurrentSemester
        {
            get { return paymentForCurrentSemester; }
            set
            {
                paymentForCurrentSemester = value;
                OnPropertyChanged("PaymentForCurrentSemester");
            }
        }
    }
}
