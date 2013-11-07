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
    /// Interaction logic for design_ideas_listbox.xaml
    /// </summary>
    public partial class design_ideas_listbox : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public design_ideas_listbox()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.desc.Height = 50;
            this.submit.Height = 50;
            this.desc.Background = new SolidColorBrush(Colors.Green);
            this.desc.Foreground = new SolidColorBrush(Colors.White);

            this.design_ideas.SelectionChanged += new SelectionChangedEventHandler(open_design_ideas_window);
        }

        void open_design_ideas_window(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public void list_all_design_ideas()
        {
            worker.DoWork += new DoWorkEventHandler(get_all_design_ideas);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_design_ideas);
            worker.RunWorkerAsync((object)this.design_ideas.Width);
        }

        public void display_all_design_ideas(object di, RunWorkerCompletedEventArgs e)
        {
            this.design_ideas.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    List<design_idea_item> ideas = (List<design_idea_item>)e.Result;
                    //manipulation_starting_handler start_handler = new manipulation_starting_handler(users_list_ManipulationStarting);
                    //manipulation_delta_handler delta_handler = new manipulation_delta_handler(users_list_ManipulationDelta);
                    foreach (design_idea_item idea in ideas)
                    {
                        item_generic i = new item_generic();
                        i.username.Content = idea.design_idea.name;
                        //i.user_desc.Content = idea.design_idea.date;
                        i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                        //i.desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.desc.Content = "Contributed on " + idea.design_idea.date.ToString();
                        //i.content.Visibility = System.Windows.Visibility.Collapsed;
                        AccessText at = new AccessText();
                        at.TextWrapping = TextWrapping.Wrap;
                        at.TextAlignment = TextAlignment.Justify;
                        at.Margin = new Thickness(0);
                        at.Text = idea.design_idea.note;
                        i.content.Content = at;
                        //i.set_touchevent(start_handler, delta_handler);
                        i.Width = this.Width - 5;
                        i.avatar.Source = idea.img;
                        this.design_ideas.Items.Add(i);
                    }
                    this.design_ideas.Items.Refresh();
                }));
        }

        public void get_all_design_ideas(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from d in db.Design_Ideas
                    select d;
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
    }

    public class design_idea_item
    {
        public ImageSource img;
        public Design_Idea design_idea;
    }
}
