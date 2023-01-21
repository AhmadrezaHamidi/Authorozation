namespace Core.Entities;

public class UserRefreshToken
{
    //int max value => 2.147.nnn.nnn
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    //unique
    public string RefreshToken { get; set; }
    public int RefreshTokenTimeout { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsValid { get; set; }
}