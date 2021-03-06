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
    public partial class LoginWindow : Window
    {
        bool theme = true;
        public LoginWindow()
        {
           
            InitializeComponent();
            IUnityContainer container = new UnityContainer();
            LoginControl loginControl = new LoginControl();
            container.RegisterInstance<IPasswordSupplier>(loginControl);
            loginMV MVlogin = new loginMV(container);
            MVlogin.CloseAction = this.Close;
            loginControl.DataContext = MVlogin;
            DataContext = MVlogin;
            Grid.SetRow(loginControl, 2);
            mainGrid.Children.Add(loginControl);
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseBT(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            if (theme)
            {
                theme = !theme;
                var delUri = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
                var uri = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.Remove(delUri);
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            else
            {
                theme = !theme;
                var delUri = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
                var uri = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.Remove(delUri);
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
        }

    }
}
