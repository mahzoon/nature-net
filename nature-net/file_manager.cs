using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Authentication;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Util;
using DotNetOpenAuth;
using DotNetOpenAuth.OAuth2;

namespace nature_net
{
    public class file_manager
    {

        public static bool download_file_from_googledirve(string file_name, int contribution_id)
        {
            try
            {
                var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
                provider.ClientIdentifier = configurations.googledrive_client_id;
                provider.ClientSecret = configurations.googledrive_client_secret;
                IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
                DriveService gd_service = new DriveService(authenticator);

                FilesResource.ListRequest list_request = gd_service.Files.List();
                list_request.Q = "title = '" + file_name + "'";
                FileList file_list = list_request.Fetch();
                byte[] transfer_buffer = new byte[configurations.download_buffer_size];
                int downloaded_files_count = 0;
                foreach (File f in file_list.Items)
                {
                    if (String.IsNullOrEmpty(f.DownloadUrl)) continue;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(f.DownloadUrl));
                    authenticator.ApplyAuthenticationToRequest(request);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode != HttpStatusCode.OK) continue;
                    System.IO.FileStream file_stream = new System.IO.FileStream(
                        configurations.contributions_directory + contribution_id.ToString() + ".jpg", System.IO.FileMode.CreateNew);
                    System.IO.Stream response_stream = response.GetResponseStream();
                    int bytes_read = 0;
                    while ((bytes_read = response_stream.Read(transfer_buffer, 0, transfer_buffer.Length)) > 0) { file_stream.Write(transfer_buffer, 0, bytes_read); }
                    downloaded_files_count++;
                    file_stream.Close();
                    response_stream.Close();
                }
                if (downloaded_files_count > 0) return true;
                else return false;
            }
            catch (Exception gd_exc)
            {
                // write log of the exception
                return false;
            }
        }

        public static void retrieve_and_process_media_changes_from_googledrive()
        {
            file_manager.retrieve_and_process_user_changes_from_googledrive();

            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
            provider.ClientIdentifier = configurations.googledrive_client_id;
            provider.ClientSecret = configurations.googledrive_client_secret;
            IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
            DriveService gd_service = new DriveService(authenticator);

            string startChangeId = configurations.googledrive_lastchange;
            List<Change> result = new List<Change>();
            ChangesResource.ListRequest request = gd_service.Changes.List();

            if (!String.IsNullOrEmpty(startChangeId))
            {
                request.StartChangeId = startChangeId;
            }
            do
            {
                try
                {
                    ChangeList changes = request.Fetch();
                    configurations.googledrive_lastchange = changes.LargestChangeId;
                    result.AddRange(changes.Items);
                    request.PageToken = changes.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    request.PageToken = null;
                }
            } while (!String.IsNullOrEmpty(request.PageToken));

            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            foreach (Change c in result)
            {
                if (c.File == null)
                    continue;
                if (c.File.Parents[0].IsRoot.HasValue)
                    if (!c.File.Parents[0].IsRoot.Value)
                        continue;

                if (c.Deleted.HasValue)
                    if (c.Deleted.Value)
                    {
                        //delete from database if exists
                        //delete from hard drive if exists
                        continue;
                    }

                if (c.File.Title != "Users")
                {
                    if (c.File.Description == null) continue;

                    string desc = c.File.Description;
                    string username = configurations.GetItemFromJSON(desc, "username");
                    if (username == "") continue;
                    
                    var contribution_list = from contrib in db.Contributions
                                            where contrib.media_url.Contains(c.File.Title)
                                            select contrib;
                    if (contribution_list.Count() == 0)
                    {
                        Contribution contribute = new Contribution();
                        contribute.media_url = c.File.Title;
                        DateTime dt = DateTime.Now;
                        bool hasdate = DateTime.TryParse(c.File.CreatedDate, out dt);
                        contribute.date = dt;
                        try { contribute.location_id = Convert.ToInt32(configurations.GetItemFromJSON(desc, "landmarkId")); }
                        catch (Exception) { contribute.location_id = 0; }
                        contribute.note = configurations.GetItemFromJSON(desc, "comment");
                        contribute.tags = configurations.GetItemFromJSON(desc, "categories");
                        db.Contributions.InsertOnSubmit(contribute);
                        db.SubmitChanges();
                        int activity_id = 0;
                        try { activity_id = Convert.ToInt32(configurations.GetItemFromJSON(desc, "activityId")) + 1; }
                        catch (Exception) { activity_id = 0; }
                        string avatar_name = configurations.GetItemFromJSON(desc, "avatarName");
                        if (avatar_name.Substring(avatar_name.Length - 4, 4) != ".png")
                            avatar_name = avatar_name + ".png";
                        int collection_id = configurations.get_or_create_collection(db, username, avatar_name, activity_id, dt);
                        Collection_Contribution_Mapping map = new Collection_Contribution_Mapping();
                        map.collection_id = collection_id;
                        map.contribution_id = contribute.id;
                        map.date = DateTime.Now;
                        db.Collection_Contribution_Mappings.InsertOnSubmit(map);
                        db.SubmitChanges();
                    }
                }
            }
        }

        public static void retrieve_and_process_user_changes_from_googledrive()
        {
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
            provider.ClientIdentifier = configurations.googledrive_client_id;
            provider.ClientSecret = configurations.googledrive_client_secret;
            IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
            DriveService gd_service = new DriveService(authenticator);

            FilesResource.ListRequest list_request = gd_service.Files.List();
            list_request.Q = "title = 'Users'";
            FileList file_list = list_request.Fetch();
            if (file_list.Items.Count == 0) return;
            File users_list_file = file_list.Items[0];
            if (String.IsNullOrEmpty(users_list_file.DownloadUrl)) return;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(users_list_file.DownloadUrl));
            authenticator.ApplyAuthenticationToRequest(request);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) return;
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string users_list = reader.ReadToEnd();
            reader.Close();
            List<string> usernames = configurations.GetUserNameList_GDText(users_list);
            List<string> avatars = configurations.GetAvatarList_GDText(users_list);

            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var ru = from u in db.Users
                     select u.name;
            List<string> users = ru.ToList<string>();
            for (int counter = 0; counter < usernames.Count; counter++)
            {
                if (!users.Contains(usernames[counter]))
                {
                    User u3 = new User();
                    u3.name = usernames[counter]; u3.password = ""; u3.email = "";
                    try { u3.avatar = avatars[counter]; }
                    catch (Exception) { u3.avatar = ""; }
                    db.Users.InsertOnSubmit(u3);
                }
            }
            db.SubmitChanges();
        }

        public static void add_user_to_googledrive(int user_id, string username, string avatar)
        {
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
            provider.ClientIdentifier = configurations.googledrive_client_id;
            provider.ClientSecret = configurations.googledrive_client_secret;
            IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
            DriveService gd_service = new DriveService(authenticator);

            FilesResource.ListRequest list_request = gd_service.Files.List();
            list_request.Q = "title = 'Users'";
            FileList file_list = list_request.Fetch();
            if (file_list.Items.Count == 0) return;
            File users_list_file = file_list.Items[0];
            string user_file_id = users_list_file.Id;
            if (String.IsNullOrEmpty(users_list_file.DownloadUrl)) return;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(users_list_file.DownloadUrl));
            authenticator.ApplyAuthenticationToRequest(request);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) return;
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string users_list = reader.ReadToEnd();
            reader.Close();

            string u = "{user: id= " + user_id.ToString() + ", name= " + username + ", avatarName= " + avatar + "}\r\n";
            users_list = users_list + u;
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
            writer.Write(users_list);
            writer.Flush();
            stream.Position = 0;
            
            //users_list_file.Title = newTitle;
            //users_list_file.Description = newDescription;
            string new_mime_type = users_list_file.MimeType;
            FilesResource.UpdateMediaUpload request_update = gd_service.Files.Update(users_list_file, user_file_id, stream, new_mime_type);
            request_update.Upload();
        }

        private static IAuthorizationState googledrive_getauthorization(NativeApplicationClient client)
        {
            string[] scopes = new string[1] { "" };
            IAuthorizationState state = new AuthorizationState(scopes) { RefreshToken = configurations.googledrive_refresh_token };
            if (state != null)
            {
                client.RefreshToken(state);
            }
            return state;
        }
    }
}
