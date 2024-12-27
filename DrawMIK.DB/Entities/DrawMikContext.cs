using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DrawMIK.DB.Entities;

public partial class DrawMikContext : DbContext
{
    public DrawMikContext()
    {
    }

    public DrawMikContext(DbContextOptions<DrawMikContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GamePlayer> GamePlayers { get; set; }

    public virtual DbSet<Line> Lines { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerLogin> PlayerLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-J0GTGC0;Database=DrawMIK;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Game__2AB897DDE7609B3B");

            entity.ToTable("Game");

            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.GameName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GamePlayer>(entity =>
        {
            entity.HasKey(e => e.GamePlayerId).HasName("PK__GamePlay__2D47DF8E7BBE4A08");

            entity.ToTable("GamePlayer");

            entity.HasOne(d => d.Game).WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__GamePlaye__GameI__4F7CD00D");

            entity.HasOne(d => d.Player).WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__GamePlaye__Playe__5070F446");
        });

        modelBuilder.Entity<Line>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__Line__2EAE65295CAD2B7F");

            entity.ToTable("Line");

            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Game).WithMany(p => p.Lines)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Line__GameId__534D60F1");

            entity.HasOne(d => d.Player).WithMany(p => p.Lines)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__Line__PlayerId__5441852A");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74C858F10907");

            entity.ToTable("Player");

            entity.Property(e => e.NombreJugador)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlayerLoginId)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PlayerLogin>(entity =>
        {
            entity.HasKey(e => e.PlayerLoginId).HasName("PK__PlayerLo__F688FF10A67A5CF5");

            entity.ToTable("PlayerLogin");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
