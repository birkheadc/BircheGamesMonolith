# Authentication API

This is the API that handles authentication. A user can provide a username and password. The API then checks for a user that meets those credentials in the Users database. If found, it returns a token that can be used across multiple services to authenticate the user. If not found, the user is denied.