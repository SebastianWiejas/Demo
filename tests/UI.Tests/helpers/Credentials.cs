namespace UI.Tests.helpers;

public class Credentials
{
    public string Username { get; }
    public string Password { get; }
    public bool Valid { get;}
    public Credentials(string username, string password,  bool valid)
    {
        Username = username;
        Password = password;
        Valid = valid;
    }
}