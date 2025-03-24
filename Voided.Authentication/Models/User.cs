using Voided.Authentication.Enums;

namespace Voided.Authentication.Models
{
    public class User
    {
        public int Id { get; init; }
        public string Username { get; init; }
        public Usergroup Usergroup { get; init; }
        public string Salt { get; init; }
        public DateTime Expiration { get; init; }
    }
}