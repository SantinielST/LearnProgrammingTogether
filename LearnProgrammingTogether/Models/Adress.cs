using System.ComponentModel.DataAnnotations;

public class Adress
{
    [Key]
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
}