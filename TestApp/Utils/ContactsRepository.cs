using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TestApp.Utils
{
    public sealed class ContactsRepository
    {
        const string fileName = "contacts.json";


        public async Task<List<Contact>> LoadContactsAsync() 
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            try
            {                
                StorageFile sampleFile = await storageFolder.GetFileAsync(fileName);
                
                var json = await FileIO.ReadTextAsync(sampleFile);
                if (String.IsNullOrWhiteSpace(json)) return new List<Contact>();

                return JsonConvert.DeserializeObject<List<Contact>>(json);
            }
            catch (FileNotFoundException)
            {
                await storageFolder.CreateFileAsync(fileName);
                return new List<Contact>();
                
            }            
        }

        public async Task Save(List<Contact> contacts)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync(fileName);
            
            var text = JsonConvert.SerializeObject(contacts);
            await FileIO.WriteTextAsync(sampleFile, text);
        }
    }
}
