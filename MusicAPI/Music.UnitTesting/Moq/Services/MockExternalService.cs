using Moq;
using Music.Spotify.Domain.Contracts.Services;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Moq.Services
{
    public class MockExternalService : Mock<IExternalService>
    {
         public MockExternalService GetName(string output)
        {
            Setup(svc => svc.GetName()).Returns(output);
            return this;
        }
        public MockExternalService ReturnClientUser(string spotifyToken, ExternalUserDTO output)
        {
            Setup(svc => svc.ReturnClientUser(spotifyToken)).Returns(output);
            return this;
        }
        public MockExternalService ReturnClientUserId(string spotifyToken, string output)
        {
            Setup(svc => svc.ReturnClientUserId(spotifyToken)).Returns(output);
            return this;
        }
    }
}
