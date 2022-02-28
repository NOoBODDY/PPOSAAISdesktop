using System;
using System.ComponentModel;
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
using System.Windows.Shapes;
using ClientDB.ViewModel;

namespace ClientDB.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        bool fullScreen = false;
        bool theme = true;
        public MainView(APIservice api)
        {
            InitializeComponent();
            DataContext = new MainViewModel(api);
            
        }


        private void CloseBT(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MinimizeBT(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MaxBT(object sender, RoutedEventArgs e)
        {
            if (!fullScreen)
            {
                this.WindowState = WindowState.Maximized;
                fullScreen = true;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                fullScreen = false;
            }
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

        private void DragAndMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DependencyProperty dependencyProperty = new DependencyProperty();
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromName("ItemsSource", list.GetType(), list.GetType());

            list.InvalidateProperty(descriptor.DependencyProperty);
        }
    }
}
