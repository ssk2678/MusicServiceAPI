using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicDataAccess.Models;

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

    public virtual DbSet<MusicClickStatistic> MusicClickStatistics { get; set; }

    public virtual DbSet<MusicCount> MusicCounts { get; set; }

    public virtual DbSet<MusicDecade> MusicDecades { get; set; }

    public virtual DbSet<MusicLoadStatistic> MusicLoadStatistics { get; set; }

    public virtual DbSet<MusicSong> MusicSongs { get; set; }

    public virtual DbSet<SongSuggestion> SongSuggestions { get; set; }    

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

        modelBuilder.Entity<MusicClickStatistic>(entity =>
        {
            entity.HasKey(e => e.MusicClickStatId).HasName("pk_musicClickStatID");

            entity.ToTable("MusicClickStatistic");

            entity.Property(e => e.MusicClickStatId).HasColumnName("musicClickStatID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
            entity.Property(e => e.MusicClickStatArtistId).HasColumnName("musicClickStatArtistId");
            entity.Property(e => e.MusicClickStatDecadeId).HasColumnName("musicClickStatDecadeId");
            entity.Property(e => e.MusicClickStatSongId).HasColumnName("musicClickStatSongId");

            entity.HasOne(d => d.MusicClickStatArtist).WithMany(p => p.MusicClickStatistics)
                .HasForeignKey(d => d.MusicClickStatArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClickStatArtistID");

            entity.HasOne(d => d.MusicClickStatDecade).WithMany(p => p.MusicClickStatistics)
                .HasForeignKey(d => d.MusicClickStatDecadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClickStatDecadeID");

            entity.HasOne(d => d.MusicClickStatSong).WithMany(p => p.MusicClickStatistics)
                .HasForeignKey(d => d.MusicClickStatSongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClickStatSongID");
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

        modelBuilder.Entity<MusicLoadStatistic>(entity =>
        {
            entity.HasKey(e => e.MusicLoadStatId).HasName("pk_musicLoadStatID");

            entity.ToTable("MusicLoadStatistic");

            entity.Property(e => e.MusicLoadStatId).HasColumnName("musicLoadStatID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
            entity.Property(e => e.MusicLoadMusicCountId).HasColumnName("musicLoadMusicCountID");
            entity.Property(e => e.MusicLoadStatCountArtists).HasColumnName("musicLoadStatCountArtists");
            entity.Property(e => e.MusicLoadStatCountArtistsAdded).HasColumnName("musicLoadStatCountArtistsAdded");
            entity.Property(e => e.MusicLoadStatCountEighties).HasColumnName("musicLoadStatCountEighties");
            entity.Property(e => e.MusicLoadStatCountEightiesAdded).HasColumnName("musicLoadStatCountEightiesAdded");
            entity.Property(e => e.MusicLoadStatCountNineties).HasColumnName("musicLoadStatCountNineties");
            entity.Property(e => e.MusicLoadStatCountNinetiesAdded).HasColumnName("musicLoadStatCountNinetiesAdded");
            entity.Property(e => e.MusicLoadStatCountSeventies).HasColumnName("musicLoadStatCountSeventies");
            entity.Property(e => e.MusicLoadStatCountSeventiesAdded).HasColumnName("musicLoadStatCountSeventiesAdded");
            entity.Property(e => e.MusicLoadStatCountSongs).HasColumnName("musicLoadStatCountSongs");
            entity.Property(e => e.MusicLoadStatCountSongsAdded).HasColumnName("musicLoadStatCountSongsAdded");
            entity.Property(e => e.MusicLoadStatCountTwoThousandTens).HasColumnName("musicLoadStatCountTwoThousandTens");
            entity.Property(e => e.MusicLoadStatCountTwoThousandTensAdded).HasColumnName("musicLoadStatCountTwoThousandTensAdded");
            entity.Property(e => e.MusicLoadStatCountTwoThousandTwenties).HasColumnName("musicLoadStatCountTwoThousandTwenties");
            entity.Property(e => e.MusicLoadStatCountTwoThousandTwentiesAdded).HasColumnName("musicLoadStatCountTwoThousandTwentiesAdded");
            entity.Property(e => e.MusicLoadStatCountTwoThousands).HasColumnName("musicLoadStatCountTwoThousands");
            entity.Property(e => e.MusicLoadStatCountTwoThousandsAdded).HasColumnName("musicLoadStatCountTwoThousandsAdded");

            entity.HasOne(d => d.MusicLoadMusicCount).WithMany(p => p.MusicLoadStatistics)
                .HasForeignKey(d => d.MusicLoadMusicCountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MusicLoadMusicCountID");
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

        modelBuilder.Entity<SongSuggestion>(entity =>
        {
            entity.HasKey(e => e.SuggestedSongId).HasName("pk_SongSuggestion");

            entity.ToTable("SongSuggestion");

            entity.Property(e => e.SuggestedSongId).HasColumnName("suggestedSongID");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateModified");
            entity.Property(e => e.SuggestedSongArtist)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("suggestedSongArtist");
            entity.Property(e => e.SuggestedSongDecade).HasColumnName("suggestedSongDecade");
            entity.Property(e => e.SuggestedSongTitle)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("suggestedSongTitle");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
