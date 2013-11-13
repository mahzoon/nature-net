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
    /// Interaction logic for image_view.xaml
    /// </summary>
    public partial class image_view : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public image_view()
        {
            InitializeComponent();

            this.image_canvas.Height = 250;

            this.image_canvas.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(image_canvas_ManipulationStarting);
            this.image_canvas.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(image_canvas_ManipulationDelta);
        }

        public void view_image(int contribution_id)
        {
            //ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteContributionPath() + contribution_id.ToString() + ".jpg"));
            //window_manager.contributions.Add(contribution_id, src);
            //the_image.Source = src;
            if (window_manager.contributions.ContainsKey(contribution_id))
            {
                the_image.Source = window_manager.contributions[contribution_id];
                the_image.UpdateLayout();
            }
            else
            {
                worker.DoWork += new DoWorkEventHandler(load_image);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(show_image);
                worker.RunWorkerAsync((object)contribution_id);
            }
        }

        public void center_image()
        {
            if (the_image.Source != null)
            {
                the_image.UpdateLayout();
                image_canvas.UpdateLayout();
                var matrix = ((MatrixTransform)image_canvas.RenderTransform).Matrix;
                matrix.OffsetX = matrix.OffsetX + (image_canvas.ActualWidth / 2) - (the_image.ActualWidth / 2);
                matrix.OffsetY = matrix.OffsetY + (image_canvas.ActualHeight / 2) - (the_image.ActualHeight / 2);
                the_image.RenderTransform = new MatrixTransform(matrix);
            }
        }

        public void load_image(object arg, DoWorkEventArgs e)
        {
            int contribution_id = (int)e.Argument;
            if (!window_manager.downloaded_contributions.Contains(contribution_id))
            {
                // download the file
            }
            try
            {
                ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteContributionPath() + contribution_id.ToString() + ".jpg"));
                src.Freeze();
                window_manager.contributions.Add(contribution_id, src);
                e.Result = (object)contribution_id;
            }
            catch (Exception exc)
            {
                /// write log
            }
        }

        public void show_image(object us, RunWorkerCompletedEventArgs e)
        {
            this.the_image.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    the_image.Source = window_manager.contributions[(int)e.Result];
                    the_image.UpdateLayout();
                    var matrix = ((MatrixTransform)image_canvas.RenderTransform).Matrix;
                    matrix.OffsetX = matrix.OffsetX + (image_canvas.ActualWidth / 2) - (the_image.ActualWidth / 2);
                    matrix.OffsetY = matrix.OffsetY + (image_canvas.ActualHeight / 2) - (the_image.ActualHeight / 2);
                    the_image.RenderTransform = new MatrixTransform(matrix);
                }));
        }

        void image_canvas_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.image_canvas;
            e.Mode = ManipulationModes.All;
        }

        void image_canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            if (element == null) return;
            var deltaManipulation = e.DeltaManipulation;
            var matrix = ((MatrixTransform)element.RenderTransform).Matrix;
            Point center = new Point(element.ActualWidth / 2, element.ActualHeight / 2);
            center = matrix.Transform(center);
            //double scale_x_old = matrix.M11;
            //double scale_y_old = matrix.M22;
            if (the_image.ActualHeight < 300 && the_image.ActualHeight > 100 && the_image.ActualWidth < 300 && the_image.ActualWidth > 100)
                if (deltaManipulation.Scale.X >= 0.5 && deltaManipulation.Scale.X <= 2.5
                    && deltaManipulation.Scale.Y >= 0.5 && deltaManipulation.Scale.Y <= 2.5)
                    matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);
            //if (matrix.M11 < 0.5) matrix.M11 = 0.5; if (matrix.M11 > 2.0) matrix.M11 = 2.0;
            //if (matrix.M22 < 0.5) matrix.M22 = 0.5; if (matrix.M22 > 2.0) matrix.M22 = 2.0;
            
            matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
            //matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);

            //matrix.OffsetX = ((MatrixTransform)image_canvas.RenderTransform).Matrix.OffsetX + (image_canvas.ActualWidth / 2) - (the_image.ActualWidth / 2);
            //matrix.OffsetY = ((MatrixTransform)image_canvas.RenderTransform).Matrix.OffsetY + (image_canvas.ActualHeight / 2) - (the_image.ActualHeight / 2);

            element.RenderTransform = new MatrixTransform(matrix);

            e.Handled = true;
        }
    }
}
