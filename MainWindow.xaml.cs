using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientDB.View;
using ClientDB.ViewModel;
using Unity;


namespace ClientDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IUnityContainer container = new UnityContainer();
            LoginControl loginControl = new LoginControl();
            container.RegisterInstance<IPasswordSupplier>(loginControl);
            loginMV MVlogin = new loginMV(container);
            loginControl.DataContext = MVlogin;
            DataContext = MVlogin;
            Grid.SetRow(loginControl, 2);
            mainGrid.Children.Add(loginControl);
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void PackIconMaterial_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void OpenTable(object sender, RoutedEventArgs e)
        {
            TablePage tablePage = new TablePage();
            tablePage.Show();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            TablePage tablePage = new TablePage();
            tablePage.Show();
        }

    }
}
