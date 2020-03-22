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
        private List<Contact> _contacts;

        public ContactsRepository()
        {
            
        }

        

        public async Task<List<Contact>> Get(string searchTerm=null) 
        {
            _contacts = _contacts ?? await LoadData();
            return _contacts
               .Where(c =>
                   String.IsNullOrWhiteSpace(searchTerm) ||
                   c.FirstName.Contains(searchTerm) ||
                   c.LastName.Contains(searchTerm) ||
                   c.PhoneNumber.Contains(searchTerm))
               .ToList();
        }

        public void Add(Contact contact)
        {
            _contacts.Add(contact);
        }

        public void Update(Contact contact)
        { 
            //do nothing
        }

        public async Task Save()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync(fileName);
            
            var text = JsonConvert.SerializeObject(_contacts);
            await FileIO.WriteTextAsync(sampleFile, text);
        }

        private async Task<List<Contact>> LoadData()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile sampleFile = await storageFolder.GetFileAsync(fileName);

                var json = await FileIO.ReadTextAsync(sampleFile);
                if (String.IsNullOrWhiteSpace(json))
                {
                    return new List<Contact>();
                }

                return JsonConvert.DeserializeObject<List<Contact>>(json);

            }
            catch (FileNotFoundException)
            {
                await storageFolder.CreateFileAsync(fileName);
                return new List<Contact>();

            }
        }

        
    }
}
