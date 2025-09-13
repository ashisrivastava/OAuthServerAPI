# OAuthServerAPI
Sample OAuth Authorization server API to support machine-to-machine API communication 

**Recommendations:**

* Store client\_id and client secret in a database table 
* client\_id and client\_secret must be unique per client 
* client\_id can be a Guid or UUID. You can generate like 

     var clientId = Guid.NewGuid().ToString("N")

* client\_secret must be long, random, and unguessable. Can be a cryptographic random string or a secure key.

  string GenerateSecret(int size = 32)
    {  
      var bytes = RandomNumberGenerator.GetBytes(size);  
      return Convert.ToBase64String(bytes);  
    }
    var clientSecret = GenerateSecret();


