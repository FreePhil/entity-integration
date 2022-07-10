using System.Runtime.InteropServices;
using Inventory.Model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repository;

public class ItemRepository: IItemRepository
{
    private InventoryContext _context;

    public ItemRepository(InventoryContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Item> GetAll()
    {
        return _context.Items.ToList();
    }

    public Item Create(Item item)
    {
        var trackedItem = _context.Items.Add(item);
        _context.SaveChanges();
        
        return trackedItem.Entity;
    }

    public Item? Get(int id)
    {
        return _context.Items.Find(id);
    }

    public bool Update(Item item)
    {
        var trackedItem = _context.Items.Find(item.Id);
        if (trackedItem == null)
        {
            return false;
        }
        _context.Entry(trackedItem).CurrentValues.SetValues(item);
        _context.SaveChanges();

        return true;
    }

    public bool Delete(int itemId)
    {
        var item = new Item() {Id = itemId};
        _context.Remove(item);
        return _context.SaveChanges() == 1;
    }
}