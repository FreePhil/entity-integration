using Inventory.Model;

namespace Inventory.Repository;

public interface IItemRepository
{
    public IEnumerable<Item> GetAll();

    public Item Create(Item item);
    public Item? Get(int id);
    public bool Update(Item item);
    public bool Delete(int  itemId);
}