using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Domain.Services.ArtistTests.AddArtist
{
    public class AddArtistTestData
    {
        public static IEnumerable<object[]> AddArtistWorkingDataWithoutExistingArtist()
        {
            yield return new object[]
            {
                new ExternalArtistDTO(){Id = "SpotifyId5176", Name = "Elvis"},
                1,
                new Artist(){Id = 0, Name = "Elvis", ClientId = "SpotifyId5176"},
                Task.FromResult(new Artist(){Id = 1, Name = "Elvis", ClientId = "SpotifyId5176"}),
                new TrackArtist(1,1),
                Task.FromResult(new TrackArtist(){Id = 1, ArtistId = 1, TrackId = 1, Artist = new Artist{ Id = 1, ClientId = "SpotifyId5176", Name = "Elvis"} }),
            };
            yield return new object[]
            {
                new ExternalArtistDTO(){Id = "SoundCloud55", Name = "Billy"},
                1,
                new Artist(){Id = 0, Name = "Billy", ClientId = "SoundCloud55"},
                Task.FromResult(new Artist(){Id = 5, Name = "Billy", ClientId = "SoundCloud55"}),
                new TrackArtist(1,5),
                Task.FromResult(new TrackArtist(){Id = 1, ArtistId = 5, TrackId = 1, Artist = new Artist{ Id = 5, ClientId = "SoundCloud55", Name = "Billy"} }),
            };
            yield return new object[]
{
                new ExternalArtistDTO(){Id = "YT_ID", Name = "Tony"},
                1,
                new Artist(){Id = 0, Name = "Tony", ClientId = "YT_ID"},
                Task.FromResult(new Artist(){Id = 1, Name = "Tony", ClientId = "YT_ID"}),
                new TrackArtist(1,1),
                Task.FromResult(new TrackArtist(){Id = 3, ArtistId = 3, TrackId = 1, Artist = new Artist{ Id = 1, ClientId = "SpotifyId5176", Name = "Elvis"} }),
};
        }
    }
}
