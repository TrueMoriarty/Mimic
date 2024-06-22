using Microsoft.EntityFrameworkCore;
using MimicWebApi.Models;

namespace MimicWebApi.PostgreSQL;

public class MimicContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<RoomStorageRelation> CharacterRoomRelations { get; set; }
    public DbSet<Properties> Properties { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<User> Users { get; set; }

    public MimicContext(DbContextOptions<MimicContext> options) : base(options) 
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Port=5423;Database=Mimicdb;Username=pgadmin;Password=pgadmin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasMany(t => t.Properties)
            .WithOne(t => t.Item)
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

        modelBuilder.Entity<Item>().Property(t => t.Id).UseIdentityAlwaysColumn();

        modelBuilder.Entity<User>().Property(t => t.Id).UseIdentityAlwaysColumn();

        modelBuilder.Entity<Room>().Property(t => t.Id).UseIdentityAlwaysColumn();

        modelBuilder.Entity<Character>().Property(t => t.Id).UseIdentityAlwaysColumn();

        modelBuilder.Entity<Properties>().Property(t => t.Id).UseIdentityAlwaysColumn();

        modelBuilder.Entity<Storage>().Property(t => t.Id).UseIdentityAlwaysColumn();

        base.OnModelCreating(modelBuilder);
    }
}
