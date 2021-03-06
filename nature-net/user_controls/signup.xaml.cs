﻿using System;
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
using System.Security.Cryptography;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for signup.xaml
    /// </summary>
    public partial class signup : UserControl, IVirtualKeyboardInjectable
    {
        virtual_keyboard keyboard;
        ContentControl keyboard_frame;
        Control focused_textbox;
        public UserControl parent;

        public signup()
        {
            InitializeComponent();

            textbox_name.GotFocus += new RoutedEventHandler(textbox_GotFocus);
            textbox_email.GotFocus += new RoutedEventHandler(textbox_GotFocus);
            textbox_password.GotFocus += new RoutedEventHandler(textbox_GotFocus);
            //textbox_name.LostFocus += new RoutedEventHandler(textbox_LostFocus);
            //textbox_email.LostFocus += new RoutedEventHandler(textbox_LostFocus);
            //textbox_password.LostFocus += new RoutedEventHandler(textbox_LostFocus);
            this.button_submit.Click+=new RoutedEventHandler(button_submit_Click);
            this.Unloaded += new RoutedEventHandler(signup_Unloaded);

            this.checkbox_agreement.IsEnabled = true;
        }

        void signup_Unloaded(object sender, RoutedEventArgs e)
        {
            if (keyboard_frame != null)
                window_manager.main_canvas.Children.Remove(keyboard_frame);
        }

        public void UpdateKeyboardLayout()
        {
            if (keyboard_frame != null)
            {
                if (keyboard != null)
                {
                    if (keyboard_frame.Visibility == System.Windows.Visibility.Visible)
                    {
                        keyboard.MoveAlongWith(parent);
                    }
                }
            }
        }

        void textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            focused_textbox = (Control)sender;
            if (keyboard_frame == null)
                keyboard_frame = new ContentControl();
            virtual_keyboard.ShowKeyboard(this, ref keyboard);
            keyboard_frame.Visibility = System.Windows.Visibility.Visible;
            if (keyboard != null)
            {
                if (this.keyboard_frame.Content == null)
                {
                    this.keyboard_frame.Content = keyboard;
                    //this.keyboard.Background = new SolidColorBrush(Colors.White);
                    this.keyboard_frame.Background = new SolidColorBrush(Colors.White);
                    window_manager.main_canvas.Children.Add(keyboard_frame);
                }
                keyboard.MoveAlongWith(parent);
            }
        }

        public void load_window()
        {
            // load avtar listbox
            listbox_avatars.Items.Clear();
            foreach (KeyValuePair<string, ImageSource> kvp in window_manager.avatars)
            {
                Image i = new Image();
                i.Source = kvp.Value;
                i.Tag = kvp.Key;
                listbox_avatars.Items.Add(i);
            }
            reset();
        }

        public Control ControlToInjectInto
        {
            get { return this.focused_textbox; }
        }

        private void button_submit_Click(object sender, RoutedEventArgs e)
        {
            reset();
            desc.Visibility = System.Windows.Visibility.Visible;
            if (textbox_name.Text == "")
            {
                textbox_name.BorderBrush = Brushes.Red;
                textbox_name.BorderThickness = new Thickness(5);
                desc.Text = "Name is empty.";
                return;
            }
            if (textbox_email.Text == "")
            {
                textbox_email.BorderBrush = Brushes.Red;
                textbox_email.BorderThickness = new Thickness(5);
                desc.Text = "Email is empty.";
                return;
            }
            if (textbox_password.Password == "")
            {
                textbox_password.BorderBrush = Brushes.Red;
                textbox_password.BorderThickness = new Thickness(5);
                desc.Text = "Password is empty.";
                return;
            }
            if (!checkbox_agreement.IsChecked.Value)
            {
                checkbox_agreement.BorderBrush = Brushes.Red;
                checkbox_agreement.BorderThickness = new Thickness(5);
                desc.Text = "You should agree to terms and conditions.";
                return;
            }
            if (listbox_avatars.SelectedIndex < 0)
            {
                label_choose_avatar.BorderBrush = Brushes.Red;
                label_choose_avatar.BorderThickness = new Thickness(5);
                desc.Text = "Please select an avatar.";
                return;
            }
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            List<string> usernames = new List<string>();
            var r = from us in db.Users
                    select us.name;
            if (r != null)
                usernames = r.ToList<string>();
            if (usernames.Contains(textbox_name.Text))
            {
                textbox_name.BorderBrush = Brushes.Red;
                textbox_name.BorderThickness = new Thickness(5);
                desc.Text = "This name has already been taken, choose another.";
                return;
            }

            User u = new User();
            u.name = textbox_name.Text;
            u.email = textbox_email.Text;
            //u.password = textbox_password.SecurePassword.ToString();

            //UnicodeEncoding encode = new UnicodeEncoding();
            //byte[] pass_byte = encode.GetBytes(textbox_password.Password);
            //SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            //byte[] pass_hash = sha1.ComputeHash(pass_byte);
            //u.technical_info = "";
            u.avatar = (string)(((Image)listbox_avatars.SelectedItem).Tag);
            try
            {
                db.Users.InsertOnSubmit(u);
                db.SubmitChanges();
                desc.Text = "Congratulations!";
                file_manager.add_user_to_googledrive(u.id, u.name, u.avatar);
                window_manager.load_users();
            }
            catch (Exception) { desc.Text = "Could not complete the operation."; }
        }

        private void reset()
        {
            textbox_name.BorderBrush = Brushes.LightGray;
            textbox_name.BorderThickness = new Thickness(2);
            textbox_email.BorderBrush = Brushes.LightGray;
            textbox_email.BorderThickness = new Thickness(2);
            textbox_password.BorderBrush = Brushes.LightGray;
            textbox_password.BorderThickness = new Thickness(2);
            checkbox_agreement.BorderBrush = Brushes.LightGray;
            checkbox_agreement.BorderThickness = new Thickness(0);
            label_choose_avatar.BorderBrush = Brushes.LightGray;
            label_choose_avatar.BorderThickness = new Thickness(0);
            desc.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
