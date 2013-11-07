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

            this.users_list.SelectionChanged += new SelectionChangedEventHandler(users_list_SelectionChanged);
        }

        void users_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            item_generic item = (item_generic)e.AddedItems[0];
            window_manager.open_collections_balloon(item.PointToScreen(new Point(0, 0)).Y, (string)item.username.Content);
        }

        void users_list_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            ManipulationDelta d = e.DeltaManipulation;
            if (d.Translation.X > 0)
            {
                
            }
        }

        void users_list_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.users_list;
            e.Mode = ManipulationModes.Translate;
        }

        public void list_all_users()
        {
            worker.DoWork += new DoWorkEventHandler(get_all_users);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_users);
            worker.RunWorkerAsync((object)this.users_list.Width);
        }

        public void display_all_users(object us, RunWorkerCompletedEventArgs e)
        {
            this.users_list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action( () =>
                {
                    List<user_item> users = (List<user_item>)e.Result;
                    manipulation_starting_handler start_handler = new manipulation_starting_handler(users_list_ManipulationStarting);
                    manipulation_delta_handler delta_handler = new manipulation_delta_handler(users_list_ManipulationDelta);
                    foreach (user_item u in users)
                    {
                        item_generic i = new item_generic();
                        i.username.Content = u.user.name;
                        //i.user_desc.Content = u.email;
                        i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.content.Visibility = System.Windows.Visibility.Collapsed;
                        i.set_touchevent(start_handler, delta_handler);
                        i.avatar.Source = u.img;
                        this.users_list.Items.Add(i);
                    }
                    this.users_list.Items.Refresh();
                }));
        }

        public void get_all_users(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from u in db.Users
                    orderby u.name
                    select u;
            List<user_item> users = new List<user_item>();
            foreach (User u in r)
            {
                user_item i = new user_item();
                ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath()+u.avatar));
                src.Freeze();
                i.img = src;
                i.user = u;
                users.Add(i);
            }
            e.Result = (object)users;
        }

        //private void open_observation_collection(object sender, TouchEventArgs e)
        //{
        //    item_generic item = (item_generic)sender;
        //    //item.username;
        //}
    }

    public class user_item
    {
        public ImageSource img;
        public User user;
    }
}
