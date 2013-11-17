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
    /// Interaction logic for item_generic.xaml
    /// </summary>
    public partial class item_generic : UserControl
    {
        public item_generic()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.top_panel.Margin = new Thickness(10, 5, 10, 5);
            this.avatar.Width = 50;
            this.avatar.Height = 50;
            this.topright_panel.Margin = new Thickness(10, 0, 10, 0);
            this.desc.Margin = new Thickness(10, 0, 10, 0);
            this.content.Margin = new Thickness(10, 0, 10, 10);
            this.username.FontFamily = new FontFamily("Segoe UI"); this.username.FontSize = 12;
            this.user_desc.FontFamily = new FontFamily("Segoe UI"); this.user_desc.FontSize = 12;
            this.desc.FontFamily = new FontFamily("Segoe UI"); this.desc.FontSize = 12;
            this.content.FontFamily = new FontFamily("Segoe UI"); this.content.FontSize = 12;
        }

        public void set_touchevent(avatar_touch_down_handler touch_handler)
        {
            this.avatar.PreviewTouchDown += new EventHandler<TouchEventArgs>(touch_handler);
        }

        public override string ToString()
        {
            string id = "-1";
            if (this.Tag != null)
                id = ((int)this.Tag).ToString();
            string c = "";
            if (((AccessText)this.content.Content).Text != null)
                c = ((AccessText)this.content.Content).Text;
            string source = "";
            if (this.avatar.Source != null)
                source = this.avatar.Source.ToString();
            string result = id + ";" + source + ";" + this.username.Content +
                ";" + this.user_desc.Content + ";" + this.desc.Content + ";" + c;
            return result;
        }
    }

    public delegate void manipulation_starting_handler(object sender, ManipulationStartingEventArgs e);
    public delegate void manipulation_delta_handler(object sender, ManipulationDeltaEventArgs e);
    public delegate void avatar_touch_down_handler(object sender, TouchEventArgs e);
}
