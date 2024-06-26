using Maki.Domain.IAM.Models.Entities;

namespace Maki.Domain.DesignRequest.Models.Entities;

public class DesignRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Characteristics { get; set; }
    public string Photo { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; } = true;

    public User User { get; set; } // Navigation property to User
}