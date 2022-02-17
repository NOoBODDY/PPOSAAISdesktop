using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace ClientDB.Model
{
    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public string firstname;
        public string middlename;
        public string lastname;
        public string group;
        public string faculty;
        public string profileTicketNumber;
        public string formOfStudy;
        public string dateOfEntry;
        public bool paymentForCurrentSemester;

        public int id { get; set; }
        public object email { get; set; }
        public string phone { get; set; }
        public object address { get; set; }
        public string status { get; set; }
        public object dateOfLeaving { get; set; }
        public object[] materialAidList { get; set; }
        public Ticketextension[] ticketExtensions { get; set; }
        public object[] rolesInGroup { get; set; }
        public object[] rolesInPPOSA { get; set; }
        public object[] documents { get; set; }

        //public class Rootobject
        //{
        //    public int id { get; set; }
        //    public string firstname { get; set; }
        //    public string middlename { get; set; }
        //    public string lastname { get; set; }
        //    public object email { get; set; }
        //    public string group { get; set; }
        //    public string phone { get; set; }
        //    public string formOfStudy { get; set; }
        //    public int profileTicketNumber { get; set; }
        //    public object address { get; set; }
        //    public string status { get; set; }
        //    public string dateOfEntry { get; set; }
        //    public object dateOfLeaving { get; set; }
        //    public object[] materialAidList { get; set; }
        //    public Ticketextension[] ticketExtensions { get; set; }
        //    public object[] rolesInGroup { get; set; }
        //    public object[] rolesInPPOSA { get; set; }
        //    public object[] documents { get; set; }
        //}

       

        public Student()
        {

        }


        public Student(JObject StdObject)
        {
            FirstName = (string)StdObject["firstName"];
            SurName = (string)StdObject["surName"];
            LastName = (string)StdObject["lastName"];
            Group = (string)StdObject["group"];
            ProfileTicket = (string)StdObject["profileTicket"];
            PaymentForCurrentSemester = (bool)StdObject["paymentForCurrentSemester"];

            if (FirstName == null || SurName == null || LastName == null || Group == null || ProfileTicket == null)
            {
                throw new InvalidEnumArgumentException("JSON havent parsed");
            }
        }


        public string FirstName
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string SurName
        {
            get { return middlename; }
            set
            {
                middlename = value;
                OnPropertyChanged("SurName");
            }
        }

        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
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
            get { return profileTicketNumber; }
            set
            {
                profileTicketNumber = value;
                OnPropertyChanged("ProfileTicket");
            }
        }
        public string StudyForm
        {
            get { return formOfStudy; }
            set
            {
                formOfStudy = value;
                OnPropertyChanged("StudyForm");
            }
        }
        public string YearOfJoining
        {
            get { return dateOfEntry; }
            set
            {
                dateOfEntry = value;
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

    public class Ticketextension
    {
        public int id { get; set; }
        public Semester semester { get; set; }
        public bool payment { get; set; }
    }

    public class Semester
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}
