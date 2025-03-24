namespace Voided.Authentication.Models
{
    public class AuthResponse
    {
        public User? User { get; init; }
        public bool Authenticated { get; init; }
        public string Message { get; init; }
    }
}