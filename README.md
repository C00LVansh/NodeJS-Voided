# Voided Authentication Module

A lightweight Node.js module to authenticate users via the Voided API with group-level access control.

## 📦 Installation

```bash
npm install axios
```

*(This module assumes `axios` is already installed.)*

## 📁 Usage

```js
 const { AuthManager, Usergroup } = require('./auth.js');
(async () => {
    const manager = new AuthManager();
  
    const userKey = 'KEY_HERE';
    const provider = 'PROVIDER'; 
    const requiredGroup = Usergroup.Cosmo; 
  
    const authResult = await manager.authenticateAsync(userKey, provider, requiredGroup);
    if(authResult.authenticated) {
    //your code
    } 
  })();
```

## 🧪 Example Response

```js
AuthResponse {
  authenticated: true,
  message: 'Authentication successful! User has been authenticated.',
  user: User {
    id: 123,
    username: 'testuser',
    usergroup: 11,
    expiration: 2025-05-01T00:00:00.000Z,
    salt: 'some_salt'
  }
}
```

## 👤 User Groups

```js
const Usergroup = {
  Contributor: 8,
  Respected: 1,
  Veteran: 2,
  VIP: 11,
  Exclusive: 12,
  Cosmo: 13
};
```

## 🔐 Security

- Do **not** hardcode sensitive information like your provider key or salt.
- Use environment variables:
  ```js
  const SALT = process.env.VOIDED_SALT;
  ```

## ⚠️ Notes

- You might need to ask Voided developers for your specific `provider` and `salt`.
- This library assumes the API response includes fields like `uid`, `username`, `usergroup`, `salt_time`, and `provider_salt`.


