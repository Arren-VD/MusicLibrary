using FluentAssertions;
using Music.Domain.Services;
using Music.Models;
using Music.UnitTesting.Moq.Automapper;
using Music.UnitTesting.Moq.Repositories;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Music.UnitTesting.Domain.Services.ArtistTests.AddArtist
{
    public class AddArtistTests
    {
        [Theory]
        [MemberData(nameof(AddArtistTestData.AddArtistWorkingDataWithoutExistingArtist), MemberType = typeof(AddArtistTestData))]
        public async void CreateUserWithWorkingDataWithoutExistingArtist(ExternalArtistDTO input, int trackId, Artist mappedArtist,Task<Artist> artistOutput, TrackArtist trackArtist,Task<TrackArtist> trackArtistOutput)
        {
            var mockRepo = new MockGenericRepository().UpsertByAnyCondition(mappedArtist, artistOutput).UpsertByAnyCondition(trackArtist, trackArtistOutput);
            var mockMapper = new MockMapper().Map(input, mappedArtist);
            CancellationToken cancellationToken = new CancellationToken();
            var artistService = ServiceTestHelper.CreateGenericService<ArtistService>(new object[] { mockMapper, mockRepo });

            // Act
            var result = await artistService.AddTrackArtist(input,trackId, cancellationToken);

            // Assert
            result.Should().Be(trackArtistOutput.Result);
        }
    }
}
