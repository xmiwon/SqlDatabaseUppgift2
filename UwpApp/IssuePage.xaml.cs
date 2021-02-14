using DataAccess.Data;
using DataAccess.Models;
using StorageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IssuePage : Page
    {
        private IEnumerable<Issue> issues { get; set; }
        private long _customerId { get; set; }




        public IssuePage()
        {
            this.InitializeComponent();
            LoadCustomersAsync().GetAwaiter();
            LoadIssuesAsync().GetAwaiter();
            LoadStatusAsync().GetAwaiter();
            LoadCategoryAsync().GetAwaiter();
            
            var connectionString = "YOUR_CONNECTION_String";
            
            var containerName = "YOUR_CONTAINER_NAME";
            StorageBlob.InitializeStorageAsync(connectionString, containerName).GetAwaiter();


        }



        private async Task LoadCustomersAsync()
        {
            //Exekverar funktionen och hämtar alla customer namn som sedan läggs in på comboxen
            cmbCustomers.ItemsSource = await SqliteContext.GetCustomerNames();
        }



        private async Task LoadIssuesAsync()
        {
            issues = await SqliteContext.GetIssues();
            LoadActiveIssues();
            LoadClosedIssues();
        }


        private async Task LoadStatusAsync()
        {

            cmbChangeStatus.ItemsSource = await SettingsContext.GetStatus();
        }

        private async Task LoadCategoryAsync()
        {
            cmbCategory.ItemsSource = await SettingsContext.GetCategory();
        }





        private void LoadActiveIssues()
        {
            //En list view som visar alla issues som INTE är "closed" och är sorterad efter datum. Den är också begränsad till hur många som visas på en gång beroende på värdet från json filen
            lvActiveIssues.ItemsSource = issues
                .Where(i => i.Status != "closed")
                .OrderByDescending(i => i.Created)
                .Take(SettingsContext.GetMaxItemsCount())
                .ToList();



        }
        private void LoadClosedIssues()
        {
            //En list view som visar alla issues som är "closed"
            lvClosedIssues.ItemsSource = issues.Where(i => i.Status == "closed").ToList();
        }

        private async void CreateIssue_Click( object sender, RoutedEventArgs e )
        {
            //Om alla av dem här inte har tomma strängar eller innehåller värdet null så exekveras CreateIssueAsync med dynamiska värden
            if (tbxTitle.Text != "" && tbxDesc.Text != "" && cmbCategory.SelectedItem != null)
            {

                await SqliteContext.CreateIssueAsync(
                    new Issue
                    {
                        Title = $"{tbxTitle.Text} - ({ Guid.NewGuid()})",
                        Description = tbxDesc.Text,
                        Category = cmbCategory.SelectedItem.ToString(),
                        //Skickar iväg värden som är vald på comboboxen. Jag konverterar om till string för säkerhets skull
                        CustomerId = await SqliteContext.GetCustomerIdByName(cmbCustomers.SelectedItem.ToString()),
                        //hämtar länken till bilden som är sparad i _picture
                        PictureSource = StorageBlob._picture
                    }
                 );
                await LoadIssuesAsync();
                uploadMessage.Text = "";
            }
        }
        private async void cmbChangeStatus_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            //Hämtar issue id
            var issueId = (sender as ComboBox).Tag;
            await SqliteContext.UpdateIssueAsync((long)issueId, (string)cmbChangeStatus.SelectedItem);
            await LoadIssuesAsync();

        }

        private async void UploadPicture_Click( object sender, RoutedEventArgs e )
        {
            await StorageBlob.UploadFileAsync();
            if (StorageBlob._picture != null)
                uploadMessage.Text = "Picture uploaded!";
        }

        private async void createComment_Click( object sender, RoutedEventArgs e )
        {
           
            var issueId = (sender as Button).Tag;

            await SqliteContext.CreateCommentAsync(
                new Comment
                {
                    IssueId = (long)issueId,
                    Description = tbxComment.Text
                }
            );
            await LoadIssuesAsync();

        }
    }
}
