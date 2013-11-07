using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace nature_net
{
    public class configurations
    {
        public static string config_file = "config.ini";
        public static string line_break = "\r\n";
        public static string log_file = "log";

        public static string current_directory = System.IO.Directory.GetCurrentDirectory() + "\\";
        public static string image_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\";
        public static string avatar_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\avatars\\";

        public static string image_path = ".\\images\\";
        public static string avatar_path = ".\\images\\avatars\\";

        public static string background_pic = "background.png";
        public static string close_icon = "close.png";
        public static string change_view_list_icon = "change_view_list.png";
        public static string change_view_stack_icon = "change_view_stack.png";
        public static string collection_window_icon = "collection_window_icon.png";
        public static string signup_icon = "signup.png";
        public static string signup_window_icon = "signup_window_icon.png";

        public static ImageSource img_background_pic;
        public static ImageSource img_close_icon;
        public static ImageSource img_change_view_list_icon;
        public static ImageSource img_change_view_stack_icon;
        public static ImageSource img_collection_window_icon;
        public static ImageSource img_signup_window_icon;
        public static ImageSource img_signup_icon;

        public static Random RAND = new Random();
        public static int SEED()
        {
            return RAND.Next();
        }

        public static int RANDOM(int min, int max)
        {
            return RAND.Next(min, max);
        }

        public static byte[] GetBytes(string content)
        {
            byte[] b = new byte[content.Length];
            for (int counter = 0; counter < content.Length; counter++)
                b[counter] = Convert.ToByte(content[counter]);
            return b;
        }

        public static string GetString(byte[] content)
        {
            string s = "";
            for (int counter = 0; counter < content.Length; counter++)
                s = s + Convert.ToChar(content[counter]);

            return s;
        }

        public static string GetAbsolutePath()
        {
            return (Path.GetFullPath(Assembly.GetExecutingAssembly().CodeBase.Substring(10))).Substring(0, Path.GetFullPath(Assembly.GetExecutingAssembly().CodeBase.Substring(10)).Length - 14);
        }

        public static string GetAbsoluteImagePath()
        {
            return configurations.GetAbsolutePath() + configurations.image_path.Substring(2);
        }

        public static string GetAbsoluteAvatarPath()
        {
            return configurations.GetAbsolutePath() + configurations.avatar_path.Substring(2);
        }

        public static void LoadIconImages()
        {
            img_background_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + background_pic));
            img_close_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + close_icon));
            img_collection_window_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + collection_window_icon));
            img_signup_window_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + signup_window_icon));
            img_change_view_list_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + change_view_list_icon));
            img_change_view_stack_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + change_view_stack_icon));
            img_signup_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + signup_icon));
        }

        public static void SetSettingsFromConfig(iniparser parser)
        {
            //this.??? = parser.GetValue("Section","Key",default_value);
        }

        public static string preset_config_file = "";
    }
}
