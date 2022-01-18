using Microsoft.EntityFrameworkCore;
using Music.Models;
using Music.Models.Local;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.DataAccess.Database
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options)
                            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<PlaylistSong> PlaylistSong { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<SongArtist> SongArtist { get; set; }
        public DbSet<SongGenre> SongGenre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

