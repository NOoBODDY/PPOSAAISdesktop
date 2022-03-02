using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

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
     
        [JsonPropertyName("key")]
        public string TypeValue { get { return Enum.GetName(typeof(FilterType), Type); } }

        [JsonPropertyName("operation")]
        public string operation { get { return EnumHelper.GetDescription(this.FilterOperations); } }

        FilterOperations filterOperations;
        [JsonIgnore]
        public FilterOperations FilterOperations 
        { 
            get { return filterOperations; } 
            set { filterOperations = value; OnPropertyChanged("FilterOperations"); } 
        }

        string value;
        [JsonPropertyName("value")]
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

    public enum FilterOperations
    {
        [Description(">")]
        more,
        [Description("<")]
        less,
        [Description(":")]
        equal
    }

}
