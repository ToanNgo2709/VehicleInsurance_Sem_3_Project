using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceSem3.Utilities.Crypto
{
    public static class PasswordSecurity
    {
        public static string GetHashedPassword(string password)
        {
            if(password != null)
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
                return hashedPassword;
            }
            
            return "Cannot do function";
        }

        public static bool ValidatePassword(string password, string hashedPassword)
        {
            bool result = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return result;
        }
    }
}
