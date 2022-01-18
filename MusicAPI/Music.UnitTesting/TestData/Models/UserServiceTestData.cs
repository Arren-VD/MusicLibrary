using Music.Models.Local;
using Music.Models.SpotifyModels;
using Music.Views.LocalDTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.TestData.Models
{
    public class UserServiceTestData : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> LinkUserToSpotifyWorkingData()
        {
            yield return new object[]
            {
                "token12345",
                1,
                new SpotifyUser {Display_Name = "S.V", Country = "Belgium",Email = "sam@test.be",Id = "SpotID1234"},
                new User {Name = "Sam Verhelst",Id = 1, SpotifyId = null},
                new User {Name = "Sam Verhelst",Id = 1, SpotifyId = "SpotID1234"},
            };
            yield return new object[]
            {
                "spotToken%598",
                2,
                new SpotifyUser {Display_Name = "Jan.DeJong", Country = "France",Email = "Jan@test.be",Id = "myid8749"},
                new User {Name = "Jan DeJong",Id = 2, SpotifyId = null},
                new User {Name = "Jan DeJong",Id = 2, SpotifyId = "myid8749"},
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
