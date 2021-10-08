﻿using System;
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
    /// Логика взаимодействия для TablePage.xaml
    /// </summary>
    public partial class TablePage : Window
    {
        public TablePage(string username, string jwt, string post)
        {
            InitializeComponent();

            MainViewModel VM = new MainViewModel();
            VM.Authorised = new Model.Account {  Name = username, JWT = jwt , Post = post};
            DataContext = VM;
            
        }
        private void CloseBT(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void MinBT(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MaxBT(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
