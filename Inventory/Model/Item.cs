using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Model;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public bool IsAvailable { get; set; }
    public int Amount { get; set; }
}