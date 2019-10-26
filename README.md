# Windows Credential Manager Password Change

## Usage:
`cmcp.exe <username> <password>`

### E.g.
`cmcp.exe DOMAIN\MyUser newPassword`  

This will iterate through all stored credentials, and any credential that matches the provided username will have its password changed to the provided value. 