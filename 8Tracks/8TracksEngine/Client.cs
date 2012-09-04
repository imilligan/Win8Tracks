using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using _8TracksEngine.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _8TracksEngine
{
    public class Client
    {
       
        #region Private Members

        HttpClient m_httpClient = null;

        #endregion

        #region Creation

        public Client()
        {
            CreateClient(new Uri(Constants.TRACKS_HOME));
        }


        private void CreateClient(Uri server, string userToken = null)
        {
            if (m_httpClient != null)
            {
                m_httpClient.Dispose();
            }
            HttpMessageHandler handler = new HttpClientHandler();

            // Use this code to add custom headers (like api key) onto data
            handler = new PlugInHandler(handler, userToken);

            m_httpClient = new HttpClient(handler);
            m_httpClient.BaseAddress = server;
        }

        #endregion

        #region Auth

        public async Task<LoginResponse> Authenticate(string username, string password )
        {
            string resourceAddress = "sessions.json";

            CreateClient(new Uri(Constants.TRACKS_HOME));

            //resourceAddress += "format=json";

            string postData = "login=";
            postData += username;
            postData += "&";
            postData += "password=";
            postData += password;

            HttpContent content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage response = await m_httpClient.PostAsync(resourceAddress, content);
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(responseString);                               
            }
            catch (HttpRequestException hre)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MixesResult> Mixes(string userToken)
        {
            //TODO: Remove hard-coded safe browse
            string resourceAddress = "mixes.json?safe_browse=1";

            CreateClient(new Uri(Constants.TRACKS_HOME), userToken);
            
            try
            {
                HttpResponseMessage response = await m_httpClient.GetAsync(resourceAddress);
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MixesResult>(responseString);
            }
            catch (HttpRequestException hre)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MixResult> Mix(string userToken, string id){
            
            string resourceAddress = String.Format( "mixes/{0}.json", id );

            CreateClient(new Uri(Constants.TRACKS_HOME), userToken);
            
            try
            {
                HttpResponseMessage response = await m_httpClient.GetAsync(resourceAddress);
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MixResult>(responseString);
            }
            catch (HttpRequestException hre)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        
        }

        public async Task<SetResult> PlayToken(string userToken)
        {
            string resourceAddress = "sets/new.json";

            CreateClient(new Uri(Constants.TRACKS_HOME), userToken);

            try
            {
                HttpResponseMessage response = await m_httpClient.GetAsync(resourceAddress);
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SetResult>(responseString);
            }
            catch (HttpRequestException hre)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<PlayResult> Play(string userToken, string setToken, string mixId )
        {

            UriBuilder builder = new UriBuilder( Constants.TRACKS_HOME );
            builder.Path = String.Format( "sets/{0}/play.json", setToken );
            builder.Query = String.Format("mix_id={0}", mixId);
            string resourceAddress = builder.ToString();

            CreateClient(new Uri(Constants.TRACKS_HOME), userToken);
            
            try
            {
                HttpResponseMessage response = await m_httpClient.GetAsync(resourceAddress);
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PlayResult>(responseString);
            }
            catch (HttpRequestException hre)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion
    }
}
