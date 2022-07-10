using Inventory;
using Inventory.Model;
using Inventory.Repository;
using Microsoft.EntityFrameworkCore;

namespace InventoryIntegrationTest;

[TestFixture]
public class ItemTest
{
    private InventoryContext _context;

    public InventoryContext CreateTestContext()
    {
        var sqlVersion = new MariaDbServerVersion(new Version(10, 6));
        var connectionString = "server=localhost;user=root;password=superman;database=integration-test";
        
        var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>()
            .UseMySql(connectionString, sqlVersion);

        return new InventoryContext(optionsBuilder.Options);
    }

    public ItemTest()
    {
        _context = CreateTestContext();
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Test]
    public void TestCreation()
    {
        // arrange
        //
        IItemRepository repository = new ItemRepository(_context);

        var item = new Item
        {
            Name = "Item1",
            IsAvailable = true,
            Amount = 10
        };

        // act
        //
        var createdItem = repository.Create(item);

        // assert
        //
        Assert.That(createdItem.Id, Is.Not.Zero);
        Assert.That(createdItem.Name, Is.EqualTo(item.Name));
        Assert.That(createdItem.IsAvailable, Is.EqualTo(item.IsAvailable));
        Assert.That(createdItem.Amount, Is.EqualTo(item.Amount));
    }
    
    private int CreateArbitraryItem(string option)
    {
        IItemRepository repository = new ItemRepository(_context);

        var item = new Item
        {
            Name = $"Item-{option}",
            IsAvailable = true,
            Amount = 10
        };

        var createdItem = repository.Create(item);

        return createdItem.Id;
    }

    [Test]
    public void TestRetrieve()
    {
        // arrange
        //
        IItemRepository repository = new ItemRepository(_context);
        int createdId = CreateArbitraryItem("Retrieve");

        // act
        //
        Item item = repository.Get(createdId)!;

        // assert
        //
        Assert.That(item.Id, Is.EqualTo(createdId));
        Assert.That(item.Name, Is.EqualTo("Item-Retrieve"));
        Assert.That(item.IsAvailable, Is.EqualTo(true));
        Assert.That(item.Amount, Is.EqualTo(10));       

    }

    [Test]
    public void TestUpdate()
    {
        // arrange
        //
        IItemRepository repository = new ItemRepository(_context);
        int createdId = CreateArbitraryItem("Update");
        Item targetItem = repository.Get(createdId)!;

        targetItem.Name = "New Updated";
        targetItem.IsAvailable = false;

        // act
        //
        bool result = repository.Update(targetItem);
        
        // assert
        //
        Item item = repository.Get(targetItem.Id)!;
        Assert.That(item.Name, Is.EqualTo("New Updated"));
        Assert.That(item.IsAvailable, Is.EqualTo(false));
        Assert.That(item.Amount, Is.EqualTo(10));
    }

    [Test]
    public void TestDeletion()
    {
        // arrange
        //
        IItemRepository repository = new ItemRepository(_context);
        int createdId = CreateArbitraryItem("DoNotCare");
        Item targetItem = repository.Get(createdId)!;
        _context.ChangeTracker.Clear();

        // act
        //
        bool result = repository.Delete(targetItem.Id);
        Item? dummyItem = repository.Get(targetItem.Id);
        
        // assert
        //
        Assert.That(result, Is.EqualTo(true));
        Assert.That(dummyItem, Is.Null);
    }
}