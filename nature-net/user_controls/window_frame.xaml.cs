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
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for window_frame.xaml
    /// </summary>
    public partial class window_frame : UserControl
    {
        public window_frame()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.Width = 300;
            //this.title_bar.Background = new SolidColorBrush(Color.;
            //this.frame.BorderBrush = new SolidColorBrush(Color.;
            this.title_bar.Height = 40;
            this.window_icon.Width = 40;
            var b1 = new ImageBrush();
            b1.ImageSource = configurations.img_close_icon;
            this.close.Background = b1;
            var b2 = new ImageBrush();
            b2.ImageSource = configurations.img_change_view_stack_icon;
            this.change_view.Background = b2;
            this.window_icon.Source = configurations.img_collection_window_icon;

            this.close.Click += new RoutedEventHandler(close_Click);
            this.change_view.Click += new RoutedEventHandler(change_view_Click);
        }

        public void set_title(string t)
        {
            this.title.Text = t;
        }

        public void set_icon(ImageSource ico)
        {
            this.window_icon.Source = ico;
        }

        public void hide_change_view()
        {
            this.change_view.Visibility = System.Windows.Visibility.Hidden;
        }

        void change_view_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        void close_Click(object sender, RoutedEventArgs e)
        {
            window_manager.close_window(this);
        }
    }
}
