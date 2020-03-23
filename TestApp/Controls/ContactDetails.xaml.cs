using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TestApp.Utils;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TestApp.Controls
{
    [ContentProperty( Name = "Contact")]
    public sealed partial class ContactDetails : UserControl
    {
        Contact contact;
        private readonly ContactsRepository repository;

        public ContactDetails()
        {
            this.InitializeComponent();
            repository = new ContactsRepository();
        }

        public Contact Contact
        {
            get => contact; 
            set
            {
                contact = value;
                if (contact != null)
                {
                    firstName.Text = contact.FirstName ?? "";
                    lastName.Text = contact.LastName ?? "";
                    phoneNumber.Text = contact.PhoneNumber ?? "";
                }
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
            if (Contact != null)
            {
                Contact.FirstName = firstName.Text;
                Contact.LastName = lastName.Text;
                Contact.PhoneNumber = phoneNumber.Text;
                repository.Update(Contact);
            }
        }
    }
}
