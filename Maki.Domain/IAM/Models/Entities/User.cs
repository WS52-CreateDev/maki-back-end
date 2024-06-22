namespace Maki.Domain.IAM.Models.Entities;

public class User
{
    public int Id { get; set; }
    public int CreatedUser { get; set; }
    public int? UpdatedUser { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; } = true;
    
    public string Username { get; set; }
    public string PasswordHashed { get; set; }
    public string Role { get; set; }
}