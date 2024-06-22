namespace Maki.Domain.Product.Models.Response;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime PublicationDate { get; set; } = DateTime.Now;
    public int Price { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Width { get; set; }
    public string Height { get; set; }
    public string Depth { get; set; }
    public string Material { get; set; }
    public int ArtisanId { get; set; }
    public string CategoryName { get; set; }
}