namespace OSIC.Server.Database;
public partial class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    public virtual DbSet<Hosting> Hostings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hosting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hosting__3214EC2754C85087");

            entity.ToTable("Hosting");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
