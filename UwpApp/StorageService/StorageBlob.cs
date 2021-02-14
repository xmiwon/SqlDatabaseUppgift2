using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace StorageService
{
    public static class StorageBlob
    {


        private static BlobContainerClient _containerClient { get; set; }
        private static BlobClient _blobClient { get; set; }

        public static string _picture { get; set; }




        public static async Task InitializeStorageAsync( string connectionString, string containerName, bool uniqueName = false )
        {
            if (uniqueName)
            {
                containerName = $"{containerName}-{Guid.NewGuid()}";
            }

            
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                
                try
                {
                    //skapar en container
                    _containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
                }
                //Här kan det dyka upp en exception error här i try/catch satsen som står att container redan existerar vilket är sant och det är därför det ska gå över till catch blocken, men det gör det inte. Jag vet inte om detta händer bara med min dator pga prestandan, men jag fick bocka av exception meddelandet och då fungerar det som det ska
                catch
                {
                    //om container redan existerar, hämta existerande container
                    try
                    {
                        
                        _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    }
                    catch { }
                }
            }
            catch { }
        }

        public static async Task UploadFileAsync()
        {
            //Använder file picker då uwp har filåtkomst begränsning. Jag filtrerar filer till bara jpg och png
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            filePicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".png");

            StorageFile file = await filePicker.PickSingleFileAsync();

            //om det finns "file", hämta file pathen och ladda upp till molnet. Sparar länken till _picture
            if (file != null)
            {
                try
                {
                    _blobClient = _containerClient.GetBlobClient(file.Path);
                    var stream = await file.OpenStreamForReadAsync();
                    await _blobClient.UploadAsync(stream, true);
                    _picture = _blobClient.Uri.ToString();
                }
                catch { }

            }

        }












    }


}
