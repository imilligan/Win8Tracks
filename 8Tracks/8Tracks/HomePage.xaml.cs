using _8Tracks.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using _8TracksEngine.Model;
using Windows.UI.Popups;
using _8Tracks.DataModel;
using System.Collections.ObjectModel;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace _8Tracks
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : _8Tracks.Common.LayoutAwarePage
    {
        public GroupedItemsPage()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<MixesGroupDataSource> mixesData;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string fake;
            fake = "hello";
        }

        internal async Task DisplayTextResult(LoginResponse response)
        {
            MessageDialog message = new MessageDialog( response.current_user.slug );
            await message.ShowAsync();
        }

        internal async void SaveAuthResult( Task<LoginResponse> response, string password )
        {
            LoginResponse login = await response;
            App.Settings.Login = login.current_user.slug;
            App.Settings.SavePassword(login.current_user.slug, password);
            App.Settings.UserToken = login.user_token;
        }

        internal async void ProcessMixes( Task<MixesResult> response)
        {
            MixesResult mixes = await response;
            MixesGroupDataSource.DataSource = new ObservableCollection<MixesGroupDataSource>();
            MixesGroupDataSource.DataSource.Add(new MixesGroupDataSource("group1", "Mixes", "The top 10 mixes", "http://", "I don't have a description"));
            if (mixes.errors == string.Empty || mixes.errors == null)
            {
                MixesGroupDataSource.DataSource[0].AddItems(mixes.mixes);               
            }
            this.DefaultViewModel["Groups"] = MixesGroupDataSource.DataSource;
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
            string username = App.Settings.Login;
            string password = string.Empty;
            bool needsCred = username == null || username == string.Empty;
            if (!needsCred)
            {
                needsCred = !App.Settings.TryGetPassword(username, out password);
            }
            if (needsCred)
            {
                Grid passwordPromt = this.FindName("passwordPrompt") as Grid;
                if (passwordPromt != null)
                {
                    passwordPromt.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
            else
            {
                
                ProcessMixes(App.Engine.Mixes(App.Settings.UserToken));
            }
            
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            //var sampleDataGroups = SampleDataSource.GetGroups((String)navigationParameter);
            //this.DefaultViewModel["Groups"] = sampleDataGroups;


        }

       

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            var group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            this.Frame.Navigate(typeof(GroupDetailPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((MixDataSource)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(MixDetailPage), itemId);
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox loginBox = this.FindName("loginBox") as TextBox;
            PasswordBox passwordBox = this.FindName("passwordBox") as PasswordBox;
            if (loginBox != null && passwordBox != null)
            {
                SaveAuthResult(App.Engine.Authenticate(loginBox.Text, passwordBox.Password), passwordBox.Password);
                Grid passwordPromt = this.FindName("passwordPrompt") as Grid;
                if (passwordPromt != null)
                {
                    passwordPromt.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
    }
}
