namespace Maki.Domain.DesignRequest.Models.Commands;

public class UpdateDesignRequestCommand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Characteristics { get; set; }
    public string Photo { get; set; }
    public int UserId { get; set; }
}