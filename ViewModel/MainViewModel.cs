using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientDB.Model;
using ClientDB.View;
using ClientDB.VirtualizingCollection;
using System.Net;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ClientDB.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region //interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
        #region //propetries

        Account authorised;

        public Account Authorised
        {
            get { return authorised; }
            set
            {
                authorised = value;
                OnPropertyChanged("Authorised");
            }
        }

        AsyncVirtualizingCollection<Student> students;
        public AsyncVirtualizingCollection<Student> Students 
        {
            get { return students; }
            set { students = value; OnPropertyChanged("Students"); }
        }

        APIservice api;

        Student selectedStudent;
        public Student SelectedStudent { get { return selectedStudent; } set { selectedStudent = value; OnPropertyChanged("SelectedStudent"); } } 

        #region FilterMenu
        bool filterMenuVisibility = false;
        public Visibility FilterMenuVisibility
        {
            get { return filterMenuVisibility ? Visibility.Visible : Visibility.Collapsed; }
            set
            {
                filterMenuVisibility = value == Visibility.Visible;
                OnPropertyChanged("FilterMenuVisibility");
            }
        }

        private double _panelX;
        private double _panelY;

        public double PanelX
        {
            get { return _panelX; }
            set
            {
                if (value.Equals(_panelX)) return;
                _panelX = value;
                OnPropertyChanged("PanelX");
            }
        }

        public double PanelY
        {
            get { return _panelY; }
            set
            {
                if (value.Equals(_panelY)) return;
                _panelY = value;
                OnPropertyChanged("PanelY");
            }
        }

        private double filterMenuX;
        private double filterMenuY;
        public double FilterMenuX
        {
            get { return filterMenuX; }
            set
            {
                ;
                filterMenuX = value;
                OnPropertyChanged("FilterMenuX");
            }
        }

        public double FilterMenuY
        {
            get { return filterMenuY; }
            set
            {
                filterMenuY = value;
                OnPropertyChanged("FilterMenuY");
            }
        }

        double filterMenuHeight;
        public double FilterMenuHeight { get { return filterMenuHeight; } set { filterMenuHeight = value; OnPropertyChanged("FilterMenuHeight"); } }
        double filterMenuWidth;
        public double FilterMenuWidth { get { return filterMenuWidth; } set { filterMenuWidth = value; OnPropertyChanged("FilterMenuWidth"); } }

        public ObservableCollection<Filter> Filters { get; set; }
        Filter selectedFilter;
        public Filter SelectedFilter { get { return selectedFilter; } set { selectedFilter = value; OnPropertyChanged("SelectedFilter"); } }

        


        #endregion

        #region AddStudent

        Student newStudent;
        public Student NewStudent { get { return newStudent; } set { newStudent = value; OnPropertyChanged("NewStudent"); } }


        #endregion

        bool tabActiveEdit;
        public bool TabActiveEdit { get { return tabActiveEdit; } set { tabActiveEdit = value; OnPropertyChanged("TabActiveEdit"); } }






        #endregion

        #region Constructor
        public MainViewModel(APIservice API)
        {
            api = API;
            Authorised = api.GetAccount();
            StudentProvider provider = new StudentProvider(api);
            Students = new AsyncVirtualizingCollection<Student>(provider, 100, 30000);
            FilterMenuWidth = 300;
            FilterMenuHeight = 100;
            Filters = new ObservableCollection<Filter>();
            NewStudent = new Student();

        }
        #endregion


        #region Commands

        #region FilterMenu
        private RelayCommand openFilterMenu;
        public RelayCommand OpenFilterMenu
        {
            get
            {
                return openFilterMenu ??
                  (openFilterMenu = new RelayCommand(obj =>
                  {

                      FilterMenuVisibility = Visibility.Visible;
                      FilterMenuX = PanelX - FilterMenuWidth;
                      FilterMenuY = PanelY - FilterMenuHeight;
                  }));
            }
        }

        private RelayCommand closeFilterMenu;
        public RelayCommand CloseFilterMenu
        {
            get
            {
                return closeFilterMenu ??
                  (closeFilterMenu = new RelayCommand(obj =>
                  {
                      FilterMenuVisibility = Visibility.Collapsed;
                      api.Filters = Filters.ToList();
                  }));
            }
        }

        private RelayCommand addFilter;
        public RelayCommand AddFilter
        {
            get
            {
                return addFilter ??
                  (addFilter = new RelayCommand(obj =>
                  {
                      Filters.Add(new Filter());
                  }));
            }
        }

        private RelayCommand deleteFilter;
        public RelayCommand DeleteFilter
        {
            get
            {
                return deleteFilter ??
                  (deleteFilter = new RelayCommand(obj =>
                  {
                      Filters.Remove((Filter)obj);
                  }));
            }
        }
        #endregion



        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Task task = new Task(() =>
                        {
                             api.AddStudent(NewStudent);
                             NewStudent = new Student();
                             lock(students)
                             {
                                 StudentProvider provider = new StudentProvider(api);
                                 Students = new AsyncVirtualizingCollection<Student>(provider, 100, 30000);
                             }
                        });
                      
                  }));
            }
        }

       

       


        RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand(obj =>
                    {
                        NewStudent = SelectedStudent;
                        TabActiveEdit = true;
                    }));
            }
        }
        
        #endregion
    }
}
