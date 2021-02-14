using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        private void NavView_Loaded( object sender, RoutedEventArgs e )
        {
            ContentFrame.Navigate(typeof(IssuePage));
        }

        private void NavView_SelectionChanged( NavigationView sender, NavigationViewSelectionChangedEventArgs args )
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                //Item som man har valt och ska casta om den som navigationviewitem
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag.ToString())
                {
                    case "IssuePage":
                        ContentFrame.Navigate(typeof(IssuePage));
                        break;
                    case "CustomerPage":
                        ContentFrame.Navigate(typeof(CustomerPage));
                        break;

                }
            }




        }
    }
}
