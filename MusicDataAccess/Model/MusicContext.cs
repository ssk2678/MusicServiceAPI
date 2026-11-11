using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicDataAccess.Model;

public partial class MusicContext : DbContext
{
    public MusicContext()
    {
    }

    public MusicContext(DbContextOptions<MusicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MusicArtist> MusicArtists { get; set; }

    public virtual DbSet<MusicArtistDecade> MusicArtistDecades { get; set; }

    public virtual DbSet<MusicCount> MusicCounts { get; set; }

    public virtual DbSet<MusicDecade> MusicDecades { get; set; }

    public virtual DbSet<MusicSong> MusicSongs { get; set; }

    public virtual DbSet<MusicStatistic> MusicStatistics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MusicArtist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("pk_Artist");

            entity.ToTable("MusicArtist");

            entity.Property(e => e.ArtistId).HasColumnName("artistID");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("artistName");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
        });

        modelBuilder.Entity<MusicArtistDecade>(entity =>
        {
            entity.HasKey(e => e.ArtistDecadeId).HasName("pk_ArtistDecade");

            entity.ToTable("MusicArtistDecade");

            entity.Property(e => e.ArtistDecadeId).HasColumnName("artistDecadeID");
            entity.Property(e => e.ArtistId).HasColumnName("artistID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
            entity.Property(e => e.DecadeId).HasColumnName("decadeID");
        });

        modelBuilder.Entity<MusicCount>(entity =>
        {
            entity.HasKey(e => e.MusicCountId).HasName("pk_MusicCount");

            entity.ToTable("MusicCount");

            entity.Property(e => e.MusicCountId).HasColumnName("musicCountID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
            entity.Property(e => e.TotalArtists).HasColumnName("totalArtists");
            entity.Property(e => e.TotalEightiesSongs).HasColumnName("totalEightiesSongs");
            entity.Property(e => e.TotalNinetiesSongs).HasColumnName("totalNinetiesSongs");
            entity.Property(e => e.TotalSeventiesSongs).HasColumnName("totalSeventiesSongs");
            entity.Property(e => e.TotalSongs).HasColumnName("totalSongs");
            entity.Property(e => e.TotalTwoThousandTensSongs).HasColumnName("totalTwoThousandTensSongs");
            entity.Property(e => e.TotalTwoThousandTwentiesSongs).HasColumnName("totalTwoThousandTwentiesSongs");
            entity.Property(e => e.TotalTwoThousandsSongs).HasColumnName("totalTwoThousandsSongs");
        });

        modelBuilder.Entity<MusicDecade>(entity =>
        {
            entity.HasKey(e => e.DecadeId).HasName("pk_Decade");

            entity.ToTable("MusicDecade");

            entity.Property(e => e.DecadeId).HasColumnName("decadeID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
            entity.Property(e => e.DecadeName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("decadeName");
        });

        modelBuilder.Entity<MusicSong>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("pk_Song");

            entity.ToTable("MusicSong");

            entity.Property(e => e.SongId).HasColumnName("songID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
            entity.Property(e => e.SongArtistId).HasColumnName("songArtistID");
            entity.Property(e => e.SongDecadeId).HasColumnName("songDecadeID");
            entity.Property(e => e.SongName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("songName");

            entity.HasOne(d => d.SongArtist).WithMany(p => p.MusicSongs)
                .HasForeignKey(d => d.SongArtistId)
                .HasConstraintName("fk_SongArtistID");

            entity.HasOne(d => d.SongDecade).WithMany(p => p.MusicSongs)
                .HasForeignKey(d => d.SongDecadeId)
                .HasConstraintName("fk_SongCategoryID");
        });

        modelBuilder.Entity<MusicStatistic>(entity =>
        {
            entity.HasKey(e => e.MusicStatId).HasName("pk_Stat");

            entity.ToTable("MusicStatistic");

            entity.Property(e => e.MusicStatId).HasColumnName("musicStatID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
            entity.Property(e => e.MusicStatArtistId).HasColumnName("musicStatArtistID");
            entity.Property(e => e.MusicStatDecadeId).HasColumnName("musicStatDecadeID");
            entity.Property(e => e.MusicStatSongId).HasColumnName("musicStatSongID");

            entity.HasOne(d => d.MusicStatArtist).WithMany(p => p.MusicStatistics)
                .HasForeignKey(d => d.MusicStatArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_StatArtistID");

            entity.HasOne(d => d.MusicStatDecade).WithMany(p => p.MusicStatistics)
                .HasForeignKey(d => d.MusicStatDecadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_StatDecadeID");

            entity.HasOne(d => d.MusicStatSong).WithMany(p => p.MusicStatistics)
                .HasForeignKey(d => d.MusicStatSongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_StatSongID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
