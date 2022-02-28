using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientDB.Model;
using ClientDB.View;
using System.Net;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows;
using BFF.DataVirtualizingCollection.DataVirtualizingCollection;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Windows.Threading;

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

        IDataVirtualizingCollection<Student> students;
        public IDataVirtualizingCollection<Student> Students 
        {
            get { return students; }
            set { students = value; OnPropertyChanged("Students"); }
        }

        APIservice api;

        
        #region Filter Menu
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

        #region Add Student

        Student newStudent;
        public Student NewStudent { get { return newStudent; } set { newStudent = value; OnPropertyChanged("NewStudent"); } }


        #endregion



        #region Edit Student
        bool tabActiveEdit;
        public bool TabActiveEdit { get { return tabActiveEdit; } set { tabActiveEdit = value; OnPropertyChanged("TabActiveEdit"); } }
        Student selectedStudent;
        public Student SelectedStudent { get { return selectedStudent; } set { selectedStudent = value; OnPropertyChanged("SelectedStudent"); } }
        bool isEditable;
        public bool IsEditable { get { return isEditable; } set { isEditable = value; OnPropertyChanged("IsEditable"); } }

        #endregion
        #endregion

        #region Constructor
        public MainViewModel(APIservice API)
        {
            api = API;
            api.ExeptionNotify += ThrowMessage;
            Authorised = api.GetAccount();
            //StudentProvider provider = new StudentProvider(api);
            //Students = new AsyncVirtualizingCollection<Student>(provider, 100, 30000);

            Func<int, int, CancellationToken, Student[]> pageFetcher = (offset, pageSize, _) =>
            api.GetPageOfStudents(offset, offset + pageSize);

            Func<CancellationToken, int> countFetcher = _ =>
                api.GetStudentsCount();

            Func<int, int, CancellationToken, Task<Student[]>> TaskpageFetcher = (offset, pageSize, _) =>
                Task.Run<Student[]>(()=>api.GetPageOfStudents(offset, offset + pageSize));
            Func<CancellationToken, Task<int>> TaskcountFetcher = _ =>
                Task.Run<int>(()=> api.GetStudentsCount());
            Func<int, int, Student> placeholderFactory = (page, items) =>
                new Student();

            var notificationScheduler =
            new SynchronizationContextScheduler(
                new DispatcherSynchronizationContext(
                    Application.Current.Dispatcher));

            Students = DataVirtualizingCollectionBuilder.Build<Student>(notificationScheduler).Preloading(placeholderFactory).LeastRecentlyUsed(3).TaskBasedFetchers(TaskpageFetcher, TaskcountFetcher).AsyncIndexAccess(placeholderFactory);

            FilterMenuWidth = 335;
            FilterMenuHeight = 350;
            Filters = new ObservableCollection<Filter>();
            NewStudent = new Student();
            IsEditable = false;
        }
        #endregion

        #region Notify Message

        void ThrowMessage(string message)
        {
            MessageBox.Show(message);
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
                      Students.Reset();
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
                        NewStudent.id = null;
                        api.AddStudent(NewStudent);
                        NewStudent = new Student();
                        Students.Reset();
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
                        TabActiveEdit = true;
                    }));
            }
        }

        RelayCommand editModeChangeCommand;
        public RelayCommand EditModeChangeCommand
        {
            get
            {
                return editModeChangeCommand ??
                    (editModeChangeCommand = new RelayCommand(obj =>
                    {
                        IsEditable = !IsEditable;
                    }));
            }
        }

        RelayCommand saveStudent;
        public RelayCommand SaveStudent
        {
            get
            {
                return saveStudent ??
                    (saveStudent = new RelayCommand(obj =>
                    {
                        api.UpdateStudent(SelectedStudent);
                    }));
            }
        }

        #endregion
    }
}
