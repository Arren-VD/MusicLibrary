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
    public class PagingAndFilteringTestData
    {
        public static IEnumerable<object[]> ReturnPageWithWorkingData()
        {
            yield return new object[]
            {
                5,2,3,1,new List<int>(){5},
            };
            yield return new object[]
            {
                5,5,1,5,new List<int>(){1,2,3,4,5},
            };
            yield return new object[]
            {
                5,3,1,3,new List<int>(){1,2,3},
            };
            yield return new object[]
            {
                4,2,2,2,new List<int>(){3,4},
            };
            yield return new object[]
            {
                4,3,2,1,new List<int>(){4},
            };
            yield return new object[]
            {
                9,3,2,3,new List<int>(){4,5,6},
            };
        }
    }
}
