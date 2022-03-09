using FluentAssertions;
using Music.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Music.UnitTesting.Domain.Services.Helpers
{
    public class PagingAndFilteringTest
    {
        [Theory]
        [InlineData(10, 2, 5)]
        [InlineData(12, 5, 3)]
        [InlineData(0, 2, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(2, 0, 1)]
        [InlineData((int)Int32.MaxValue, 5, 429496730)]
        [InlineData(5, Int32.MaxValue, 1)]
        public async void CalculatePageWithWorkingData(int totalcount, int pageSize, int expected)
        {
            // Act
            var result = PagingAndFiltering.CalculatePages(totalcount, pageSize);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(PagingAndFilteringTestData.ReturnPageWithWorkingData), MemberType = typeof(PagingAndFilteringTestData))]
        public async void ReturnPageWithWorkingData<T>(int amountOfObjects, int pageSize,int page, int expectedCount, List<int> expectedList)
        {
            List<int> objects = new List<int>();
            for (int i = 1; i < amountOfObjects + 1; i++)
            {
                objects.Add(i);
            }
            // Act
            var result = PagingAndFiltering.ReturnPage<int>(objects, pageSize,page);

            // Assert
            result.Count.Should().Be(expectedCount);
            result.Should().BeEquivalentTo(expectedList);
        }
        [Theory]
        [InlineData(5, 2, 5, 1)]
        [InlineData(4, 2, 3, 2)]
        public async void ReturnPageWithUnexistingPageReturnsLastPage<T>(int amountOfObjects, int pageSize, int page, int expectedCount)
        {
            List<int> objects = new List<int>();
            for (int i = 0; i < amountOfObjects; i++)
            {
                objects.Add(i);
            }
            // Act
            var result = PagingAndFiltering.ReturnPage<int>(objects, pageSize, page);

            // Assert
            result.Count.Should().Be(expectedCount);
        }
        [Theory]
        [InlineData(5, 7, 5, 5)]
        [InlineData(4, 9, 3, 4)]
        public async void ReturnPageWithPageSizeLargerThanListSizeReturnsAll<T>(int amountOfObjects, int pageSize, int page, int expectedCount)
        {
            List<int> objects = new List<int>();
            for (int i = 0; i < amountOfObjects; i++)
            {
                objects.Add(i);
            }
            // Act
            var result = PagingAndFiltering.ReturnPage<int>(objects, pageSize, page);

            // Assert
            result.Count.Should().Be(expectedCount);
        }
        [Theory]
        [InlineData(0, 7, 5, 0)]
        [InlineData(0, 9, 3, 0)]
        public async void ReturnPageWithNoCollectionReturnsEmptyList<T>(int amountOfObjects, int pageSize, int page, int expectedCount)
        {
            List<int> objects = new List<int>();
            for (int i = 0; i < amountOfObjects; i++)
            {
                objects.Add(i);
            }
            // Act
            var result = PagingAndFiltering.ReturnPage<int>(objects, pageSize, page);

            // Assert
            result.Count.Should().Be(expectedCount);
        }
    }
}
