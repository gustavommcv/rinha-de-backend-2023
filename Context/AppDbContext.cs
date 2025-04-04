using Microsoft.EntityFrameworkCore;
using rinha_de_backend_2023.Entities;

namespace rinha_de_backend_2023.Context;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Person>()
            .HasIndex(p => p.Apelido)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
