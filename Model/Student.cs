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

        ObservableCollection<Ticketextension> TicketExtensions;

        public ObservableCollection<Ticketextension> ticketExtensions { get { return TicketExtensions; } set { TicketExtensions = value; OnPropertyChanged("ticketExtensions"); } }

        [JsonProperty]
        public int? id;

        #region name
        string Firstname;
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
        public string email { get { return Email; } set { Email = value; OnPropertyChanged("email"); } }

        string Address;
        [JsonProperty]
        public string address { get { return Address; } set { Address = value; OnPropertyChanged("address"); } }

        #region Status
        [JsonProperty]
        public string status 
        { 
            get { return Enum.GetName(typeof(StudentStatusEnum), StatusEnum); } 
            set 
            {
                if (value != null)
                {
                    StatusEnum = (StudentStatusEnum)EnumHelper.GetEnum(value, typeof(StudentStatusEnum));
                    OnPropertyChanged("status");
                }
            } 
        }

        
        StudentStatusEnum statusEnum;
        [JsonIgnore]
        public StudentStatusEnum StatusEnum
        {
            get { return statusEnum; }
            set
            {
                statusEnum = value;
                OnPropertyChanged("StatusEnum");
            }
        }

        #endregion
        string DateOfLeaving;
        [JsonProperty]
        public string dateOfLeaving { get { return DateOfLeaving; } set { DateOfLeaving = value; OnPropertyChanged("dateOfLeaving"); } }


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
        public string semesterTitle { get; set; }
        public int? studentId { get; set; }

        static public PaymentSemester FromSemester(Semester semester)
        {
            PaymentSemester payment = new PaymentSemester();
            payment.semesterTitle = semester.title;
            return payment;
        }
    }


}
