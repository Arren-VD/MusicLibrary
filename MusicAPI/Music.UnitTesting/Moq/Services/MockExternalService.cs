using Moq;
using Music.Domain.Contracts.Services;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public MockExternalService ReturnClientUser(CancellationToken cancellationToken,string spotifyToken, Task<ExternalUserDTO> output)
        {
            Setup(svc => svc.ReturnClientUser( spotifyToken, cancellationToken)).Returns(output);
            return this;
        }
        public MockExternalService ReturnClientUserId(CancellationToken cancellationToken,string spotifyToken, Task<string> output)
        {
            Setup(svc => svc.ReturnClientUserId(spotifyToken, cancellationToken)).Returns(output);
            return this;
        }
    }
}
