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

        public design_ideas_listbox parent;

        public design_ideas_listbox()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.desc.Height = 50;
            this.submit_button.Height = 80;
            this.desc.Background = new SolidColorBrush(Colors.Green);
            this.desc.Foreground = new SolidColorBrush(Colors.White);

            this.submit_button.Click += new RoutedEventHandler(submit_Click);
        }

        void submit_Click(object sender, RoutedEventArgs e)
        {
            window_manager.open_design_idea_window_ext(this,
                configurations.RANDOM((int)(window_manager.main_canvas.ActualWidth - this.ActualWidth) - 20,
                (int)(window_manager.main_canvas.ActualWidth - this.ActualWidth)),
                configurations.RANDOM((int)(window_manager.main_canvas.ActualHeight - this.submit_button.Height - 20),
                (int)window_manager.main_canvas.ActualHeight));
        }

        public void list_all_design_ideas()
        {
            this.design_ideas_list.parent = this;
            this.design_ideas_list.list_all_design_ideas();
        }
    }
}
