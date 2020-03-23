using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Utils
{
    public static class ContactExtension
    {
        public static Contact Convert(string value)
        {
            var values = value.Split(',');
            return new Contact()
            {
                FirstName = values[0],
                LastName =  values[1],
                PhoneNumber = values[2],
            };
        }
    }
}
