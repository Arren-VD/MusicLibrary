using Music.Domain.ErrorHandling;
using Music.Models;
using Music.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Domain.Services.UserTests.CreateUser
{
    public class CreateUserTestData : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> CreateUserTest()
        {
            yield return new object[]
            {

                new UserCreationDTO{ Name = "Rick"},
                null,
                new User {Name = "Rick"},
                new User {Name = "Rick",Id = 1},
                new UserDTO {Name = "Rick",Id = 1},
            };
        }
        public static IEnumerable<object[]> CreateUserWithExistingUserReturnsError()
        {
            yield return new object[]
            {

                new UserCreationDTO{ Name = "Rick"},
                new User {Name = "Rick"},
                new User {Name = "Rick"},
                new User {Name = "Rick",Id = 1},
                new UserDTO {Name = "Rick",Id = 1},
            };
        }
        public static IEnumerable<object[]> CreateUserWithNameShorterThanTwoCharactersReturnsError()
        {
            yield return new object[]
            {

                new UserCreationDTO{ Name = "R"},
                null,
                new Error { Code = Error.ErrorValues.MinimumLength,Keyword = "User", Parameter = "Name", Message = "User name is below minimum length"},
            };
        }
        public static IEnumerable<object[]> CreateUserWithNameLongerThan30CharactersReturnsError()
        {
            yield return new object[]
            {

                new UserCreationDTO{ Name = "RarRarRarRarRarRarRarRarRarRarRarRarRarRarRar"},
                null,
                new Error { Code = Error.ErrorValues.MaximumLength,Keyword = "User", Parameter = "Name", Message = "User name exceeds maximum length"},
            };
        }
        public static IEnumerable<object[]> CreateUserWithEmptyNameReturnsError()
        {
            yield return new object[]
            {

                new UserCreationDTO{ Name = ""},
                null,
                new Error { Code = Error.ErrorValues.MinimumLength,Keyword = "User", Parameter = "Name", Message = "User name is below minimum length"},
                new Error { Code = Error.ErrorValues.Empty,Keyword = "User", Parameter = "Name", Message = "User name must not be empty"}
            };
        }
        public static IEnumerable<object[]> CreateUserWithNullNameReturnsError()
        {
            yield return new object[]
            {

                new UserCreationDTO{ Name = null},
                null,
                new Error { Code = Error.ErrorValues.Empty,Keyword = "User", Parameter = "Name", Message = "User name must not be empty"}
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
