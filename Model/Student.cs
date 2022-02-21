using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

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

        string Firstname;
        string Middlename;
        string Lastname;
        string Group;
        string faculty;
        string ProfileTicketNumber;
        string FormOfStudy;
        string DateOfEntry;
        FormOfStudyEnum formofstudyenum;

        public FormOfStudyEnum Formofstudyenum
        {
            get { return formofstudyenum;}
            set { formofstudyenum = value; OnPropertyChanged("Formofstudyenum"); }
        }

        [JsonIgnore]
        public int id;
        string Phone;
        [JsonIgnore]
        public object email { get; set; }
        
        [JsonIgnore]
        public object address { get; set; }
        [JsonIgnore]
        public string status { get; set; }
        [JsonIgnore]
        public object dateOfLeaving { get; set; }
        [JsonIgnore]
        public object[] materialAidList { get; set; }
        [JsonIgnore]
        public Ticketextension[] ticketExtensions { get; set; }
        [JsonIgnore]
        public object[] rolesInGroup { get; set; }
        [JsonIgnore]
        public object[] rolesInPPOSA { get; set; }
        [JsonIgnore]
        public object[] documents { get; set; }
        public string firstname
        {
            get { return Firstname; }
            set
            {
                Firstname = value;
                OnPropertyChanged("firstname");
            }
        }

        public string lastname
        {
            get { return Lastname; }
            set
            {
                Lastname = value;
                OnPropertyChanged("lastname");
            }
        }

        public string middlename
        {
            get { return Middlename; }
            set
            {
                Middlename = value;
                OnPropertyChanged("middlename");
            }
        }

        public string group
        {
            get { return Group; }
            set
            {
                Group = value;
                OnPropertyChanged("group");
            }
        }
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
        public string profileTicketNumber
        {
            get { return ProfileTicketNumber; }
            set
            {
                ProfileTicketNumber = value;
                OnPropertyChanged("profileTicketNumber");
            }
        }

        public string formOfStudy
        {
            get { return GetDescription(formofstudyenum); }
            set
            {
                FormOfStudy = value;
                OnPropertyChanged("formOfStudy");
            }
        }
        public string dateOfEntry
        {
            get { return DateOfEntry; }
            set
            {
                DateOfEntry = value;
                OnPropertyChanged("dateOfEntry");
            }

        }
        
        public string phone
        {
            get { return Phone; } 
            set
            {
                Phone = value;
                OnPropertyChanged("phone");
            }
        }
       



        public Student()
        {

        }

        static string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
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
