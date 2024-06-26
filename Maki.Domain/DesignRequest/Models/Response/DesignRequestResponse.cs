namespace Maki.Domain.DesignRequest.Models.Response;

public class DesignRequestResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Characteristics { get; set; }
    public string Photo { get; set; }
    public string Email { get; set; } // Nuevo campo
    public int ArtisanId { get; set; } // Nuevo campo
}