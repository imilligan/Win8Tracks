using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Storage;

namespace _8Tracks.Settings
{
    public class AppSettings
    {
        Windows.Storage.ApplicationDataContainer roamingSettings;
        ApplicationDataContainer localSettings;
        PasswordVault vault;

        public AppSettings()
        {

            roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            localSettings = ApplicationData.Current.LocalSettings;

        }

        public string Login
        {
            get
            {
                if (roamingSettings.Values.ContainsKey(Constants.Keys.LOGIN))
                {
                    return roamingSettings.Values[Constants.Keys.LOGIN] as string;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                roamingSettings.Values[Constants.Keys.LOGIN] = value;
            }
        }
        public string UserToken
        {
            get
            {
                if (localSettings.Values.ContainsKey(Constants.Keys.USER_TOKEN))
                {
                    return localSettings.Values[Constants.Keys.USER_TOKEN] as string;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                localSettings.Values[Constants.Keys.USER_TOKEN] = value;
            }
        }

        public string PlayToken
        {
            get
            {
                if (localSettings.Values.ContainsKey(Constants.Keys.PLAY_TOKEN))
                {
                    return localSettings.Values[Constants.Keys.PLAY_TOKEN] as string;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                localSettings.Values[Constants.Keys.PLAY_TOKEN] = value;
            }
        }

        public bool TryGetPassword(string username, out string password)
        {
            vault = new Windows.Security.Credentials.PasswordVault();
            try
            {
                IReadOnlyList<PasswordCredential> creds = vault.FindAllByUserName(username);
                foreach (var cred in creds)
                {
                    if (cred.UserName == username )
                    {
                        cred.RetrievePassword();
                        password = cred.Password;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
            }
            password = null;
            return false;

        }
        public void SavePassword(string username, string password)
        {
            vault = new Windows.Security.Credentials.PasswordVault();
            try
            {
                IReadOnlyList<PasswordCredential> creds = vault.FindAllByResource(username);
                foreach (PasswordCredential c in creds)
                {
                    try
                    {
                        vault.Remove(c);
                    }
                    catch (Exception e) // Stored credential was deleted
                    {
                    }
                }
            }
            catch (Exception e)
            {
            }            
            PasswordCredential cred = new PasswordCredential(Constants.Keys.VAULT_RESOURCE, username, password );
            vault.Add(cred);
        }

    }
}
