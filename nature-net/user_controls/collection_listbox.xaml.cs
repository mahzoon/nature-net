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

        public collection_listbox()
        {
            InitializeComponent();
            Image img = new Image();
            img.Source = configurations.img_loading_image_pic;
            img.Tag = -1;
            this.contributions.Items.Add(img);

            this.contributions.PreviewTouchUp += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_PreviewTouchUp);
            this.contributions.PreviewTouchMove += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_PreviewTouchMove);
            this.contributions.TouchDown += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_TouchDown);
        }

        void contributions_PreviewTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            TouchPointCollection points = e.GetIntermediateTouchPoints(sender as IInputElement);
            if (points.Count < 2) return;
            double dy = points[points.Count - 1].Position.Y - points[0].Position.Y;
            double dx = points[0].Position.X - points[points.Count - 1].Position.X;
            if (Math.Abs(dx) / Math.Abs(dy) < configurations.drag_dx_dy_factor)
            {
                FrameworkElement findSource = e.OriginalSource as FrameworkElement;
                ListBoxItem element = null;
                while (element == null && findSource != null)
                    if ((element = findSource as ListBoxItem) == null)
                        findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;

                if (element == null)
                    return;

                Image i = (Image)element.DataContext;
                int contribution_id = (int)i.Tag;
                string contribution = "image;" + contribution_id.ToString() + ";nothing";
                if (contribution_id == -1) return;
                start_drag(element, contribution, e.TouchDevice, i.Source.Clone());
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

        public void list_all_images(string username)
        {
            worker.DoWork += new DoWorkEventHandler(get_all_images);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_images);
            worker.RunWorkerAsync((object)username);
        }

        public void get_all_images(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var result1 = from c in db.Collection_Contribution_Mappings
                          where c.Collection.User.name == (string)e.Argument
                          select c.contribution_id;
            if (result1 == null)
            {
                e.Result = (object)(new List<int>());
                return;
            }
            List<int> contribution_ids = result1.ToList<int>();
            //var result2 = from m in db.Contributions
            //              where contribution_ids.Contains(m.id)
            //              select m;
            //List<Contribution> medias = result2.ToList<Contribution>();

            // download the image if there is no one
            // create thumbnail if there is no one
            foreach (int i in contribution_ids)
            {
                if (!window_manager.thumbnails.ContainsKey(i))
                {
                    if (!window_manager.downloaded_contributions.Contains(i))
                    {
                        // download the file
                    }
                    BitmapImage bi = new BitmapImage();
                    try
                    {
                        // create the thumbnail
                        bi.BeginInit();
                        bi.DecodePixelWidth = configurations.thumbnail_pixel_width;
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.UriSource = new Uri(configurations.GetAbsoluteContributionPath() + i.ToString() + ".jpg");
                        bi.EndInit();
                        bi.Freeze();
                    }
                    catch (Exception exc)
                    {
                        // could not create thumbnail -- reason: filenotfound or currupt download or ...
                        // write log
                        continue;
                    }
                    window_manager.thumbnails.Add(i, bi);
                    // save the thumbnail
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bi));
                    using (var fs = new FileStream(configurations.GetAbsoluteThumbnailPath() + i.ToString() + ".jpg", FileMode.Create))
                    {
                        encoder.Save(fs);
                    }
                }
            }
            e.Result = (object)contribution_ids;
        }

        public void display_all_images(object c_obj, RunWorkerCompletedEventArgs e)
        {
            this.contributions.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this.contributions.Items.Clear();
                   List<int> contribution_ids = (List<int>)e.Result;
                   foreach (int i in contribution_ids)
                   {
                       if (window_manager.thumbnails.ContainsKey(i))
                       {
                           Image img = new Image();
                           img.Source = window_manager.thumbnails[i];
                           img.Tag = i;
                           this.contributions.Items.Add(img);
                       }
                       else
                       {
                           Image img = new Image();
                           img.Source = configurations.img_not_found_image_pic;
                           img.Tag = i;
                           this.contributions.Items.Add(img);
                       }
                   }
                   if (contribution_ids.Count == 0)
                   {
                       Image img = new Image();
                       img.Source = configurations.img_empty_image_pic;
                       img.Tag = -1;
                       this.contributions.Items.Add(img);
                   }
                   this.contributions.Items.Refresh();
               }));
        }

        public bool start_drag(ListBoxItem item, string contribution_id, TouchDevice touch_device, ImageSource i)
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
                  contribution_id,            // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }
    }
}
