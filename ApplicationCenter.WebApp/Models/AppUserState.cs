namespace ApplicationCenter.WebApp.Models;

public class AppUserState
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EMail { get; set; } = string.Empty;

    public bool IsKnown { get; set; } = false;
}