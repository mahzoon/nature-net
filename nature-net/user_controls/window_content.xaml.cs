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
using System.ComponentModel;
using System.Windows.Threading;
using Microsoft.Surface.Presentation;
using JHVirtualKeyboard;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for window_content.xaml
    /// </summary>
    public partial class window_content : UserControl, IVirtualKeyboardInjectable
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private int _object_id;
        private Type _object_type;
        bool is_design_idea = false;

        private int comment_user_id;
        bool expand_state = false;

        VirtualKeyboard keyboard;
        ContentControl keyboard_frame;
        UserControl parent;

        public window_content()
        {
            InitializeComponent();

            this.submit_comment.Content = "Submit";
            this.submit_comment.TouchDown += new EventHandler<TouchEventArgs>(submit_comment_TouchDown);
            this.leave_comment_panel.Visibility = System.Windows.Visibility.Collapsed;

            this.leave_comment_area.AllowDrop = true;
            SurfaceDragDrop.AddPreviewDropHandler(this.leave_comment_area, new EventHandler<SurfaceDragDropEventArgs>(item_droped_on_leave_comment_area));
            this.expander.TouchDown += new EventHandler<TouchEventArgs>(expander_TouchDown);
            this.comments_listbox.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area.Visibility = System.Windows.Visibility.Collapsed;

            this.comment_textbox.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(comment_textbox_GotKeyboardFocus);
            this.comment_textbox.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(comment_textbox_LostKeyboardFocus);
            this.LayoutUpdated += new EventHandler(window_content_LayoutUpdated);
            this.Unloaded += new RoutedEventHandler(window_content_Unloaded);
        }

        void window_content_Unloaded(object sender, RoutedEventArgs e)
        {
            if (keyboard_frame != null)
                window_manager.main_canvas.Children.Remove(keyboard_frame);
        }

        void window_content_LayoutUpdated(object sender, EventArgs e)
        {
            if (keyboard_frame != null)
            {
                if (keyboard != null)
                {
                    if (keyboard_frame.Visibility == System.Windows.Visibility.Visible)
                    {
                        keyboard.MoveAlongWith(parent);
                    }
                }
            }
        }

        void comment_textbox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        void comment_textbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (keyboard_frame == null)
                keyboard_frame = new ContentControl();
            VirtualKeyboard.ShowOrAttachTo(this, ref keyboard);
            keyboard_frame.Visibility = System.Windows.Visibility.Visible;
            if (keyboard != null)
            {
                if (this.keyboard_frame.Content == null)
                {
                    this.keyboard_frame.Content = keyboard;
                    //this.keyboard.Background = new SolidColorBrush(Colors.White);
                    this.keyboard_frame.Background = new SolidColorBrush(Colors.White);
                    window_manager.main_canvas.Children.Add(keyboard_frame);
                }
                keyboard.MoveAlongWith(parent);
            }
        }

        void expander_TouchDown(object sender, TouchEventArgs e)
        {
            expand_state = !expand_state;
            if (expand_state)
            {
                this.comments_listbox.Visibility = System.Windows.Visibility.Visible;
                this.leave_comment_area.Visibility = System.Windows.Visibility.Visible;
                this.expander.Content = "^";
            }
            else
            {
                this.comments_listbox.Visibility = System.Windows.Visibility.Collapsed;
                this.leave_comment_area.Visibility = System.Windows.Visibility.Collapsed;
                this.expander.Content = "v";
            }
        }

        void submit_comment_TouchDown(object sender, TouchEventArgs e)
        {
            if (is_design_idea)
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                Contribution idea = new Contribution();
                idea.date = DateTime.Now;
                idea.location_id = 0;
                idea.note = this.comment_textbox.Text;
                db.Contributions.InsertOnSubmit(idea);
                db.SubmitChanges();
                int collection_id = get_or_create_collection(db, this.comment_user_id, 1);
                Collection_Contribution_Mapping map = new Collection_Contribution_Mapping();
                map.collection_id = collection_id;
                map.contribution_id = idea.id;
                map.date = DateTime.Now;
                db.Collection_Contribution_Mappings.InsertOnSubmit(map);
                db.SubmitChanges();
                
                ((design_ideas_listbox)the_item.Content).list_all_design_ideas();
                if (((design_ideas_listbox)the_item.Content).parent != null)
                    ((design_ideas_listbox)the_item.Content).parent.list_all_design_ideas();
            }
            else
            {
                Feedback comment = new Feedback();
                comment.date = DateTime.Now;
                comment.note = this.comment_textbox.Text;
                comment.object_id = this._object_id;
                comment.object_type = this._object_type.ToString();
                comment.parent_id = 0;
                comment.technical_info = "";
                comment.type_id = 1;
                comment.user_id = this.comment_user_id;
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                db.Feedbacks.InsertOnSubmit(comment);
                db.SubmitChanges();
                this.list_all_comments();
            }
            this.comment_textbox.SelectAll();
        }

        void item_droped_on_leave_comment_area(object sender, SurfaceDragDropEventArgs e)
        {
            string[] data = ((string)e.Cursor.Data).Split(new Char[] { ';' });
            if (data == null) return;
            if (data.Count() < 4) return;
            string context = data[0];
            if (context == "user")
            {
                string username = data[2];
                int user_id = Convert.ToInt32(data[1]);
                this.add_comment_img.Visibility = System.Windows.Visibility.Collapsed;
                this.leave_comment_panel.Visibility = System.Windows.Visibility.Visible;
                this.comment_user_id = user_id;
                this.avatar.Source = new BitmapImage(new Uri(data[3]));
            }
            e.Handled = true;
        }

        public void initialize_contents(UserControl uc, Type obj_type, int obj_id, UserControl parent_frame)
        {
            this.the_item.Content = uc;
            this._object_id = obj_id;
            this._object_type = obj_type;
            this.list_all_comments();
            this.add_comment_img.Source = configurations.img_drop_avatar_pic;
            this.parent = parent_frame;
        }

        public void initialize_contents(UserControl uc, bool is_design, UserControl parent_frame)
        {
            this.the_item.Content = uc;
            this.is_design_idea = is_design;
            this.comments_listbox.Visibility = System.Windows.Visibility.Collapsed;
            this.expander.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area.Visibility = System.Windows.Visibility.Visible;
            this.add_comment_img.Source = configurations.img_drop_avatar_pic;
            ((design_ideas_listbox)the_item.Content).list_all_design_ideas();
            ((design_ideas_listbox)the_item.Content).desc.Visibility = System.Windows.Visibility.Collapsed;
            ((design_ideas_listbox)the_item.Content).Height = configurations.design_idea_ext_window_width;
            ((design_ideas_listbox)the_item.Content).Background = new SolidColorBrush(Colors.White);
            this.parent = parent_frame;
        }

        public void list_all_comments()
        {
            worker.DoWork += new DoWorkEventHandler(get_all_comments);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_comments);
            if (!worker.IsBusy)
                worker.RunWorkerAsync(null);
        }

        public void get_all_comments(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from c in db.Feedbacks
                    where (c.Feedback_Type.name == "Comment") && (c.object_type == this._object_type.ToString())
                    && (c.object_id == this._object_id)
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
            this.comments_listbox.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this.comments_listbox.Items.Clear();
                   List<Feedback> comments = (List<Feedback>)e.Result;
                   //manipulation_starting_handler start_handler = new manipulation_starting_handler(users_list_ManipulationStarting);
                   //manipulation_delta_handler delta_handler = new manipulation_delta_handler(users_list_ManipulationDelta);
                   foreach (Feedback c in comments)
                   {
                       item_generic i = new item_generic();
                       i.username.Content = c.User.name;
                       //i.user_desc.Content = idea.design_idea.date;
                       i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                       //i.desc.Visibility = System.Windows.Visibility.Collapsed;
                       i.desc.Content = "Commented on " + c.date.ToString();
                       //i.content.Visibility = System.Windows.Visibility.Collapsed;
                       AccessText at = new AccessText();
                       at.TextWrapping = TextWrapping.Wrap;
                       at.TextAlignment = TextAlignment.Justify;
                       at.Margin = new Thickness(0);
                       at.Text = c.note;
                       i.content.Content = at;
                       //i.set_touchevent(start_handler, delta_handler);
                       i.Width = this.Width - 5;
                       i.avatar.Source = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + c.User.avatar));
                       this.comments_listbox.Items.Add(i);
                   }
                   this.comments_listbox.Items.Refresh();
               }));
        }

        private int get_or_create_collection(naturenet_dataclassDataContext db, int user_id, int activity_id)
        {
            var r = from c in db.Collections
                    where ((c.user_id == user_id) && c.activity_id == activity_id)
                    orderby c.date descending
                    select c;
            if (r.Count() != 0)
            {
                foreach (Collection col in r)
                {
                    if (configurations.GetDate_Formatted(col.date) == configurations.GetCurrentDate_Formatted())
                        return col.id;
                }
            }

            // create new collection
            Collection cl = new Collection();
            cl.activity_id = activity_id;
            cl.date = DateTime.Now;
            cl.name = configurations.GetCurrentDate_Formatted();
            cl.user_id = user_id;
            db.Collections.InsertOnSubmit(cl);
            db.SubmitChanges();
            return cl.id;
        }

        public Control ControlToInjectInto
        {
            get { return this.comment_textbox; }
        }
    }
}
