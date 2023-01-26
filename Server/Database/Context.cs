using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OSIC.Server.Database;

public partial class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Binding> Bindings { get; set; }

    public virtual DbSet<BindingAuthentication> BindingAuthentications { get; set; }

    public virtual DbSet<BindingConnection> BindingConnections { get; set; }

    public virtual DbSet<BindingSecurity> BindingSecurities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Binding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Binding__3214EC2789A4847D");

            entity.ToTable("Binding");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Expires).HasColumnType("datetime");
            entity.Property(e => e.UnitTwoLetterIsolanguageName).HasColumnName("UnitTwoLetterISOLanguageName");
            entity.Property(e => e.UnitTwoLetterIsoregionName).HasColumnName("UnitTwoLetterISORegionName");
        });

        modelBuilder.Entity<BindingAuthentication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BindingA__3214EC278EB9DD86");

            entity.ToTable("BindingAuthentication");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BindingId).HasColumnName("Binding.ID");

            entity.HasOne(d => d.Binding).WithMany(p => p.BindingAuthentications)
                .HasForeignKey(d => d.BindingId)
                .HasConstraintName("FK_BindingAuthentication_ToBinding");
        });

        modelBuilder.Entity<BindingConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BindingC__3214EC270911117E");

            entity.ToTable("BindingConnection");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BindingId).HasColumnName("Binding.ID");
            entity.Property(e => e.SignalrConnectionId).HasColumnName("SignalrConnectionID");

            entity.HasOne(d => d.Binding).WithMany(p => p.BindingConnections)
                .HasForeignKey(d => d.BindingId)
                .HasConstraintName("FK_BindingConnection_ToBinding");
        });

        modelBuilder.Entity<BindingSecurity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC272AE4DA9B");

            entity.ToTable("BindingSecurity");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BindingId).HasColumnName("Binding.ID");
            entity.Property(e => e.Created).HasColumnType("datetime");

            entity.HasOne(d => d.Binding).WithMany(p => p.BindingSecurities)
                .HasForeignKey(d => d.BindingId)
                .HasConstraintName("FK_BindingSecurity_ToBinding");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
