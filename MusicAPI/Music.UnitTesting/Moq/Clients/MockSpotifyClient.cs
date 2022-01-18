using Moq;
using Music.Domain.Contracts.Clients;
using Music.Domain.Contracts.Repositories;
using Music.Models.Local;
using Music.Models.SpotifyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Moq.Clients
{
    public class MockSpotifyClient : Mock<ISpotifyClient>
    {
        public MockSpotifyClient GetCurrentSpotifyUser(string authToken, SpotifyUser Output)
        {
            Setup(repo => repo.GetCurrentSpotifyUser(authToken)).ReturnsAsync(Output);
            return this;
        }
    }
}
