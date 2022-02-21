using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ClientDB.Model
{
    public class Filter : INotifyPropertyChanged
    {
        #region interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region propetries
        FilterType type;
        [JsonIgnore]
        public FilterType Type { get { return type; } set { type = value; OnPropertyChanged("Type"); } }
        public string key;
        [JsonIgnore]
        public string TypeValue { get { return key; } set { key = value; OnPropertyChanged("TypeValue"); } }

        public string value;
        [JsonIgnore]
        public string FilterValue { get { return this.value; } set { this.value = value; OnPropertyChanged("FilterValue"); } }
        #endregion
    }
    public enum FilterType
    {
        [Description ("Группа")]
        group,
        [Description("Институт")]
        faculty,
        [Description("Имя")]
        firstname,
        [Description("Отчество")]
        middlename,
        [Description("Фамилия")]
        lastname,
        [Description("Номер профбилета")]
        profileTicketNumber,
        [Description("Форма обучения")]
        formOfStudy,
        [Description("Дата вступления")]
        dateOfEntry
    }
}
