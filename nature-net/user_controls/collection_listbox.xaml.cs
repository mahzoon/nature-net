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
using System.IO;
using Microsoft.Surface;
using Microsoft.Surface.Core;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for collection_listbox.xaml
    /// </summary>
    public partial class collection_listbox : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        private Point drag_direction1 = new Point(0, 1);
        private Point drag_direction2 = new Point(0, -1);

        public window_frame parent;

        public collection_listbox()
        {
            InitializeComponent();
            Image img = new Image();
            img.Source = configurations.img_loading_image_pic;
            img.Tag = null;
            this.contributions.Items.Add(img);

            this.contributions.PreviewTouchUp += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_PreviewTouchUp);
            this.contributions.PreviewTouchMove += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_PreviewTouchMove);
            this.contributions.TouchDown += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_TouchDown);
            //this.contributions.PreviewTouchDown += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_TouchDown);
        }

        void contributions_PreviewTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (parent == null) return;
            FrameworkElement findSource = e.OriginalSource as FrameworkElement;
            ListBoxItem element = null;
            while (element == null && findSource != null)
                if ((element = findSource as ListBoxItem) == null)
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;

            if (element == null)
                return;
            TouchPointCollection points = e.GetIntermediateTouchPoints(sender as IInputElement);
            if (points.Count < 2) return;

            MatrixTransform trans_mat = (MatrixTransform)parent.RenderTransform;
            Matrix mat = trans_mat.Matrix;
            MatrixTransform mat2 = new MatrixTransform(mat.M11, mat.M12, mat.M21, mat.M22, 0, 0);
            
            Point drag_dir1 = mat2.Transform(drag_direction1);
            Point drag_dir2 = mat2.Transform(drag_direction2);
            double size1 = Math.Sqrt(drag_dir1.X * drag_dir1.X + drag_dir1.Y * drag_dir1.Y);
            double size2 = Math.Sqrt(drag_dir2.X * drag_dir2.X + drag_dir2.Y * drag_dir2.Y);
            drag_dir1.X = drag_dir1.X / size1; drag_dir1.Y = drag_dir1.Y / size1;
            drag_dir2.X = drag_dir2.X / size2; drag_dir2.Y = drag_dir2.Y / size2;

            double dy = points[points.Count - 1].Position.Y - points[0].Position.Y;
            double dx = points[0].Position.X - points[points.Count - 1].Position.X;
            double size_n = Math.Sqrt(dx * dx + dy * dy);
            dx = dx / size_n; dy = dy / size_n;
            //dx = Math.Abs(dx); dy = Math.Abs(dy);

            double theta1 = Math.Acos(dx * drag_dir1.X + dy * drag_dir1.Y);
            double theta2 = Math.Acos(dx * drag_dir2.X + dy * drag_dir2.Y);
            //convert to degree
            theta1 = theta1 * 180 / Math.PI;
            theta2 = theta2 * 180 / Math.PI;
            double theta = (theta1 < theta2) ? theta1 : theta2;

            if (theta < configurations.drag_collection_theta)
            {
                Image i = (Image)element.DataContext;
                if (i.Tag == null) return;
                collection_item item = (collection_item)i.Tag;
                start_drag(element, item, e.TouchDevice, i.Source.Clone());
                e.Handled = true;
            }
        }

        void contributions_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            e.TouchDevice.Capture(sender as IInputElement);
        }

        void contributions_PreviewTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            UIElement element = sender as UIElement;
            element.ReleaseTouchCapture(e.TouchDevice);
            e.Handled = false;
        }

        public void list_contributions_in_location(int location)
        {
            worker.DoWork += new DoWorkEventHandler(get_contributions_in_location);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_contributions);
            if (!worker.IsBusy)
                worker.RunWorkerAsync((object)location);
        }

        public void list_all_contributions(string username)
        {
            worker.DoWork += new DoWorkEventHandler(get_all_contributions);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_contributions);
            if (!worker.IsBusy)
                worker.RunWorkerAsync((object)username);
        }

        public void get_all_contributions(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var result1 = from c in db.Collection_Contribution_Mappings
                          where c.Collection.User.name == (string)e.Argument
                          select c.contribution_id;
            if (result1 == null)
            {
                e.Result = (object)(new List<collection_item>());
                return;
            }
            List<int> contribution_ids = result1.ToList<int>();
            var result2 = from m in db.Contributions
                          where contribution_ids.Contains(m.id)
                          select m;
            List<Contribution> medias = result2.ToList<Contribution>();

            // download the image if there is no image
            // create thumbnail if there is no thumbnail
            List<collection_item> items = new List<collection_item>();
            foreach (Contribution c in medias)
            {
                collection_item ci = create_collection_item_from_contribution(c);
                items.Add(ci);
            }
            e.Result = (object)items;
        }

        public void display_all_contributions(object c_obj, RunWorkerCompletedEventArgs e)
        {
            this.contributions.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this.contributions.Items.Clear();
                   List<collection_item> items;
                   if (e.Result == null)
                       items = new List<collection_item>();
                   else
                       items = (List<collection_item>)e.Result;
                   
                   foreach (collection_item i in items)
                   {
                       Image img = new Image();
                       if (window_manager.thumbnails.ContainsKey(i._contribution.id))
                           img.Source = window_manager.thumbnails[i._contribution.id];
                       else
                           img.Source = configurations.img_not_found_image_pic;
                       img.Tag = i;
                       this.contributions.Items.Add(img);
                   }
                   if (items.Count == 0)
                   {
                       Image img = new Image();
                       img.Source = configurations.img_empty_image_pic;
                       img.Tag = null;
                       this.contributions.Items.Add(img);
                   }
                   this.contributions.Items.Refresh();
               }));
        }

        public void get_contributions_in_location(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var result1 = from c in db.Contributions
                          where c.location_id == (int)e.Argument
                          select c;
            if (result1 == null)
            {
                e.Result = (object)(new List<collection_item>());
                return;
            }
            List<Contribution> medias = result1.ToList<Contribution>();
            List<collection_item> items = new List<collection_item>();
            foreach (Contribution c in medias)
            {
                collection_item ci = create_collection_item_from_contribution(c);
                items.Add(ci);
            }
            e.Result = (object)items;
        }

        public collection_item create_collection_item_from_contribution(Contribution c)
        {
            collection_item ci = new collection_item();
            ci._contribution = c;
            int i = c.id;
            string fname = c.media_url;
            string ext = fname.Substring(fname.Length - 4, 4);
            if (ext == ".jpg" || ext == ".bmp" || ext == ".png")
                ci.is_image = true;
            if (ext == ".wmv" || ext == ".mpg" || ext == "mpeg" || ext == ".avi" || ext == ".mp4")
                ci.is_video = true;
            if (ext == ".wav" || ext == ".mp3")
                ci.is_audio = true;

            if (!window_manager.thumbnails.ContainsKey(i))
            {
                if (!window_manager.downloaded_contributions.Contains(i))
                {
                    // download the file
                }

                ImageSource img = null;
                if (ci.is_image)
                    img = configurations.GetThumbnailFromImage(i.ToString() + ext, configurations.thumbnail_pixel_width);
                if (ci.is_video)
                    img = configurations.GetThumbnailFromVideo(i.ToString() + ext, configurations.thumbnail_video_span, configurations.thumbnail_pixel_width);
                if (ci.is_audio)
                    img = configurations.img_sound_image_pic;
                if (img == null)
                    img = configurations.img_not_found_image_pic;

                window_manager.thumbnails.Add(i, img);
                // save the thumbnail
                try
                {
                    if (!ci.is_audio)
                    {
                        BitmapSource bs = img as BitmapSource; configurations.SaveThumbnail(bs, i.ToString());
                    }
                }
                catch (Exception) { }   // not a problem
            }

            return ci;
        }

        public bool start_drag(ListBoxItem item, collection_item contribution_item, TouchDevice touch_device, ImageSource i)
        {
            Image i2 = new Image();
            i2.Source = i;
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
                  this.contributions,         // The SurfaceListBox object that the cursor is dragged out from.
                  item,                       // The SurfaceListBoxItem object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  contribution_item,          // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }
    }

    public class collection_item
    {
        public Contribution _contribution;
        public bool is_audio = false;
        public bool is_image = false;
        public bool is_video = false;

        public override string ToString()
        {
            if (is_audio) return "Audio";
            if (is_image) return "Image";
            if (is_video) return "Video";
            return "Media";
        }
    }
}
