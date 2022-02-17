using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClientDB.ViewModel;
using ClientDB.Model;

namespace ClientDB.View
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public StudentWindow(Student student = null)
        {
            InitializeComponent();
            DataContext = new EditVM(student, this.Close);
            //ToggleButton button = new ToggleButton();
            //button.Style = this.Resources["Toggle"] as Style;
            //button.Name = "asdd";
            //button.Width = 100;
            //button.Height = 100;
            //SemPanel.Children.Add(button);
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        void loadSem(Student student)
        {

        }
    }
}
