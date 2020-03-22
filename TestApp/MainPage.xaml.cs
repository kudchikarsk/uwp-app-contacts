using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TestApp.Utils;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ContactsRepository repository;

        public MainPage()
        {
            this.InitializeComponent();
            repository = new ContactsRepository();
            this.Loaded += OnWindowLoad;
            Application.Current.Suspending += OnSuspending;
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            var contacts = contactListView.Items.OfType<Contact>().ToList();
            await repository.Save(contacts);
            deferral.Complete();
        }

        private async void OnWindowLoad(object sender, RoutedEventArgs e)
        {
            var contacts = await repository.LoadContactsAsync();
            contacts.ForEach(c => contactListView.Items.Add(c));
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            contactListView.Items.Add(new Contact() { FirstName="New"});
        }

        private void contactListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contact = contactListView.SelectedItem as Contact;
            if (contact != null) 
            {
                firstName.Text = contact.FirstName ?? "";
                lastName.Text = contact.LastName ?? "";
                phoneNumber.Text = contact.PhoneNumber ?? "";
            }
        }

        private void firstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateContact();
        }

        private void lastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateContact();
        }

        private void phoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateContact();
        }

        private void UpdateContact()
        {
            var contact = contactListView.SelectedItem as Contact;
            if (contact != null)
            {
                contact.FirstName = firstName.Text;
                contact.LastName = lastName.Text;
                contact.PhoneNumber = phoneNumber.Text;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
