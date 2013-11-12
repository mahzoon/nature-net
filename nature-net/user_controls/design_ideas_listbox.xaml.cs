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

            this.design_ideas.SelectionChanged += new SelectionChangedEventHandler(design_ideas_SelectionChanged);
            //this.design_ideas.PreviewTouchDown += new EventHandler<TouchEventArgs>(design_ideas_PreviewTouchDown);
            this.design_ideas.PreviewTouchMove += new EventHandler<TouchEventArgs>(design_ideas_PreviewTouchMove);
            this.design_ideas.PreviewTouchUp += new EventHandler<TouchEventArgs>(design_ideas_PreviewTouchUp);
            //SurfaceDragDrop.AddPreviewDragLeaveHandler(this.design_ideas, new EventHandler<SurfaceDragDropEventArgs>(design_ideas_DragLeave));
            //SurfaceDragDrop.AddDragLeaveHandler(this.design_ideas, new EventHandler<SurfaceDragDropEventArgs>(design_ideas_DragLeave));
            this.design_ideas.TouchDown += new EventHandler<TouchEventArgs>(design_ideas_PreviewTouchDown);
        }

        void design_ideas_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            TouchPointCollection points = e.GetIntermediateTouchPoints(sender as IInputElement);
            if (points.Count < 2) return;
            double dx = points[0].Position.X - points[points.Count - 1].Position.X;
            if (dx>0)
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

                    string contribution = "design idea;" + ((int)i.Tag).ToString() + ";nothing";
                    start_drag(element, contribution, e.TouchDevice, i.avatar.Source.Clone());
                    e.Handled = true;
                }
            }
        }

        void design_ideas_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.TouchDevice.Capture(sender as IInputElement);
        }

        void design_ideas_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            UIElement element = sender as UIElement;
            element.ReleaseTouchCapture(e.TouchDevice);
            e.Handled = false;
        }

        void design_ideas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            item_generic item = (item_generic)e.AddedItems[0];
            window_manager.open_design_idea_window((int)item.Tag,
                configurations.RANDOM((int)(window_manager.main_canvas.ActualWidth - item.ActualWidth) - 20,
                (int)(window_manager.main_canvas.ActualWidth - item.ActualWidth)),
                item.PointToScreen(new Point(0, 0)).Y);
            design_ideas.SelectedIndex = -1;
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
                    this.design_ideas.Items.Clear();
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
                        i.Tag = idea.design_idea.id;
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

        public bool start_drag(ListBoxItem item, string contribution_id, TouchDevice touch_device, ImageSource i)
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
                  this.design_ideas,          // The SurfaceListBox object that the cursor is dragged out from.
                  item,                       // The SurfaceListBoxItem object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  contribution_id,            // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }
    }

    public class design_idea_item
    {
        public ImageSource img;
        public Design_Idea design_idea;
    }
}
