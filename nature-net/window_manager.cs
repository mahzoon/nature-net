﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using nature_net.user_controls;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace nature_net
{
    public class window_manager
    {
        public static Canvas main_canvas;
        public static List<BalloonDecorator> balloons = new List<BalloonDecorator>();
        public static List<int> downloaded_contributions = new List<int>();
        public static Dictionary<int, ImageSource> thumbnails = new Dictionary<int, ImageSource>();
        public static Dictionary<int, ImageSource> contributions = new Dictionary<int, ImageSource>();
        public static List<window_frame> collection_frames = new List<window_frame>();
        public static List<window_frame> signup_frames = new List<window_frame>();
        public static List<window_frame> design_ideas_frames = new List<window_frame>();
        public static List<window_frame> image_display_frames = new List<window_frame>();

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
                //l.SelectionChanged += new SelectionChangedEventHandler(open_collection_window);
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

        public static void close_collections_balloon(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            BalloonDecorator bd = (BalloonDecorator)b.Tag;
            main_canvas.Children.Remove(bd);
            window_manager.balloons.Remove(bd);
        }

        public static void open_collection_window(string username, int userid, double pos_x, double pos_y)
        {
            if (window_manager.collection_frames.Count + 1 > configurations.max_collection_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            collection_listbox c_listbox = new collection_listbox();
            c_listbox.list_all_images(username);
            content.initialize_contents(c_listbox, Type.GetType("nature_net.User"), userid, frame);
            frame.window_content.Content = content;
            content.list_all_comments();

            window_manager.collection_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            frame.set_title(username + "'s contributions");
        }

        public static void open_image_window(int contribution_id, double pos_x, double pos_y)
        {
            if (window_manager.image_display_frames.Count + 1 > configurations.max_image_display_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            image_view img = new image_view();
            img.view_image(contribution_id);
            content.initialize_contents(img, Type.GetType("nature_net.Contribution"), contribution_id, frame);
            frame.window_content.Content = content;
            content.list_all_comments();
            window_manager.image_display_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            img.center_image();
            frame.hide_change_view();
            frame.set_title("Image");
        }

        public static void open_design_idea_window(string[] idea_item, double pos_x, double pos_y)
        {
            if (window_manager.design_ideas_frames.Count + 1 > configurations.max_design_ideas_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            item_generic i = new item_generic();
            i.avatar.Source = new BitmapImage(new Uri(idea_item[2]));
            i.avatar.Source.Freeze();
            i.username.Content = idea_item[3]; i.user_desc.Content = idea_item[4];
            i.desc.Content = idea_item[5];
            AccessText at = new AccessText(); at.TextWrapping = TextWrapping.Wrap;
            at.TextAlignment = TextAlignment.Justify; at.Margin = new Thickness(0);
            at.Text = idea_item[6]; i.content.Content = at;
            i.Background = new SolidColorBrush(Colors.White);
            content.initialize_contents(i, Type.GetType("nature_net.Contribution"), Convert.ToInt32(idea_item[1]), frame);

            frame.window_content.Content = content;
            content.list_all_comments();

            window_manager.design_ideas_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            frame.hide_change_view();
            frame.set_title("Design Idea");
        }

        public static void open_design_idea_window_ext(design_ideas_listbox parent, double pos_x, double pos_y)
        {
            if (window_manager.design_ideas_frames.Count + 1 > configurations.max_design_ideas_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            design_ideas_listbox list = new design_ideas_listbox();
            list.parent = parent;
            list.submit.Visibility = Visibility.Collapsed;
            content.initialize_contents(list, true, frame);
            frame.window_content.Content = content;
            //content.list_all_comments();

            window_manager.design_ideas_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            frame.hide_change_view();
            frame.set_title("Design Ideas");
        }

        public static void open_signup_window(double pos_x, double pos_y)
        {
            if (window_manager.signup_frames.Count + 1 > configurations.max_signup_frame)
                return;

            window_frame frame = new window_frame();
            //window_content content = new window_content();
            //image_view img = new image_view();
            //img.view_image(contribution_id);
            //content.initialize_contents(img, Type.GetType("nature_net.Contribution"), contribution_id);
            //frame.window_content.Content = content;
            //content.list_all_comments();

            window_manager.signup_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            frame.hide_change_view();
            frame.set_title("Sign up");
        }

        private static void open_window(window_frame frame, double pos_x, double pos_y)
        {
            main_canvas.Children.Add(frame);
            frame.IsManipulationEnabled = true;
            frame.UpdateLayout();

            if (pos_y > window_manager.main_canvas.ActualHeight - frame.ActualHeight)
                pos_y = window_manager.main_canvas.ActualHeight - frame.ActualHeight;
            TranslateTransform m = new TranslateTransform(pos_x, pos_y);
            Matrix matrix = m.Value;
            frame.RenderTransform = new MatrixTransform(matrix);
        }

        public static void close_window(window_frame frame)
        {
            collection_frames.Remove(frame);
            image_display_frames.Remove(frame);
            signup_frames.Remove(frame);
            design_ideas_frames.Remove(frame);
            main_canvas.Children.Remove(frame);
        }

        public static void refresh_downloaded_contributions()
        {
            DirectoryInfo d = new DirectoryInfo(configurations.GetAbsoluteContributionPath());
            FileInfo[] files = d.GetFiles();
            window_manager.downloaded_contributions.Clear();
            foreach (FileInfo f in files)
                window_manager.downloaded_contributions.Add(Convert.ToInt32(f.Name.Substring(0, f.Name.Length - 4)));
        }

        public static void refresh_thumbnails()
        {
            DirectoryInfo d = new DirectoryInfo(configurations.GetAbsoluteThumbnailPath());
            FileInfo[] files = d.GetFiles();
            window_manager.thumbnails.Clear();
            foreach (FileInfo f in files)
                window_manager.thumbnails.Add(Convert.ToInt32(f.Name.Substring(0, f.Name.Length - 4)),
                    new BitmapImage(new Uri(configurations.GetAbsoluteThumbnailPath() + f.Name)));
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
