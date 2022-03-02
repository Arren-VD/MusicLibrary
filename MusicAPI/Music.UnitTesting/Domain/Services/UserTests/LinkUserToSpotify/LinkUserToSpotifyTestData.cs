using Music.Models;
using Music.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.UnitTesting.Domain.Services.UserTests.LinkUserToSpotify
{
    public class LinkUserToSpotifyTestData : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> LinkUserToExternalAPISpotifyTest()
        {
            yield return new object[]
            {
                new CancellationToken(),
                1,
                new List<UserTokenDTO>{new UserTokenDTO{ Name = "Spotify",Value = "abcd"} },
                Task.FromResult("spotifyid"),
                new UserClient {ClientId="spotifyid",ClientName ="Spotify",UserId=1 },
                Task.FromResult(new UserClient {ClientId="spotifyid",ClientName ="Spotify",Id=1,UserId=1 }),
                new UserClientDTO {ClientId="spotifyid",ClientName ="Spotify",UserId=1, Id = 1},
                new List<UserClientDTO>{new UserClientDTO {ClientId= "spotifyid", ClientName ="Spotify",UserId=1, Id = 1} },
            };
            yield return new object[]
            {
                new CancellationToken(),
                2,
                new List<UserTokenDTO>{new UserTokenDTO{ Name = "Spotify",Value = "1234"} },
                Task.FromResult("spotid1"),
                new UserClient {ClientId="spotid1",ClientName ="Spotify",UserId=2,Id = 0},
                Task.FromResult(new UserClient {ClientId="spotid1",ClientName ="Spotify",Id=2,UserId=2}),
                new UserClientDTO {ClientId="spotid1",ClientName ="Spotify",UserId=2, Id = 2},
                new List<UserClientDTO>{new UserClientDTO {ClientId="spotid1",ClientName ="Spotify",UserId=2, Id = 2} },
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
