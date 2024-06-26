namespace Maki.Domain.DesignRequest.Models.Response;

public class DesignRequestResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Characteristics { get; set; }
    public string Photo { get; set; }
    public int UserId { get; set; }
}