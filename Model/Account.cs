using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientDB.Model
{
    class Account : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        string name;
        string post;
        string jwt;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Post
        {
            get { return post; }
            set
            {
                post = value;
                OnPropertyChanged("Post");
            }
        }

        public string JWT
        {
            get { return jwt; }
            set
            {
                jwt = value;
                OnPropertyChanged("JWT");
            }
        }
    }
}
