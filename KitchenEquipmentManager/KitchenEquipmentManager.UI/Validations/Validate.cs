using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.UI.Validations
{
    public static class Validate
    {


        public static string IsUserValid(this User user)
        {
            List<string> errors = new List<string>();
            
            if (!user.FirstName.All(c => !char.IsDigit(c)))
            {
                errors.Add("First name field should not have numeric characters");
            }
            if (!user.LastName.All(c => !char.IsDigit(c)))
            {
                errors.Add("Last name field should not have numeric characters");
            }
            if (!isValidEmail(user.EmailAddress))
            {
                errors.Add("Email should follow the format example@email.com");
            }
            if(user.Password.Length < 5)
            {
                errors.Add("Password should have at least 6 characters");
            }

            string combinedText = string.Join(Environment.NewLine, errors);

            return combinedText;
        }

        public static string IsEquipmentValid(this Equipment equipment)
        {
            List<string> errors = new List<string>();

            if (equipment.SerialNumber.Length != 8)
            {
                errors.Add("Serial Number should be 8 characters");
            }

            string combinedText = string.Join(Environment.NewLine, errors);

            return combinedText;
        }


        private static bool isValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
                return false;

            // Use a regular expression pattern to validate the email address
            var regexPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(trimmedEmail, regexPattern);
        }
    }
}
