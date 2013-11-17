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
    /// Interaction logic for tab_control.xaml
    /// </summary>
    public partial class tab_control : UserControl
    {
        public tab_control()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.users_listbox.signup.avatar.Source = configurations.img_signup_icon;
        }

        public void load_control(bool left)
        {
            this.tab.BorderThickness = new Thickness(0, 0, 2, 0);
            if (!left)
            {
                foreach (TabItem ti in this.tab.Items)
                {
                    Transform t = new RotateTransform(-90);
                    ((TextBlock)ti.Header).LayoutTransform = t;
                }
                this.tab.TabStripPlacement = Dock.Left;
                this.tab.BorderThickness = new Thickness(2, 0, 0, 0);
            }
            this.users_listbox.list_all_users();
            this.activities_listbox.list_all_activities();
            this.design_ideas_listbox.list_all_design_ideas();
        }
    }
}
