using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using nature_net.user_controls;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;

namespace nature_net
{
    public class window_manager
    {
        public static Canvas main_canvas;
        public static List<BalloonDecorator> balloons = new List<BalloonDecorator>();
        public static Dictionary<int, ImageSource> thumbnails = new Dictionary<int, ImageSource>();

        public static void open_collections_balloon(double y, string username)
        {
            BalloonDecorator b = new BalloonDecorator();
            b.PointerLength = 10;
            b.PointerVerticalOffset = 20;
            b.CornerRadius = 5;
            b.CornerPosition = "Left";
            //b.Height = 100;
            b.Width = 200;

            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0.5, 0);
            brush.EndPoint = new Point(0.5, 1);
            GradientStop gs1 = new GradientStop(Colors.LightGoldenrodYellow, 0);
            GradientStop gs2 = new GradientStop(Colors.Orange, 1);
            brush.GradientStops.Add(gs1); brush.GradientStops.Add(gs2);

            b.Background = brush;

            Button b2 = new Button();
            b2.Click += new RoutedEventHandler(close_collections_balloon);
            b2.Name = "close";
            b2.Content = "close";
            b2.Tag = b;
            b2.Margin = new System.Windows.Thickness(10);
            ListBox l = new ListBox();
            l.Margin = new System.Windows.Thickness(10,5,10,10);
            l.Tag = b;
            //StackPanel s = new StackPanel();
            //s.Orientation = Orientation.Horizontal;
            //s.IsItemsHost = true;
            //l.ItemsPanel.Template = 

            StackPanel s2 = new StackPanel();
            s2.Children.Add(b2);
            
            //filling the list
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var cs = from c in db.Collections
                     where (c.User.name.Equals(username) && c.Activity.name.Equals("Free Observation"))
                     select c;
            List<int> ids = new List<int>();
            foreach (Collection c in cs)
            {
                l.Items.Add(c);
                ids.Add(c.id);
            }
            if (cs.Count() > 0)
            {
                l.Items.Add("All Collections");
                s2.Children.Add(l);
                l.SelectionChanged += new SelectionChangedEventHandler(open_collection_window);
            }
            else
            {
                Label l2 = new Label();
                l2.Content = "No Collections.";
                s2.Children.Add(l2);
            }

            b.Child = s2;
            b.Tag = ids;
            main_canvas.Children.Add(b);
            window_manager.balloons.Add(b);

            TranslateTransform trans = new TranslateTransform(0, y);
            b.RenderTransform = trans;
        }

        static void open_collection_window(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            ListBox l = (ListBox)sender;
            BalloonDecorator bd = (BalloonDecorator)l.Tag;
            List<int> ids = (List<int>)bd.Tag;
            main_canvas.Children.Remove(bd);
            window_manager.balloons.Remove(bd);

            //naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            //List<Contribution> medias;
            //if (e.AddedItems[0].GetType().Name == "string")
            //{
            //    var result = from m in db.Contributions
            //            where ids.Contains(m.collection_id)
            //            select m;
            //    medias = result.ToList<Media>();
            //}
            //else
            //{
            //    Collection c = (Collection)e.AddedItems[0];
            //    var result = from m in db.Medias
            //            where c.id == m.collection_id
            //            select m;
            //    medias = result.ToList<Media>();
            //}

            //foreach (Media m in medias)
            //{
            //    if (!thumbnails.ContainsKey(m.id))
            //    {
            //        //load the thumbnail of media with id = m.id
            //    }
            //}
        }

        public static void close_collections_balloon(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            BalloonDecorator bd = (BalloonDecorator)b.Tag;
            main_canvas.Children.Remove(bd);
            window_manager.balloons.Remove(bd);
        }
    }

    public partial class Collection : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public override string ToString()
        {
            return this.name;
        }
    }
}
