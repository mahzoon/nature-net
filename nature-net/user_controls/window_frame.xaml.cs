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

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for window_frame.xaml
    /// </summary>
    public partial class window_frame : UserControl
    {
        public object WindowContent
        {
            get { return GetValue(WindowContentProperty); }
            set { SetValue(WindowContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowContentProperty =
            DependencyProperty.Register("WindowContent", typeof(object), typeof(window_frame), null);

        public window_frame()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.Width = 300;
            //this.title_bar.Background = new SolidColorBrush(Color.;
            //this.frame.BorderBrush = new SolidColorBrush(Color.;
            this.title_bar.Height = 40;
            this.change_view.Margin = new Thickness(2, 5, 5, 2);
            this.close.Margin = new Thickness(5, 5, 5, 2);
            this.window_icon.Margin = new Thickness(5, 5, 5, 5);
            this.window_icon.Width = 40;
            var b1 = new ImageBrush();
            b1.ImageSource = configurations.img_close_icon;
            this.close.Background = b1;
            var b2 = new ImageBrush();
            b2.ImageSource = configurations.img_change_view_stack_icon;
            this.close.Background = b2;
            this.window_icon.Source = configurations.img_collection_window_icon;
        }
    }
}
