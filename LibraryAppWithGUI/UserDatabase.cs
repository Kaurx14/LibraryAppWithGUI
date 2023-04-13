using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryAppWithGUI
{
    public class UserDatabase
    {
        public bool IsValidEmail(string email)
        {
            // Regular expression pattern to validate email addresses
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Create a regular expression object
            Regex regex = new Regex(pattern);

            // Use the regular expression to validate the email address
            return regex.IsMatch(email);
        }

        public bool AreValidFields(string email, string firstName, string lastName, string password)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (string.IsNullOrEmpty(firstName)) return false;
            if (string.IsNullOrEmpty(lastName)) return false;
            if (string.IsNullOrEmpty(password)) return false;

            return IsValidEmail(email);
        }
    }
}
