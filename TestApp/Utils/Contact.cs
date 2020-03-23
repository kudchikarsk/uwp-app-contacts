using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace TestApp.Utils
{
    [CreateFromString(MethodName = "TestApp.Utils.ContactExtension.Convert")]
    public class Contact : Observable
    {
        private string firstName;
        private string lastName;
        private string phoneNumber;

        public string FirstName
        {
            get => firstName; set
            {
                firstName = value;
                PropertyChange();
            }
        }
        public string LastName
        {
            get => lastName; set
            {
                lastName = value;
                PropertyChange();
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber; set
            {
                phoneNumber = value;
                PropertyChange();
            }
        }


    }

    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        protected virtual void PropertyChange([CallerMemberName]string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}