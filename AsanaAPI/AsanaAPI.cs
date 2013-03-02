using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using SimpleJSON;

namespace AsanaAPI
{
    public class AsanaAPI
    {
        public enum RequestMethods
        {
            POST,
            GET,
            PUT
        }

        #region Constants
        private const string API_KEY = "1b7gx5Bm.ZbnpShpy1EprffQaSXKRfzp";
        private const string BASE_URL = "https://app.asana.com/api/1.0/";
        #endregion Constants

        #region Private Fields
        private string taskUrl = BASE_URL + "tasks";
        private string userUrl = BASE_URL + "users";
        private string projectsUrl = BASE_URL + "projects";
        private string workspaceUrl = BASE_URL + "workspaces";
        private string storiesUrl = BASE_URL + "stories";
        private string tagsUrl = BASE_URL + "tags";
        #endregion Private Fields

        #region Constructor
        public AsanaAPI()
        {
        }
        #endregion Constructor

        #region Users
        public User GetSingleUser(string userID = "me"){
            if(userID == "") throw new ArgumentException("UserID cannot be blank");

            string url = string.Format("{0}/{1}{2}", userUrl, userID, "?opt_fields=id,name,email,workspaces,workspaces.id,workspaces.name");
            return User.Parse(JSON.Parse(GetResponse(url))["data"]);
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string url = string.Format("{0}{1}", userUrl, "?opt_fields=id,name,email,workspaces,workspaces.id,workspaces.name");
            JSONArray root = JSON.Parse(GetResponse(url))["data"].AsArray;
            foreach(JSONNode userRoot in root)
            {
                users.Add(User.Parse(userRoot));
            }

            return users;
        }

        public string GetUsersInWorkspace(string workspaceID)
        {
            string url = string.Format(userUrl + "/{0}/users{1}", workspaceID, "?opt_fields=name,email,workspaces");
            return GetResponse(url);
        }
        #endregion Users

        private string EncodeParameters(ICollection<KeyValuePair<string, string>> parameters)
        {
            string ret = "";
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                ret += string.Format("{0}={1}&", pair.Key, pair.Value);
            }
            ret = ret.TrimEnd('&');

            return ret;
        }

        public string GetResponse(string uri, ICollection<KeyValuePair<string, string>> parameters, RequestMethods method = RequestMethods.POST)
        {
            return GetResponse(uri, EncodeParameters(parameters), method);
        }

        public string GetResponse(string uri, string data = "", RequestMethods method = RequestMethods.GET)
        {
            System.Diagnostics.Trace.WriteLine(uri);

            // create request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.PreAuthenticate = true;
            request.Method = method.ToString().ToUpper();
            request.ContentType = "application/x-www-form-urlencoded";

            // log in
            string authInfo = API_KEY + ":" + ""; // blank password
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;

            // send data
            if (data != "")
            {
                byte[] paramBytes = Encoding.ASCII.GetBytes(data);
                request.ContentLength = paramBytes.Length;
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(paramBytes, 0, paramBytes.Length);
                reqStream.Close();
            }

            // get response
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (WebException ex)
            {
                HttpWebResponse response = ((HttpWebResponse)ex.Response);
                throw new Exception(uri + " caused a " + (int)response.StatusCode + " error.\n" + response.StatusDescription);
            }
        }
    }
}
