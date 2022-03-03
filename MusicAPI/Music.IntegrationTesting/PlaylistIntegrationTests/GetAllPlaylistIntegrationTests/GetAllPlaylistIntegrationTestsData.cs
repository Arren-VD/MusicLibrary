using Music.Models;
using Music.Views;
using Music.Views.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.IntegrationTesting.PlaylistIntegrationTests.GetAllPlaylistIntegrationTests
{
    public class GetAllPlaylistIntegrationTestsData
    {
        public static IEnumerable<object[]> GetAllSeededDBWithOnlyUserData()
        {
            yield return new object[]
            {
                1,
                new List<Playlist>
                {
                new Playlist(){Id = 1, Name = "Jazz",UserId = 1},
                new Playlist(){Id = 2, Name = "Pop",UserId = 1},
                new Playlist(){Id = 3, Name = "Techno",UserId = 1}
                },
                new List<PlaylistResult>
                {
                new PlaylistResult(){Id = 1, Name = "Jazz"},
                new PlaylistResult(){Id = 2, Name = "Pop"},
                new PlaylistResult(){Id = 3, Name = "Techno"}
                }
            };
        }
        public static IEnumerable<object[]> GetAllSeededDBWithDifferentUserData()
        {
            yield return new object[]
            {
                1,
                new List<Playlist>
                {
                new Playlist(){Id = 1, Name = "Jazz",UserId = 1},
                new Playlist(){Id = 2, Name = "Pop",UserId = 1},
                new Playlist(){Id = 3, Name = "Techno",UserId = 1}
                },
                new List<Playlist>
                {
                new Playlist(){Id = 4, Name = "New wave",UserId = 2},
                new Playlist(){Id = 5, Name = "Synth",UserId = 3}
                },
                new List<PlaylistResult>
                {
                new PlaylistResult(){Id = 1, Name = "Jazz"},
                new PlaylistResult(){Id = 2, Name = "Pop"},
                new PlaylistResult(){Id = 3, Name = "Techno"}
                }
            };
            yield return new object[]
           {
                1,
                new List<Playlist>
                {
                    new Playlist(){Id = 1, Name = "Jazz",UserId = 1},
                    new Playlist(){Id = 2, Name = "Pop",UserId = 1},
                    new Playlist(){Id = 3, Name = "Techno",UserId = 1},
                    new Playlist(){Id = 4, Name = "Indie",UserId = 1}
                },
                new List<Playlist>
                {
                    new Playlist(){Id = 5, Name = "New wave",UserId = 2},
                    new Playlist(){Id = 6, Name = "Synth",UserId = 3},
                    new Playlist(){Id = 7, Name = "80s",UserId = 3}
                },
                new List<PlaylistResult>
                {
                    new PlaylistResult(){Id = 1, Name = "Jazz"},
                    new PlaylistResult(){Id = 2, Name = "Pop"},
                    new PlaylistResult(){Id = 3, Name = "Techno"},
                    new PlaylistResult(){Id = 4, Name = "Indie"}
                }
           };
        }
    }
}
