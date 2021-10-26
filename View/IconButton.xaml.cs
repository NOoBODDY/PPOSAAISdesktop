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
using MahApps.Metro.IconPacks;

namespace ClientDB.View
{
    /// <summary>
    /// Логика взаимодействия для IconButton.xaml
    /// </summary>
    public partial class IconButton : UserControl
    {



        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CircleFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Color), typeof(IconButton));


        public MahApps.Metro.IconPacks.PackIconMaterialKind Kind
        {
            get { return (MahApps.Metro.IconPacks.PackIconMaterialKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Kind.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register("Kind", typeof(MahApps.Metro.IconPacks.PackIconMaterialKind), typeof(IconButton), new UIPropertyMetadata(kindChange));

        static void kindChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var c = sender as IconButton;
            c.icon.Kind = (PackIconMaterialKind) e.NewValue;
        }


        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(IconButton),
            new UIPropertyMetadata(commandChange));
        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        static void commandChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var c = sender as IconButton;
            c.btn.Command = (ICommand)e.NewValue;
        }


        public int Width
        {
            get { return (int)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(int), typeof(IconButton), new UIPropertyMetadata(widthChange));
        static void widthChange (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var c = sender as IconButton;
            c.btn.Width =Convert.ToDouble(e.NewValue);
        }


        public int Height
        {
            get { return (int)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(int), typeof(IconButton), new UIPropertyMetadata(heightChange));

        static void heightChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var c = sender as IconButton;
            c.btn.Height = Convert.ToDouble(e.NewValue);
        }

        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(int), typeof(IconButton), new UIPropertyMetadata(IconWidthChange));
        static void IconWidthChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var c = sender as IconButton;
            c.icon.Width = Convert.ToDouble(e.NewValue);
        }


        public int IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(int), typeof(IconButton), new UIPropertyMetadata(IconHeightChange));

        static void IconHeightChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var c = sender as IconButton;
            c.icon.Height = Convert.ToDouble(e.NewValue);
        }



        public bool needT
        {
            get { return (bool)GetValue(needTProperty); }
            set { SetValue(needTProperty, value); }
        }

        // Using a DependencyProperty as the backing store for needT.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty needTProperty =
            DependencyProperty.Register("needT", typeof(bool), typeof(IconButton), new PropertyMetadata(false));



        public IconButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("loh");
        }
    }
}
