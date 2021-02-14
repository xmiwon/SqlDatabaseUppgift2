using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataAccess.Data
{
    public static class SettingsContext
    {
        private static Settings _settings { get; set; }

        private static StorageFolder storageFolder;
        private static StorageFile file;

        public static async void GetSettingsInformation()
        {
            //Designar hur json innehållet ska se ut och lägger den till en var
            var fileContent = "{\"status\": [\"new\", \"ongoing\", \"closed\"], \"maxItemsCount\": 4, \"category\": [\"internet\", \"hardware\", \"performance\"]}";

            //Väljer mappen Documents och skapar en json fil
            storageFolder = KnownFolders.DocumentsLibrary;
            await storageFolder.CreateFileAsync("settings.json", CreationCollisionOption.ReplaceExisting);
            //Hämtar och skriver in fileContent i filen
            file = await storageFolder.GetFileAsync("settings.json");
            await FileIO.WriteTextAsync(file, fileContent);

            //Läser och konverterar json till en objekt
            var json = await FileIO.ReadTextAsync(file);
            _settings = JsonConvert.DeserializeObject<Settings>(json);
        }

 
        public static async Task<IEnumerable<string>> GetStatus()
        {
            var list = new List<string>();
            //Loopar igenom alla statusen och läggs till en lista
            foreach (var status in _settings.status)
            {
                list.Add(status);
            }
            return list;
        }

        public static async Task<IEnumerable<string>> GetCategory()
        {
            var list = new List<string>();

            foreach (var category in _settings.category)
            {
                list.Add(category);
            }
            return list;
        }

        public static int GetMaxItemsCount()
        {
            return _settings.maxItemsCount;
        }

    }



    public class Settings
    {
        public string[] status { get; set; }
        public string[] category { get; set; }
        public int maxItemsCount { get; set; }
    }

}
