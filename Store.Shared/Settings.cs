namespace Store.Shared;
public static class Settings
{
    public static string? ConnectionString = @$"
        Server=url,port;
        Database=db;
        User ID=user;
        Password=pass;
        TrustServerCertificate=True";
}
