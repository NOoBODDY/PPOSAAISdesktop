using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ClientDB.Model
{
    public class Student : INotifyPropertyChanged
    {
        #region Interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region Properties

        public int? id;

        #region name
        string Firstname;
        public string firstname
        {
            get { return Firstname; }
            set
            {
                Firstname = value;
                OnPropertyChanged("firstname");
            }
        }

        string Middlename;
        public string middlename
        {
            get { return Middlename; }
            set
            {
                Middlename = value;
                OnPropertyChanged("middlename");
            }
        }

        string Lastname;
        public string lastname
        {
            get { return Lastname; }
            set
            {
                Lastname = value;
                OnPropertyChanged("lastname");
            }
        }
        #endregion

        string Group;
        public string group
        {
            get { return Group; }
            set
            {
                Group = value;
                OnPropertyChanged("group");
            }
        }

        string faculty;
        [JsonIgnore]
        public string Faculty
        {
            get { return faculty; }
            set
            {
                faculty = value;
                OnPropertyChanged("Faculty");
            }
        }

        string ProfileTicketNumber;
        public string profileTicketNumber
        {
            get { return ProfileTicketNumber; }
            set
            {
                ProfileTicketNumber = value;
                OnPropertyChanged("profileTicketNumber");
            }
        }
        #region FormOfStudy
        FormOfStudyEnum formofstudyenum;
        [JsonIgnore]
        public FormOfStudyEnum Formofstudyenum
        {
            get { return formofstudyenum; }
            set { formofstudyenum = value; OnPropertyChanged("Formofstudyenum"); }
        }
        public string formOfStudy
        {
            get { return Enum.GetName(typeof(FormOfStudyEnum), formofstudyenum); }
            set
            {
                if (value != null)
                {
                    Formofstudyenum = (FormOfStudyEnum)EnumHelper.GetEnum(value, typeof(FormOfStudyEnum));
                    OnPropertyChanged("formOfStudy");
                }
            }
        }
        #endregion

        string DateOfEntry;
        public string dateOfEntry
        {
            get { return DateOfEntry; }
            set
            {
                DateOfEntry = value;
                OnPropertyChanged("dateOfEntry");
            }

        }

        string Phone;
        public string phone
        {
            get { return Phone; }
            set
            {
                Phone = value;
                OnPropertyChanged("phone");
            }
        }

        string Email;
        public string email { get { return Email; } set { Email = value; OnPropertyChanged("email"); } }

        string Address;
        public string address { get { return Address; } set { Address = value; OnPropertyChanged("address"); } }

        string Status;
        public string status { get { return Status; } set { Status = value; OnPropertyChanged("status"); } }

        string DateOfLeaving;
        public string dateOfLeaving { get { return DateOfLeaving; } set { DateOfLeaving = value; OnPropertyChanged("dateOfLeaving"); } }

        ObservableCollection<Ticketextension> TicketExtensions;
        [JsonIgnore]
        public ObservableCollection<Ticketextension> ticketExtensions { get { return TicketExtensions; } set { TicketExtensions = value; OnPropertyChanged("ticketExtensions"); } }

        #endregion





        [JsonIgnore]
        public object[] materialAidList { get; set; }
        [JsonIgnore]
        public object[] rolesInGroup { get; set; }
        [JsonIgnore]
        public object[] rolesInPPOSA { get; set; }
        [JsonIgnore]
        public object[] documents { get; set; }
        
    }

    public class Ticketextension
    {
        #region Interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
        public int id { get; set; }


        Semester Semester;
        public Semester semester { get { return Semester; } set { Semester = value; OnPropertyChanged("semester"); } }
        bool Payment;
        public bool payment { get { return Payment; } set { Payment = value; OnPropertyChanged("payment"); } }
    }

    public class Semester
    {
        #region Interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
        public int id { get; set; }

        string Title;
        public string title { get { return Title; } set { Title = value; OnPropertyChanged("title"); } }
    }

    public class PaymentSemester
    {
        public int id { get; set; }
        public string semesterTitle { get; set; }
        public int? studentId { get; set; }

        static public PaymentSemester FromSemester(Semester semester)
        {
            PaymentSemester payment = new PaymentSemester();
            payment.id = semester.id;
            payment.semesterTitle = semester.title;
            return payment;
        }
    }


}
