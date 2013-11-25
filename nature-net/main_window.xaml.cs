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
using Microsoft.Surface.Presentation.Controls;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System.IO;
using System.Drawing;

namespace nature_net
{
    /// <summary>
    /// Interaction logic for main_window.xaml
    /// </summary>
    public partial class MainWindow : SurfaceWindow
    {
        private double debug_var = 10;
        private Canvas debug_canvas = new Canvas();
        private int num_updates = 0;

        public MainWindow()
        {
            iniparser parser = new iniparser();
            configurations.SetSettingsFromConfig(parser);
            configurations.LoadIconImages();
            window_manager.load_avatars();
            window_manager.refresh_downloaded_contributions();
            if (configurations.use_existing_thumbnails)
                window_manager.refresh_thumbnails();

            InitializeComponent();
            
            this.load_background();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            if (!configurations.use_scatter_view)
            {
                this.workspace.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(workspace_ManipulationStarting);
                this.workspace.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(workspace_ManipulationDelta);
                ///this.workspace.ManipulationBoundaryFeedback += new EventHandler<ManipulationBoundaryFeedbackEventArgs>(workspace_ManipulationBoundaryFeedback);
            }

            this.workspace.AllowDrop = true;
            SurfaceDragDrop.AddDropHandler(this.workspace, new EventHandler<SurfaceDragDropEventArgs>(item_droped_on_workspace));

            application_panel.PreviewTouchDown += new EventHandler<TouchEventArgs>(application_panel_PreviewTouchDown);
            //application_panel.PreviewMouseDown += new MouseButtonEventHandler(application_panel_PreviewMouseDown);

            System.Windows.Threading.DispatcherTimer update_changes_thread = new System.Windows.Threading.DispatcherTimer();
            update_changes_thread.Tick += new EventHandler(update_changes);
            update_changes_thread.Interval = new TimeSpan(0, 5, 0);
            update_changes_thread.Start();
            update_changes(null, null);
        }

        void update_changes(object sender, EventArgs e)
        {
            file_manager.retrieve_and_process_media_changes_from_googledrive();
            this.left_tab.load_users();
            this.left_tab.load_design_ideas();
            this.right_tab.load_users();
            this.right_tab.load_design_ideas();
        }

        void application_panel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //debug things!
            if (e.RightButton == MouseButtonState.Pressed)
            {
                UIElement[] elements = new UIElement[window_manager.main_canvas.Children.Count];
                window_manager.main_canvas.Children.CopyTo(elements, 0);
                foreach (UIElement element in elements)
                {
                    try
                    {
                        Shape shape = element as Shape;
                        window_manager.main_canvas.Children.Remove(shape);
                    }
                    catch (Exception) { continue; }
                }
            }
        }

        void application_panel_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            Microsoft.Surface.Presentation.Input.InteractiveSurfaceDevice isd = Microsoft.Surface.Presentation.Input.InteractiveSurface.PrimarySurfaceDevice;
            bool finger_supported = isd.IsFingerRecognitionSupported;
            bool finger = Microsoft.Surface.Presentation.Input.TouchExtensions.GetIsFingerRecognized(e.TouchDevice);
            //bool blob = Microsoft.Surface.Presentation.Input.TouchExtensions.(e.TouchDevice);
            bool tag = Microsoft.Surface.Presentation.Input.TouchExtensions.GetIsTagRecognized(e.TouchDevice);

            if (!finger && finger_supported)
            {
                e.Handled = true;
                return;
            }

            //TouchPoint tp = e.GetTouchPoint(sender as IInputElement);
            ////Point p = tp.Position;
            ////p.X = p.X - 245;

