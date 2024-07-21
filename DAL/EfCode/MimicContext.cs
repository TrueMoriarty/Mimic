using DAL.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace DAL.EfCode;

public class MimicContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<RoomStorageRelation> RoomStorageRelation { get; set; }
    public DbSet<Properties> Properties { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<User> Users { get; set; }

    public MimicContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Mimicdb;Username=pgadmin;Password=pgadmin");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasMany(t => t.Properties)
            .WithOne()
            .HasForeignKey(t => t.ItemId);

        modelBuilder.Entity<User>()
            .HasMany(t => t.Items)
            .WithOne(t => t.Creator)
            .HasForeignKey(t => t.CreatorId);

        modelBuilder.Entity<User>()
            .HasMany(t => t.Rooms)
            .WithOne(t => t.Master)
            .HasForeignKey(t => t.MasterId);

        modelBuilder.Entity<User>()
            .HasMany(t => t.Characters)
            .WithOne(t => t.Creator)
            .HasForeignKey(t => t.CreatorId);

        modelBuilder.Entity<Room>()
            .HasMany(t => t.RoomStorageRelations)
            .WithOne(t => t.Room)
            .HasForeignKey(t => t.RoomId);

        modelBuilder.Entity<Room>()
            .HasMany(t => t.Characters)
            .WithOne(t => t.Room)
            .HasForeignKey(t => t.RoomId);

        modelBuilder.Entity<Storage>()
            .HasMany(t => t.Items)
            .WithOne(t => t.Storage)
            .HasForeignKey(t => t.StorageId);

        modelBuilder.Entity<Storage>()
            .HasMany(t => t.RoomStorageRelations)
            .WithOne(t => t.Storage)
            .HasForeignKey(t => t.StorageId);

        modelBuilder.Entity<Item>().Property(t => t.ItemId).UseIdentityAlwaysColumn();

        modelBuilder.Entity<User>().Property(t => t.UserId).UseIdentityAlwaysColumn();

        modelBuilder.Entity<Room>().Property(t => t.RoomId).UseIdentityAlwaysColumn();

        modelBuilder.Entity<Character>().Property(t => t.CharacterId).UseIdentityAlwaysColumn();

        modelBuilder.Entity<Properties>().Property(t => t.PropertiesId).UseIdentityAlwaysColumn();

        modelBuilder.Entity<Storage>().Property(t => t.StorageId).UseIdentityAlwaysColumn();

        modelBuilder.Entity<RoomStorageRelation>().Property(t => t.RoomStorageRelationId).UseIdentityAlwaysColumn();

        base.OnModelCreating(modelBuilder);
    }
}
