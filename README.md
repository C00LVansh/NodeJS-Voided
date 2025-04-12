

# 🔐 Voided Authentication System

A community-driven implementation of the [Voided.to](https://voided.to) user authentication logic — supporting multiple languages and environments. Authenticate users using their API key, validate usergroups, and manage access control with ease.

> 💡 This repository contains implementations in **C#**, **Node.js**, and potentially more — contributed and maintained by developers who love to build smarter tooling on top of the Voided platform.

---

## 🚀 What is This?

This repo provides reusable logic to authenticate users from Voided.to using their developer API. Each implementation checks:
- If a user key is valid
- If the salt is valid (not expired)
- If the user belongs to a required user group
- And returns meaningful details back to your app

---

## 📂 Available Implementations

| Language | Folder / File                  | Description                          |
|----------|--------------------------------|--------------------------------------|
| C#       | `Voided.Authentication/`       | Original implementation              |
| C#       | `Voided.Authentication.Example/` | Sample usage in C#                  |
| Node.js  | `NodeJS Voided Auth/auth.js`      | Implementation in NodeJS      |
| Node.js | `NodeJS Voided Auth/index.js`   | Sample usage in NodeJS |
> Want to contribute in Python, Go, Rust, or another language? PRs are welcome! 💙

---

## 🧪 Sample Response (All Languages)

```json
{
  "uid": 123,
  "username": "username",
  "usergroup": 13,
  "provider_salt": "some_salt",
  "salt_time": 1712956800
}

```

---

## 🎯 Supported User Groups

These may differ per system, but here’s a common mapping:

| Role        | ID  |
|-------------|-----|
| Contributor | 8   |
| Respected   | 9   |
| Veteran     | 10   |
| VIP         | 11  |
| Exclusive   | 12  |
| Cosmo       | 13  |

---

## 🛠️ Developer Notes

- You'll need a `provider` and `salt` from Voided developer.
- Always use environment variables to store your API keys or salts (`.env`, `process.env`, etc.).
- Never expose secrets in public repositories!

---

## 🤝 Contributing

Want to port this to another language or framework? Fork it, make your changes, and submit a pull request! See an issue? Open one. Let's make this better together.

---


## 👥 Credits

- Original C# Implementation by **@zp8ept**
- Node.js Port by **@C00LVansh**
- Voided.to devs 💜



