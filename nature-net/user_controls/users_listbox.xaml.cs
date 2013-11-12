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

            //this.users_list.PreviewDragLeave += new DragEventHandler(users_list_PreviewDragLeave);
            //this.users_list.PreviewTouchDown += new EventHandler<TouchEventArgs>(users_list_PreviewTouchDown);
            this.users_list.SelectionChanged += new SelectionChangedEventHandler(users_list_SelectionChanged);
            this.users_list.TouchDown += new EventHandler<TouchEventArgs>(users_list_TouchDown);
            this.users_list.PreviewTouchMove += new EventHandler<TouchEventArgs>(users_list_PreviewTouchMove);
            this.users_list.PreviewTouchUp += new EventHandler<TouchEventArgs>(users_list_PreviewTouchUp);
        }

        void users_list_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            TouchPointCollection points = e.GetIntermediateTouchPoints(sender as IInputElement);
            if (points.Count < 2) return;
            double dx = points[points.Count - 1].Position.X - points[0].Position.X;
            if (dx > 0)
            {
                double dy = points[0].Position.Y - points[points.Count - 1].Position.Y;
                if (Math.Abs(dy) / dx < configurations.drag_dy_dx_factor)
                {
                    FrameworkElement findSource = e.OriginalSource as FrameworkElement;
                    ListBoxItem element = null;
                    while (element == null && findSource != null)
                        if ((element = findSource as ListBoxItem) == null)
                            findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;

                    if (element == null)
                        return;

                    item_generic i = (item_generic)element.DataContext;

                    string username_id = "user;" + ((int)i.Tag).ToString() + ";" + (string)i.username.Content;
                    start_drag(element, username_id, e.TouchDevice, i.avatar.Source.Clone());
                    e.Handled = true;
                }
            }
        }

        void users_list_TouchDown(object sender, TouchEventArgs e)
        {
            e.TouchDevice.Capture(sender as IInputElement);
        }

        void users_list_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            UIElement element = sender as UIElement;
            element.ReleaseTouchCapture(e.TouchDevice);
            e.Handled = false;
        }

        void users_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            item_generic item = (item_generic)e.AddedItems[0];
            window_manager.open_collection_window((string)item.username.Content, (int)item.Tag,
                configurations.RANDOM(0, 20), item.PointToScreen(new Point(0, 0)).Y);
            ///////window_manager.open_collections_balloon(item.PointToScreen(new Point(0, 0)).Y, (string)item.username.Content);
            users_list.SelectedIndex = -1;
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
                    this.users_list.Items.Clear();
                    List<user_item> users = (List<user_item>)e.Result;
                    foreach (user_item u in users)
                    {
                        item_generic i = new item_generic();
                        i.username.Content = u.user.name;
                        //i.user_desc.Content = u.email;
                        i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.content.Visibility = System.Windows.Visibility.Collapsed;
                        i.avatar.Source = u.img;
                        i.Tag = u.user.id;
                        this.users_list.Items.Add(i);
                    }
                    this.users_list.Items.Refresh();
                    this.users_list.UpdateLayout();
                }));
        }

        public void get_all_users(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from u in db.Users
                    orderby u.name
                    select u;
            if (r == null)
            {
                e.Result = (object)(new List<user_item>());
                return;
            }
            List<user_item> users = new List<user_item>();
            foreach (User u in r)
            {
                user_item i = new user_item();
                ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + u.avatar));
                src.Freeze();
                i.img = src;
                i.user = u;
                users.Add(i);
            }
            e.Result = (object)users;
        }

        public bool start_drag(ListBoxItem item, string username_id, TouchDevice touch_device, ImageSource i)
        {
            Image i2 = new Image();
            i2.Source = i; i2.Stretch = Stretch.Uniform;
            ContentControl cursorVisual = new ContentControl()
            {
                Content = i2,
                Style = FindResource("CursorStyle") as Style
            };

            //SurfaceDragDrop.AddTargetChangedHandler(cursorVisual, OnTargetChanged);

            List<InputDevice> devices = new List<InputDevice>();
            devices.Add(touch_device);
            foreach (TouchDevice touch in item.TouchesCapturedWithin)
            {
                if (touch != touch_device)
                {
                    devices.Add(touch);
                }
            }

            SurfaceDragCursor startDragOkay =
                SurfaceDragDrop.BeginDragDrop(
                  this.users_list,            // The SurfaceListBox object that the cursor is dragged out from.
                  item,                       // The SurfaceListBoxItem object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  username_id,                // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }
    }

    public class user_item
    {
        public ImageSource img;
        public User user;
    }
}
