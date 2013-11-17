using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for users_listbox.xaml
    /// </summary>
    public partial class users_listbox : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public users_listbox()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.Width = 270;
            this.signup.user_desc.Visibility = System.Windows.Visibility.Collapsed;
            this.signup.content.Visibility = System.Windows.Visibility.Collapsed;
            this.signup.desc.Visibility = System.Windows.Visibility.Collapsed;
            this.signup.username.Content = "Signup";
            this.signup.Background = new SolidColorBrush(Colors.LightGreen);
            this.signup.username.Foreground = new SolidColorBrush(Colors.White);
            this.signup.user_desc.Foreground = new SolidColorBrush(Colors.White);
            this.signup.top_panel.Margin = new Thickness(13, 13, 13, 13);
            this.signup.avatar.Source = configurations.img_signup_icon;
        }

        public void list_all_users()
        {
            this.users_list.list_all_users();
        }
    }
}
