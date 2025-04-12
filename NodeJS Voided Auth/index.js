 const { AuthManager, Usergroup } = require('./auth.js');
(async () => {
    const manager = new AuthManager();
  
    const userKey = 'KEY_HERE';
    const provider = 'PROVIDER'; //Ask Voided developers if you need one.
    const requiredGroup = Usergroup.Cosmo; 
  
    const authResult = await manager.authenticateAsync(userKey, provider, requiredGroup);
    if(authResult.authenticated) {
    //your code
    } 
  })();
