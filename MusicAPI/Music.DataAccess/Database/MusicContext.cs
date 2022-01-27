using Microsoft.EntityFrameworkCore;
using Music.Models;
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
        public DbSet<UserClient> UserClients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<TrackPlaylist> TrackPlaylists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackArtist> TrackArtists { get; set; }
        public DbSet<TrackGenre> TrackGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

