using Inventory.Model;
using Microsoft.EntityFrameworkCore;

namespace Inventory;

public class InventoryContext: DbContext
{
    public InventoryContext() {}
    
    public InventoryContext(DbContextOptions<InventoryContext> options): base(options) {}

    public DbSet<Item> Items { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>().ToTable("items");
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.IsAvailable).HasColumnName("is_available");
            entity.Property(e => e.Amount).HasColumnName("amount");
        });
    }
}