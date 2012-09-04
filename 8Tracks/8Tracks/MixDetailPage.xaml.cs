using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _8Tracks.DataModel;
using _8TracksEngine.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace _8Tracks
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class MixDetailPage : _8Tracks.Common.LayoutAwarePage
    {
        public MixDetailPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var item = MixesGroupDataSource.GetItem((String)navigationParameter);
            this.DefaultViewModel["Group"] = MixesGroupDataSource.DataSource[0];
            this.DefaultViewModel["Items"] = MixesGroupDataSource.DataSource[0].Items;
            this.flipView.SelectedItem = item;

            InitialisePlay( item.UniqueId );
        }

        internal async void InitialisePlay( string mixId )
        {
            if (App.Settings.PlayToken == null || App.Settings.PlayToken == string.Empty)
            {
                //TODO: Lock down user token
                SetResult result = await App.Engine.PlayToken(App.Settings.UserToken);
                if (result.errors == null || result.errors == string.Empty)
                {
                    App.Settings.PlayToken = result.play_token;
                }
            }

            //TODO: Lock down user token
            PlayResult playResult = await App.Engine.Play(App.Settings.UserToken, App.Settings.PlayToken, mixId );
            if (playResult.errors == null || playResult.errors == string.Empty)
            {
                MediaElement playMedia = this.FindName("playMedia") as MediaElement;
                if (playMedia != null)
                {
                    playMedia.Source = new Uri(playResult.set.track.url);
                    playMedia.Play();
                }
                
            }

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            var selectedItem = (MixDataSource)this.flipView.SelectedItem;
            pageState["SelectedItem"] = selectedItem.UniqueId;
        }
    }
}
