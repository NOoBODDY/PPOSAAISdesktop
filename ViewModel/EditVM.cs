using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using ClientDB.Model;

namespace ClientDB.ViewModel
{
    class EditVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Action CloseAction { get; set; }
        RelayCommand closeCommand;
        Student selected;
        public Student Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                                    (closeCommand = new RelayCommand(obj => { CloseAction(); }));
            }
        }
        public EditVM (Student student, Action close)
        {
            Selected = student;
            CloseAction = close;
        }

    }
}
