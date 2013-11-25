using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace nature_net
{
    public class configurations
    {
        public static string config_file = "config.ini";
        public static string line_break = "\r\n";
        public static string log_file = "log";

        public static bool high_contrast = false;

        public static int max_num_content_update = 12;

        public static int max_signup_frame = 5;
        public static int max_collection_frame = 5;
        public static int max_image_display_frame = 5;
        public static int max_design_ideas_frame = 5;
        public static int max_activity_frame = 5;
        public static int max_activity_frame_title_chars = 10;
        //public static int thumbnail_pixel_width = 100;
        public static int thumbnail_pixel_height = 100;
        public static TimeSpan thumbnail_video_span = new TimeSpan(0, 0, 2);
        public static bool use_existing_thumbnails = true;
        public static double drag_dy_dx_factor = 2.1;
        //public static double drag_dx_dy_factor = 1.0;

        public static bool use_scatter_view = false;

        public static double drag_collection_theta = 5;
        public static double scroll_scale_factor = 5;
        public static int min_touch_points = 2;
        public static int max_consecutive_drag_points = 5;
        public static double tap_error = 2;
        
        public static int design_idea_ext_window_width = 250;
        public static bool use_avatar_drag = false;

        public static List<Point> locations = new List<Point>();
        public static int location_dot_diameter = 40;
        public static Brush location_dot_color = Brushes.Crimson;

        public static string current_directory = System.IO.Directory.GetCurrentDirectory() + "\\";
        public static string image_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\";
        public static string avatar_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\avatars\\";
        public static string thumbnails_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\thumbnails\\";
        public static string contributions_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\contributions\\";
        public static string googledrive_directory_id = "0B9mU-w_CpbztUUxtaXVIeE9SbWM";
        public static string googledrive_client_id = "333780750675-ag76kpq3supbbqi3v92vn3ejil8ght23.apps.googleusercontent.com";
        public static string googledrive_client_secret = "bCYIAfrAC0i-qIfl0cLRnhwn";
        public static string googledrive_storage = "gdrive_uploader";
        public static string googledrive_key = "z},drdzf11x9;87";
        public static string googledrive_refresh_token = "1/jpJHu8br2TnnM5hwCqgFe-yagf6zixlDZrlUvdXZ9s8";
        public static string googledrive_lastchange = "";

        // 10KB = 10 * 1024
        public static int download_buffer_size = 10240;

        public static string image_path = ".\\images\\";
        public static string avatar_path = ".\\images\\avatars\\";
        public static string thumbnails_path = ".\\images\\thumbnails\\";
        public static string contributions_path = ".\\images\\contributions\\";

        public static string background_pic = "background.png";
        public static string drop_avatar_pic = "drop_avatar.png";
        public static string loading_image_pic = "loading_image.png";
        public static string empty_image_pic = "empty_image.png";
        public static string not_found_image_pic = "not_found_image.png";
        public static string sound_image_pic = "sound_image.png";
        public static string video_image_pic = "film.png";
        public static string keyboard_pic = "keyboard.png";
        public static string close_icon = "close.png";
        public static string change_view_list_icon = "change_view_list.png";
        public static string change_view_stack_icon = "change_view_stack.png";
        public static string collection_window_icon = "collection_window_icon.png";
        public static string signup_icon = "signup.png";
        public static string signup_window_icon = "signup_window_icon.png";

        public static string keyboard_click_wav = "click.wav";

        public static ImageSource img_background_pic;
        public static ImageSource img_drop_avatar_pic;
        public static ImageSource img_loading_image_pic;
        public static ImageSource img_empty_image_pic;
        public static ImageSource img_not_found_image_pic;
        public static ImageSource img_sound_image_pic;
        public static ImageSource img_video_image_pic;
        public static ImageSource img_keyboard_pic;
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

        public static string GetAbsoluteThumbnailPath()
        {
            return configurations.GetAbsolutePath() + configurations.thumbnails_path.Substring(2);
        }

        public static string GetAbsoluteContributionPath()
        {
            return configurations.GetAbsolutePath() + configurations.contributions_path.Substring(2);
        }

        public static void LoadIconImages()
        {
            img_background_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + background_pic));
            img_drop_avatar_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + drop_avatar_pic));
            img_loading_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + loading_image_pic));
            img_empty_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + empty_image_pic));
            img_not_found_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + not_found_image_pic));
            img_sound_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + sound_image_pic));
            img_video_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + video_image_pic));
            img_keyboard_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + keyboard_pic));
            img_close_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + close_icon));
            img_collection_window_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + collection_window_icon));
            img_signup_window_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + signup_window_icon));
            img_change_view_list_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + change_view_list_icon));
            img_change_view_stack_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + change_view_stack_icon));
            img_signup_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + signup_icon));
        }

        public static string GetDate_Formatted(DateTime dt)
        {
            string r = dt.Day.ToString() + " " + GetMonthName(dt.Month) + " " + dt.Year.ToString();
            return r;
        }

        public static string GetCurrentDate_Formatted()
        {
            DateTime dt = DateTime.Now;
            string r = dt.Day.ToString() + " " + GetMonthName(dt.Month) + " " + dt.Year.ToString();
            return r;
        }

        public static String GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    return "Unknown";
            }
        }

        public static ImageSource GetThumbnailFromImage(string filename, int height)
        {
            BitmapImage bi = new BitmapImage();
            try
            {
                // create the thumbnail
                bi.BeginInit();
                bi.DecodePixelHeight = height;
                //bi.DecodePixelWidth = width;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = new Uri(configurations.GetAbsoluteContributionPath() + filename);
                bi.EndInit();
                bi.Freeze();
            }
            catch (Exception)
            {
                // could not create thumbnail -- reason: filenotfound or currupt download or ...
                // write log
                return null;
            }
            return bi;
        }

        public static ImageSource GetThumbnailFromVideo(string filename, TimeSpan interval, int height)
        {
            MediaPlayer _mediaPlayer = new MediaPlayer();
            _mediaPlayer.ScrubbingEnabled = true;
            _mediaPlayer.Open(new Uri(configurations.GetAbsoluteContributionPath() + filename));
            _mediaPlayer.Play();
            _mediaPlayer.Pause();
            _mediaPlayer.Position = interval;
            System.Threading.Thread.Sleep(5 * 1000);
            ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + video_image_pic));
            src.Freeze();
            //uint[] framePixels = new uint[width * height];
            // Render the current frame into a bitmap
            var drawingVisual = new DrawingVisual();
            try
            {
                using (var drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawVideo(_mediaPlayer, new System.Windows.Rect(0, 0, height * _mediaPlayer.NaturalVideoWidth / _mediaPlayer.NaturalVideoHeight, height));
                    drawingContext.DrawImage(src, new System.Windows.Rect(0, 0, height * _mediaPlayer.NaturalVideoWidth / _mediaPlayer.NaturalVideoHeight, height));
                    //drawingContext.DrawVideo(_mediaPlayer, new System.Windows.Rect(0, 0, width, width));
                    //drawingContext.DrawImage(src, new System.Windows.Rect(0, 0, width, width));
                }
                var renderTargetBitmap = new RenderTargetBitmap(height * _mediaPlayer.NaturalVideoWidth / _mediaPlayer.NaturalVideoHeight, height, 96, 96, PixelFormats.Default);
                renderTargetBitmap.Render(drawingVisual);
                return renderTargetBitmap;
            }
            catch (Exception) { return null; }
        }

        public static void SaveThumbnail(BitmapSource src, string filename)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(src));
            using (var fs = new FileStream(configurations.GetAbsoluteThumbnailPath() + filename + ".jpg", FileMode.Create))
            {
                encoder.Save(fs);
            }
        }

        public static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null)
            {
                return null;
            }
            if (element.GetType() == type)
            {
                return element;
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                {
                    break;
                }
            }
            return foundElement;
        }

        public static string GetItemFromJSON(string whole, string key)
        {
            if (whole.Length < 3) return "";
            string data = whole.Substring(1, whole.Length - 2);
            string[] items = data.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                string[] values = item.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Count() < 2) continue;
                if (values[0].Length < 3) continue;
                string pkey = values[0].Substring(1, values[0].Length - 2);
                if (pkey == key)
                {
                    string value = "";
                    if (!values[1].Contains('"'))
                    {
                        value = values[1].Trim();
                        return value;
                    }
                    if (values[1].Length < 3) return "";
                    value = values[1].Substring(1, values[1].Length - 2);
                    return value;
                }
            }
            return "";
        }

        public static List<string> GetUserNameList_GDText(string text_file)
        {
            List<string> usernames = new List<string>();
            string[] lines = text_file.Split(new Char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Count() < 2) { usernames.Add(""); continue; }
                string[] values = parts[1].Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Count() < 2) { usernames.Add(""); continue; }
                usernames.Add(values[1].Trim());
            }
            return usernames;
        }

        public static List<string> GetAvatarList_GDText(string text_file)
        {
            List<string> avatars = new List<string>();
            string[] lines = text_file.Split(new Char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Count() < 3) { avatars.Add(""); continue; }
                string[] values = parts[2].Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Count() < 2) { avatars.Add(""); continue; }
                string value = values[1].Trim();
                value = value.Substring(0, value.Length - 1);
                if (value.Substring(value.Length - 4, 4) != ".png")
                    value = value + ".png";
                avatars.Add(value);
            }
            return avatars;
        }

        public static int get_or_create_collection(naturenet_dataclassDataContext db, int user_id, int activity_id, DateTime dt)
        {
            var r = from c in db.Collections
                    where ((c.user_id == user_id) && c.activity_id == activity_id)
                    orderby c.date descending
                    select c;
            if (r.Count() != 0)
            {
                foreach (Collection col in r)
                {
                    if (configurations.GetDate_Formatted(col.date) == configurations.GetDate_Formatted(dt))
                        return col.id;
                }
            }

            // create new collection
            Collection cl = new Collection();
            cl.activity_id = activity_id;
            cl.date = dt;
            cl.name = configurations.GetDate_Formatted(dt);
            cl.user_id = user_id;
            db.Collections.InsertOnSubmit(cl);
            db.SubmitChanges();
            return cl.id;
        }

        public static int get_or_create_collection(naturenet_dataclassDataContext db, string user_name, string avatar, int activity_id, DateTime dt)
        {
            int user_id =0;
            var ru = from u in db.Users
                     where u.name == user_name
                     select u;
            if (ru.Count() == 0)
            {
                User u0 = new User();
                u0.name = user_name; u0.avatar = avatar;
                u0.password = ""; u0.email = "";
                db.Users.InsertOnSubmit(u0);
                db.SubmitChanges();
                user_id = u0.id;
            }
            else
            {
                user_id = ru.First<User>().id;
            }

            var r = from c in db.Collections
                    where ((c.user_id == user_id) && c.activity_id == activity_id)
                    orderby c.date descending
                    select c;
            if (r.Count() != 0)
            {
                foreach (Collection col in r)
                {
                    if (configurations.GetDate_Formatted(col.date) == configurations.GetDate_Formatted(dt))
                        return col.id;
                }
            }

            // create new collection
            Collection cl = new Collection();
            cl.activity_id = activity_id;
            cl.date = dt;
            cl.name = configurations.GetDate_Formatted(dt);
            cl.user_id = user_id;
            db.Collections.InsertOnSubmit(cl);
            db.SubmitChanges();
            return cl.id;
        }

        public static void SetSettingsFromConfig(iniparser parser)
        {
            // locations in MJ's laptop
            //Point p1 = new Point(120, 382); locations.Add(p1);
            //Point p2 = new Point(140, 420); locations.Add(p2);
            //Point p3 = new Point(290, 230); locations.Add(p3);
            //Point p4 = new Point(400, 300); locations.Add(p4);
            //Point p5 = new Point(405, 480); locations.Add(p5);
            //Point p6 = new Point(480, 595); locations.Add(p6);
            //Point p7 = new Point(515, 690); locations.Add(p7);
            //Point p8 = new Point(465, 755); locations.Add(p8);
            //Point p9 = new Point(415, 755); locations.Add(p9);
            //Point p10 = new Point(180, 545); locations.Add(p10);
            //Point p11 = new Point(150, 570); locations.Add(p11);

            // locations in tabletop
            Point p1 = new Point(453 - 240, 530 - 20); locations.Add(p1);
            Point p2 = new Point(491 - 240, 577 - 20); locations.Add(p2);
            Point p3 = new Point(751 - 240, 323 - 20); locations.Add(p3);
            Point p4 = new Point(934 - 230, 406 - 15); locations.Add(p4);
            Point p5 = new Point(949 - 240, 659 - 20); locations.Add(p5);
            Point p6 = new Point(1083 - 230, 810 - 20); locations.Add(p6);
            Point p7 = new Point(1149 - 240, 923 - 10); locations.Add(p7);
            Point p8 = new Point(1057 - 230, 1021 - 10); locations.Add(p8);
            Point p9 = new Point(965 - 230, 1018 - 10); locations.Add(p9);
            Point p10 = new Point(555 - 240, 743 - 20); locations.Add(p10);
            Point p11 = new Point(505 - 240, 776 - 20); locations.Add(p11);

            //locations.Add();
            //this.??? = parser.GetValue("Section","Key",default_value);
        }

        public static string preset_config_file = "";
    }
}
