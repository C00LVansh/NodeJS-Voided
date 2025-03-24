using Voided.Authentication.Models;
using static Voided.Authentication.Enums.Usergroup;

namespace Voided.Authentication.Example
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            var manager = new AuthManager();

            /* Key should be provided by user */
            /* Provider should be provided by the developer */
            /* Required usergroup for the tool */
            var key = string.Empty;
            var provider = string.Empty;
            var requiredGroup = VIP;

            var response = await manager.AuthenticateAsync(key, provider, requiredGroup);

            if (response.Authenticated)
            {
                /* Any further logic here, from here user is fully authenticed. */
                /* Required usergroup has already been compared, you can obtain user data thru the User property. */
            }
        }
    }
}