            //TextBlock tb = new TextBlock();
            //tb.Text = tp.Position.X.ToString() + ", " + tp.Position.Y.ToString();
            //Canvas.SetLeft(tb, 200); Canvas.SetTop(tb, debug_var);
            //tb.FontSize = 16; tb.FontWeight = FontWeights.Bold;
            //workspace.Children.Add(tb);
            //debug_var = debug_var + 30;
            //if (debug_var > 700) { debug_var = 10; debug_canvas.Children.RemoveRange(0, debug_canvas.Children.Count); }
        }

        void item_droped_on_workspace(object sender, SurfaceDragDropEventArgs e)
        {
            string[] data = (e.Cursor.Data.ToString()).Split(new Char[] { ';' });
            if (data == null) return;
            string context = data[0];
            if (context == "user")
            {
                if (data.Count() < 3) return;
                string username = data[2];
                int user_id = Convert.ToInt32(data[1]);
                window_manager.open_collection_window(username, user_id,
                    e.Cursor.GetPosition(sender as IInputElement).X, e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            if (context == "design idea")
            {
                if (data.Count() < 7) return;
                window_manager.open_design_idea_window(data, e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            if (context == "Image" || context == "Audio" || context == "Video" || context == "Media")
            {
                nature_net.user_controls.collection_item ci = (nature_net.user_controls.collection_item)(e.Cursor.Data);
                window_manager.open_contribution_window(ci, e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y, context);
                e.Handled = true;
            }
            if (context == "comment")
            {
                if (data.Count() < 7) return;
                window_manager.open_design_idea_window(data, e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            if (context == "activity")
            {
                if (data.Count() < 7) return;
                window_manager.open_activity_window(data[3], Convert.ToInt32(data[1]), e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
        }

        void workspace_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.workspace;
            e.Mode = ManipulationModes.All;
            FrameworkElement element = (FrameworkElement)e.Source;
            if (element == null) return;

            //if (e.Manipulators.Count() > 0)
            //{
            //    TouchDevice td = (TouchDevice)(e.Manipulators.First());
            //    TouchPoint tp = td.GetTouchPoint(element);
            //    e.Pivot = new ManipulationPivot(new Point(tp.Position.X, tp.Position.Y), 48);
            //}
            
            this.UpdateZOrder(element, true);
        }

        void workspace_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            if (element == null) return;
            Matrix matrix = ((MatrixTransform)element.RenderTransform).Matrix;
            ManipulationDelta deltaManipulation = e.DeltaManipulation;
            System.Windows.Point center = new System.Windows.Point(element.ActualWidth / 2, element.ActualHeight / 2);
            center = matrix.Transform(center);
            //matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y); 
            matrix.RotateAt(e.DeltaManipulation.Rotation, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);// center.X, center.Y);
            matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
            element.RenderTransform = new MatrixTransform(matrix);
            num_updates++;
            if (num_updates > configurations.max_num_content_update)
            {
                num_updates = 0;
                try { user_controls.window_frame w = (user_controls.window_frame)element; w.UpdateContents(); }
                catch (Exception) { }
            }
        }

        void workspace_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            throw new NotImplementedException();
        }

        void load_background()
        {
            var b = new ImageBrush();
            b.ImageSource = configurations.img_background_pic;
            this.workspace.Background = b;
            //this.application_panel.Background = b;

            int i = 1;
            foreach (System.Windows.Point p in configurations.locations)
            {
                Ellipse e = new Ellipse();
                e.Fill = configurations.location_dot_color;
                e.Width = configurations.location_dot_diameter;
                e.Height = configurations.location_dot_diameter;
                Canvas.SetLeft(e, p.X);
                Canvas.SetTop(e, p.Y);
                e.Tag = i;
                e.PreviewTouchDown += new EventHandler<TouchEventArgs>(reddot_PreviewTouchDown);
                workspace.Children.Add(e);
                TextBlock tb = new TextBlock();
                tb.Text = i.ToString(); tb.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(tb, p.X + configurations.location_dot_diameter / 2 - 4);
                Canvas.SetTop(tb, p.Y + configurations.location_dot_diameter / 2 - 8);
                workspace.Children.Add(tb);
                i++;
            }
        }

        void reddot_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            Ellipse dot = (Ellipse)sender;
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var loc = from l in db.Locations
                      where l.id == (int)dot.Tag
                      select l;
            Location location = loc.Single<Location>();
            window_manager.open_location_collection_window(location.name, location.id, Canvas.GetLeft(dot), Canvas.GetTop(dot));
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (configurations.use_scatter_view)
            {
                this.sv.Width = this.workspace.ActualWidth;
                this.sv.Height = this.workspace.ActualHeight;
            }

            this.left_tab.load_control(true, 0);
            this.right_tab.load_control(false, 2);
            window_manager.main_canvas = this.workspace;
            window_manager.main_scatter_view = this.sv;
            window_manager.left_tab = left_tab;
            window_manager.right_tab = right_tab;

            debug_canvas.Width = window_manager.main_canvas.ActualWidth;
            debug_canvas.Height = window_manager.main_canvas.ActualHeight;
            window_manager.main_canvas.Children.Add(debug_canvas);
        }

        private void UpdateZOrder(UIElement element, bool bringToFront)
        {
            if (element == null)return;
            if (!this.workspace.Children.Contains(element))return;

            // Determine the Z-Index for the target UIElement.
            int elementNewZIndex = -1;
            if (bringToFront)
            {
                foreach (UIElement elem in this.workspace.Children)
                    if (elem.Visibility != Visibility.Collapsed)
                        ++elementNewZIndex;
            }
            else
            {
                elementNewZIndex = 0;
            }

            // Determine if the other UIElements' Z-Index 
            // should be raised or lowered by one. 
            int offset = (elementNewZIndex == 0) ? +1 : -1;
            int elementCurrentZIndex = Canvas.GetZIndex(element);

            // Update the Z-Index of every UIElement in the Canvas.
            foreach (UIElement childElement in this.workspace.Children)
            {
                if (childElement == element)
                    Canvas.SetZIndex(element, elementNewZIndex);
                else
                {
                    int zIndex = Canvas.GetZIndex(childElement);

                    // Only modify the z-index of an element if it is  
                    // in between the target element's old and new z-index.
                    if (bringToFront && elementCurrentZIndex < zIndex ||
                        !bringToFront && zIndex < elementCurrentZIndex)
                    {
                        Canvas.SetZIndex(childElement, zIndex + offset);
                    }
                }
            }
        }
    }
}
            