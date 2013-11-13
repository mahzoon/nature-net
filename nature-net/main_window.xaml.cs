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

namespace nature_net
{
    /// <summary>
    /// Interaction logic for main_window.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            iniparser parser = new iniparser();
            configurations.SetSettingsFromConfig(parser);
            configurations.LoadIconImages();

            //Static Configuration Values:
            this.users_listbox.signup.avatar.Source = configurations.img_signup_icon;
            
            var b = new ImageBrush();
            b.ImageSource = configurations.img_background_pic;
            this.workspace.Background = b;

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            this.workspace.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(workspace_ManipulationStarting);
            this.workspace.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(workspace_ManipulationDelta);
            ///this.workspace.ManipulationBoundaryFeedback += new EventHandler<ManipulationBoundaryFeedbackEventArgs>(workspace_ManipulationBoundaryFeedback);

            this.workspace.AllowDrop = true;
            SurfaceDragDrop.AddDropHandler(this.workspace, new EventHandler<SurfaceDragDropEventArgs>(item_droped_on_workspace));

            application_panel.PreviewTouchDown += new EventHandler<TouchEventArgs>(application_panel_PreviewTouchDown);
        }

        void application_panel_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            Microsoft.Surface.Presentation.Input.InteractiveSurfaceDevice isd = Microsoft.Surface.Presentation.Input.InteractiveSurface.PrimarySurfaceDevice;
            bool finger_supported = isd.IsFingerRecognitionSupported;
            bool finger = Microsoft.Surface.Presentation.Input.TouchExtensions.GetIsFingerRecognized(e.TouchDevice);
            //bool blob = Microsoft.Surface.Presentation.Input.TouchExtensions.(e.TouchDevice);
            bool tag = Microsoft.Surface.Presentation.Input.TouchExtensions.GetIsTagRecognized(e.TouchDevice);

            //if (!finger && finger_supported)
            //    e.Handled = true;
        }

        void item_droped_on_workspace(object sender, SurfaceDragDropEventArgs e)
        {
            string[] data = ((string)e.Cursor.Data).Split(new Char[] { ';' });
            if (data == null) return;
            if (data.Count() < 3) return;
            string context = data[0];
            if (context == "user")
            {
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
            if (context == "image")
            {
                int contribution_id = Convert.ToInt32(data[1]);
                window_manager.open_image_window(contribution_id, e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
        }

        void workspace_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.workspace;
            e.Mode = ManipulationModes.All;
        }

        void workspace_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            if (element == null) return;
            Matrix matrix = ((MatrixTransform)element.RenderTransform).Matrix;
            ManipulationDelta deltaManipulation = e.DeltaManipulation;
            Point center = new Point(element.ActualWidth / 2, element.ActualHeight / 2);
            center = matrix.Transform(center);
            //matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y); 
            matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
            matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
            element.RenderTransform = new MatrixTransform(matrix);
        }

        void workspace_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            throw new NotImplementedException();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            window_manager.refresh_downloaded_contributions();
            if (configurations.use_existing_thumbnails)
                window_manager.refresh_thumbnails();
            this.users_listbox.list_all_users();
            this.design_ideas_listbox.list_all_design_ideas();
            window_manager.main_canvas = this.workspace;
        }
    }
}
