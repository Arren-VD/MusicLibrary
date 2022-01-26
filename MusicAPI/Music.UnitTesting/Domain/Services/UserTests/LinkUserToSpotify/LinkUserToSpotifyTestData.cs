using Music.Models;
using Music.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Domain.Services.UserTests.LinkUserToSpotify
{
    public class LinkUserToSpotifyTestData : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> LinkUserToExternalAPISpotifyTest()
        {
            yield return new object[]
            {

                1,
                new List<UserTokenDTO>{new UserTokenDTO{ Name = "Spotify",Value = "abcd"} },
                "spotifyid",
                new List<UserClient>{new UserClient {ClientId="spotifyid",ClientName ="Spotify",UserId=1 } },
                new UserClient {ClientId="spotifyid",ClientName ="Spotify",Id=1,UserId=1 }
            };
            yield return new object[]
            {
                2,
                new List<UserTokenDTO>{new UserTokenDTO{ Name = "Spotify",Value = "1234"} },
                "spotid1",
                new List<UserClient>{new UserClient {ClientId="spotid1",ClientName ="Spotify",UserId=2} },
                new UserClient {ClientId="spotid1",ClientName ="Spotify",Id=2,UserId=2}
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
