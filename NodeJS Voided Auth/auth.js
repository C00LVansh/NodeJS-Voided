const axios = require('axios');

const BASE_URL = 'https://voided.to/auth.php';
const SALT = 'SALT_HERE'; //Ask Voided developers if you need one.

const Usergroup = {
  Contributor: 8,
  Respected: 1,
  Veteran: 2,
  VIP: 11,
  Exclusive: 12,
  Cosmo: 13
  // Add more if needed
};

class AuthResponse {
  /**
   * @param {boolean} authenticated - Whether the user is authenticated or not.
   * @param {string} message - A message explaining the authentication result.
   * @param {User} [user=null] - The user object if authenticated.
   */
  constructor(authenticated, message, user = null) {
    this.authenticated = authenticated;
    this.message = message;
    this.user = user;
  }
}

class User {
  /**
   * @param {number} id - The user's ID.
   * @param {string} username - The user's username.
   * @param {number} usergroup - The user's usergroup.
   * @param {Date} expiration - The date that the user's authentication will expire.
   * @param {string} salt - The provider's (developer) salt.
   */
  constructor(id, username, usergroup, expiration, salt) {
    this.id = id;
    this.username = username;
    this.usergroup = usergroup;
    this.expiration = expiration;
    this.salt = salt;
  }
}

class AuthManager {
  /**
   * Authenticates a user asynchronously based on their key and required user group.
   *
   * @param {string} key - The user's authentication key.
   * @param {string} provider - The provider associated with developer || Ask Voided developers if you need one.
   * @param {number} requiredGroup - The minimum user group required for authentication.
   * @returns {Promise<AuthResponse>} A promise that resolves to an AuthResponse indicating the result of the authentication.
   */
  async authenticateAsync(key, provider, requiredGroup) {
    try {
      const url = `${BASE_URL}?key=${encodeURIComponent(key)}&provider=${encodeURIComponent(provider)}`;

      const response = await axios.get(url, {
        headers: {
          'PKey': SALT
        }
      });

      const data = response.data;
      if (!data.uid || !data.username || !data.usergroup || !data.salt_time || !data.provider_salt) {
        return new AuthResponse(false, 'Authentication failed. Malformed response.');
    }  
      const expiration = new Date(data.salt_time * 1000);
      if (expiration < new Date()) {
        return new AuthResponse(false, 'Authentication failed. Salt has expired.');
      }  
      const user = new User(
        data.uid,
        data.username,
        data.usergroup,  
        expiration,
        data.provider_salt
      );

      if (user.usergroup >= requiredGroup) {
        return new AuthResponse(true, 'Authentication successful! User has been authenticated.', user);
      } else {
        return new AuthResponse(false, "Authentication failed! User doesn't have the required usergroup.", user);
      }

    } catch (error) {
      return new AuthResponse(false, `Authentication failed. Unexpected exception: ${error.message}`);
    }
  }
}
module.exports = {
  AuthManager,
  Usergroup
};
