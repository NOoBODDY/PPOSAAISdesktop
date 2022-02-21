using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace ClientDB.Model
{
    public class Account : INotifyPropertyChanged
    {
        #region interface
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        Account()
        {
        }


        string Username;
        string base64Avatar;
        public string avatar { get { return base64Avatar; } set { base64Avatar = value; OnPropertyChanged("avatar"); OnPropertyChanged("Avatar"); } }
        string FullName;
        ObservableCollection<Role> Roles;
        public ObservableCollection<Role> roles { get { return Roles; } set { Roles = value; OnPropertyChanged("roles"); } }

        public Role Role { get { return roles !=null && roles.Count != 0 ? roles[0]: new Role { title= ""}; } set { roles[0] = value; OnPropertyChanged("Role"); } }

        public string fullname
        {
            get { return FullName; }
            set
            {
                FullName = value;
                OnPropertyChanged("fullname");
            }
        }

        string post;
        string pass;
        string ava;
        string Email;

        public string username
        {
            get { return Username; }
            set
            {
                Username = value;
                OnPropertyChanged("username");
            }
        }
        [JsonIgnore]
        public string Post
        {
            get { return post; }
            set
            {
                post = value;
                OnPropertyChanged("Post");
            }
        }

        public string password
        {
            get { return pass; }
            set
            {
                pass = value;
                OnPropertyChanged("password");
            }
        }

        [JsonIgnore]
        public BitmapImage Avatar { 
            get 
            {
                var image = Base64StringToImage(avatar);
                return ConvertImageToBitmapImage(image); 
            }}

        



        public string email{ get; set; }


        public static System.Drawing.Image Base64StringToImage(string base64String)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;
            return System.Drawing.Image.FromStream(memoryStream,false,false);
        }

        public static BitmapImage ConvertImageToBitmapImage(System.Drawing.Image image)
        {
            using (var memory = new MemoryStream())
            {
                image.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

    }

    public class Role : INotifyPropertyChanged
    {
        #region interface
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
        int Id;
        string Title;
        int Rank;
        ObservableCollection<Authoriti> Authorities;
        public int id { get { return Id; } set { Id = value; OnPropertyChanged("id"); } }
        public string title { get { return Title; } set { Title = value; OnPropertyChanged("title"); } }
        public ObservableCollection<Authoriti> authorities { get { return Authorities; } set { Authorities = value; OnPropertyChanged("authorities");}  }

    }

    public class Authoriti : INotifyPropertyChanged
    {
        #region interface
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
        int Id;
        string Title;

        public int id { get { return Id; } set { Id = value; OnPropertyChanged("id"); } }
        public string title { get { return Title; } set { Title = value; OnPropertyChanged("title"); } }

    }
}
