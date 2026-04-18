using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace VoltGearSystem.Models;

public class Laptop
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required(ErrorMessage = "Model Name is required.")]
    [Display(Name = "Model Name")]

    public string ModelName { get; set; } = null!;

    [Required(ErrorMessage = "Serial Number is required.")]
    [Display(Name = "Serial Number")]
    public string SerialNumber { get; set; } = null!;

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }
}