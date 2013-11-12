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

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for window_content.xaml
    /// </summary>
    public partial class window_content : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private int _object_id;
        private Type _object_type;

        public window_content()
        {
            InitializeComponent();
        }

        public void initialize_contents(UserControl uc, Type obj_type, int obj_id)
        {
            this.the_item.Content = uc;
            this._object_id = obj_id;
            this._object_type = obj_type;
            this.list_all_comments();
            this.add_comment_img.Source = configurations.img_drop_avatar_pic;
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
                    select c;
            //if (r != null)
            //{
                //List<Feedback> comments = r.ToList<Feedback>();
                //e.Result = (object)comments;
            //}
            //else
            //{
                e.Result = (object)(new List<Feedback>());
            //}
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
    }
}
