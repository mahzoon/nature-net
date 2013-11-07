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

namespace nature_net
{
    /// <summary>
    /// Interaction logic for main_window.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            iniparser parser = new iniparser();
            configurations.SetSettingsFromConfig(parser);
            configurations.LoadIconImages();

            //Static Configuration Values:
            this.users_listbox.signup.avatar.Source = configurations.img_signup_icon;
            
            var b = new ImageBrush();
            b.ImageSource = configurations.img_background_pic;
            this.workspace.Background = b;

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.users_listbox.list_all_users();
            this.design_ideas_listbox.list_all_design_ideas();
            window_manager.main_canvas = this.workspace;
        }
    }
}
