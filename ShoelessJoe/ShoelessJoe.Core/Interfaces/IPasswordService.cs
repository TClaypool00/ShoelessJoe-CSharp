namespace ShoelessJoe.Core.Interfaces
{
    public interface IPasswordService
    {
        public string InvalidPasswordMessage { get; }
        public string PasswordDoesNotMeetRequirementsMessage { get; }

        public string HashPassword(string password);
        public bool VerifyPassword(string text, string password);
        public bool PasswordMeetsRequirements(string password);
    }
}
