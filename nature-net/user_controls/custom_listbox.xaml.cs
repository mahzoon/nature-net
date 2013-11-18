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
using System.ComponentModel;
using System.Windows.Threading;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for custom_listbox.xaml
    /// </summary>
    public partial class custom_listbox : UserControl
    {
        public bool list_users = false;
        public bool list_design_ideas = false;
        public bool list_comments = false;
        public bool list_activities = false;

        public UserControl parent;

        private readonly BackgroundWorker worker = new BackgroundWorker();

        public custom_listbox()
        {
            InitializeComponent();
            initialize_list();
        }

        public void initialize_list()
        {
            this._list.SelectionChanged += new SelectionChangedEventHandler(_list_SelectionChanged);
            if (!configurations.use_avatar_drag)
            {
                this._list.TouchDown += new EventHandler<TouchEventArgs>(_list_TouchDown);
                this._list.PreviewTouchMove += new EventHandler<TouchEventArgs>(_list_PreviewTouchMove);
                this._list.PreviewTouchUp += new EventHandler<TouchEventArgs>(_list_PreviewTouchUp);
            }
        }

        // Generic Behaviors

        private void avatar_drag(object sender, TouchEventArgs e)
        {
            FrameworkElement findSource = e.OriginalSource as FrameworkElement;
            ListBoxItem element = null;
            while (element == null && findSource != null)
                if ((element = findSource as ListBoxItem) == null)
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;

            if (element == null)
                return;

            item_generic i = (item_generic)element.DataContext;

            if (list_users)
            {
                string username_id = "user;" + ((int)i.Tag).ToString() + ";" + (string)i.username.Content + ";" + i.avatar.Source.ToString();
                start_drag(element, username_id, e.TouchDevice, i.avatar.Source.Clone());
            }
            if (list_design_ideas)
            {
                string idea = "design idea;" + ((int)i.Tag).ToString() + ";" + i.avatar.Source.ToString() + ";" +
                    (string)i.username.Content + ";" + i.user_desc.Content + ";" + i.desc.Content + ";" + 
                    i.content.Text;
                start_drag(element, idea, e.TouchDevice, i.avatar.Source.Clone());
            }
            if (list_comments)
            {
                string idea = "comment;" + ((int)i.Tag).ToString() + ";" + i.avatar.Source.ToString() + ";" +
                    (string)i.username.Content + ";" + i.user_desc.Content + ";" + i.desc.Content + ";" +
                    i.content.Text;
                start_drag(element, idea, e.TouchDevice, i.avatar.Source.Clone());
            }
            e.Handled = true;
        }

        private void _list_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            TouchPointCollection points = e.GetIntermediateTouchPoints(sender as IInputElement);
            if (points.Count < 2) return;
            double dx = points[points.Count - 1].Position.X - points[0].Position.X;
            if (dx > 0)
            {
                double dy = points[0].Position.Y - points[points.Count - 1].Position.Y;
                if (Math.Abs(dy) / dx < configurations.drag_dy_dx_factor)
                {
                    avatar_drag(sender, e);
                }
            }
        }

        private void _list_TouchDown(object sender, TouchEventArgs e)
        {
            e.TouchDevice.Capture(sender as IInputElement);
        }

        private void _list_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            UIElement element = sender as UIElement;
            element.ReleaseTouchCapture(e.TouchDevice);
            e.Handled = false;
        }

        private void _list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            item_generic item = (item_generic)e.AddedItems[0];
            if (list_design_ideas)
            {
                string[] idea_item = ("design idea;" + item.ToString()).Split(new Char[] { ';' });
                window_manager.open_design_idea_window(idea_item,
                    configurations.RANDOM(20, (int)(window_manager.main_canvas.ActualWidth - item.ActualWidth)),
                    item.PointToScreen(new Point(0, 0)).Y);
                _list.SelectedIndex = -1;
                return;
            }
            if (list_users)
            {
                window_manager.open_collection_window((string)item.username.Content, (int)item.Tag,
                    configurations.RANDOM(20, (int)(window_manager.main_canvas.ActualWidth - item.ActualWidth)),
                    item.PointToScreen(new Point(0, 0)).Y);
                ///////window_manager.open_collections_balloon(item.PointToScreen(new Point(0, 0)).Y, (string)item.username.Content);
                _list.SelectedIndex = -1;
                return;
            }
            if (list_comments)
            {
                string[] idea_item = ("comment;" + item.ToString()).Split(new Char[] { ';' });
                //window_manager.open_design_idea_window(idea_item,
                //    configurations.RANDOM((int)(window_manager.main_canvas.ActualWidth - item.ActualWidth) - 20,
                //    (int)(window_manager.main_canvas.ActualWidth - item.ActualWidth)),
                //    item.PointToScreen(new Point(0, 0)).Y);
                _list.SelectedIndex = -1;
                return;
            }
            _list.SelectedIndex = -1;
        }

        private bool start_drag(ListBoxItem item, string username_id, TouchDevice touch_device, ImageSource i)
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

            FrameworkElement element = item;
            if (configurations.use_avatar_drag)
                element = ((item_generic)(item.DataContext)).avatar;

            SurfaceDragCursor startDragOkay =
                SurfaceDragDrop.BeginDragDrop(
                  this._list,                 // The SurfaceListBox object that the cursor is dragged out from.
                  element,                    // The SurfaceListBoxItem object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  username_id,                // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }

        // Custom Behaviors (users or design ideas)

        // for design ideas
        public void list_all_design_ideas()
        {
            this.list_design_ideas = true;
            worker.DoWork += new DoWorkEventHandler(get_all_design_ideas);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_design_ideas);
            if (!worker.IsBusy)
                worker.RunWorkerAsync(null);
        }
        public void get_all_design_ideas(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from d in db.Design_Ideas
                    orderby d.date descending
                    select d;
            if (r == null)
            {
                e.Result = (object)(new List<design_idea_item>());
                return;
            }
            List<design_idea_item> ideas = new List<design_idea_item>();
            foreach (Design_Idea d in r)
            {
                design_idea_item i = new design_idea_item();
                ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + d.avatar));
                src.Freeze();
                i.img = src;
                i.design_idea = d;
                ideas.Add(i);
            }
            e.Result = (object)ideas;
        }
        public void display_all_design_ideas(object di, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    this._list.Items.Clear();
                    List<design_idea_item> ideas = (List<design_idea_item>)e.Result;
                    foreach (design_idea_item idea in ideas)
                    {
                        item_generic i = new item_generic();
                        i.username.Content = idea.design_idea.name;
                        //i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.user_desc.Content = configurations.GetDate_Formatted(idea.design_idea.date);
                        i.desc.Content = "Contributed:";
                        i.content.Text = idea.design_idea.note;
                        if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                        if (parent != null) i.Width = parent.Width;
                        i.avatar.Source = idea.img;
                        i.Tag = idea.design_idea.id;
                        this._list.Items.Add(i);
                    }
                    this._list.Items.Refresh();
                    this._list.UpdateLayout();
                }));
        }

        // for users
        public void list_all_users()
        {
            this.list_users = true;
            worker.DoWork += new DoWorkEventHandler(get_all_users);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_users);
            if (!worker.IsBusy)
                worker.RunWorkerAsync(null);
        }
        public void get_all_users(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from u in db.Users
                    where u.id != 0
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
        public void display_all_users(object us, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    this._list.Items.Clear();
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
                        if (parent != null) i.Width = parent.Width;
                        i.Tag = u.user.id;
                        if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                        this._list.Items.Add(i);
                    }
                    this._list.Items.Refresh();
                    this._list.UpdateLayout();
                }));
        }

        // for comments
        public void list_all_comments(comment_item item)
        {
            this.list_comments = true;
            worker.DoWork += new DoWorkEventHandler(get_all_comments);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_comments);
            if (!worker.IsBusy)
                worker.RunWorkerAsync(item);
        }
        public void get_all_comments(object arg, DoWorkEventArgs e)
        {
            if (e.Argument == null) return;
            comment_item item = (comment_item)e.Argument;
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from c in db.Feedbacks
                    where (c.Feedback_Type.name == "Comment") && (c.object_type == item._object_type.ToString())
                    && (c.object_id == item._object_id)
                    orderby c.date descending
                    select c;
            if (r != null)
            {
                List<Feedback> comments = r.ToList<Feedback>();
                e.Result = (object)comments;
            }
            else
            {
                e.Result = (object)(new List<Feedback>());
            }
        }
        public void display_all_comments(object c_obj, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this._list.Items.Clear();
                   List<Feedback> comments = (List<Feedback>)e.Result;
                   foreach (Feedback c in comments)
                   {
                       item_generic i = new item_generic();
                       i.username.Content = c.User.name;
                       i.user_desc.Content = configurations.GetDate_Formatted(c.date);
                       //i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                       i.desc.Content = "Commented:";
                       i.content.Text = c.note;
                       if (parent != null) i.Width = parent.Width;
                       i.avatar.Source = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + c.User.avatar));
                       i.Tag = c.id;
                       if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                       this._list.Items.Add(i);
                   }
                   this._list.Items.Refresh();
                   this._list.UpdateLayout();
               }));
        }

        // for activities
        public void list_all_activities()
        {
            this.list_activities = true;
            worker.DoWork += new DoWorkEventHandler(get_all_activities);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_activities);
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }
        public void get_all_activities(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from a in db.Activities
                    where (a.name != "Free Observation") && (a.name != "Design Idea")
                    select a;
            if (r != null)
            {
                List<Activity> activities = r.ToList<Activity>();
                e.Result = (object)activities;
            }
            else
            {
                e.Result = (object)(new List<Activity>());
            }
        }
        public void display_all_activities(object arg, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this._list.Items.Clear();
                   List<Activity> activities = (List<Activity>)e.Result;
                   foreach (Activity a in activities)
                   {
                       item_generic i = new item_generic();
                       i.username.Content = a.name;
                       i.user_desc.Content = configurations.GetDate_Formatted(a.creation_date);
                       //i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                       i.desc.Content = "Description:";
                       i.content.Text = a.description;
                       if (parent != null) i.Width = parent.Width;
                       i.avatar.Visibility = System.Windows.Visibility.Collapsed;
                       //i.avatar.Source = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + c.User.avatar));
                       i.Tag = a.id;
                       if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                       this._list.Items.Add(i);
                   }
                   this._list.Items.Refresh();
                   this._list.UpdateLayout();
               }));
        }
    }

    public class design_idea_item
    {
        public ImageSource img;
        public Design_Idea design_idea;
    }

    public class user_item
    {
        public ImageSource img;
        public User user;
    }

    public class comment_item
    {
        public int _object_id;
        public Type _object_type;
    }
}
