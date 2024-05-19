using ShoelessJoe.Core.Interfaces;
using System.Text.RegularExpressions;

namespace ShoelessJoe.DataAccess.Services
{
    public class PasswordService : IPasswordService
    {
        //TODO: Put strings in it's own AppSettings
        public string InvalidPasswordMessage => "Invalid Password";

        public string PasswordDoesNotMeetRequirementsMessage => "Password does not meet our requirements";

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool PasswordMeetsRequirements(string password)
        {
            return Regex.IsMatch(password, @"^(.{8,20}|[^0-9]*|[^A-Z])$");
        }

        public bool VerifyPassword(string text, string password)
        {
            return BCrypt.Net.BCrypt.Verify(text, password);
        }
    }
}
