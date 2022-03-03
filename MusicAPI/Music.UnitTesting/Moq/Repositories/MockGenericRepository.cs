using Moq;
using Music.Domain.Contracts.Repositories;
using Music.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Music.UnitTesting.Moq.Repositories
{
    public class MockGenericRepository : Mock<IGenericRepository>
    {
        public MockGenericRepository GetById<T>(T input, Task<T> output) where T : class
        {
            Setup(repo => repo.GetById<T>(input)).Returns(output);
            return this;
        }
        public MockGenericRepository GetAll<T>(Task<IEnumerable<T>> output) where T : class
        {
            Setup(repo => repo.GetAll<T>()).Returns(output);
            return this;
        }
        public MockGenericRepository UpsertByAnyCondition<T>(T inputObj,Task<T> Output) where T : class
        {
            Setup(repo => repo.UpsertByCondition(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<T>())).Returns(Output);
            return this;
        }
        public MockGenericRepository UpsertByCondition<T>(Expression<Func<T, bool>> inputExpression, T inputObj, Task<T> Output) where T : class
        {
            Setup(repo => repo.UpsertByCondition(inputExpression, inputObj)).Returns(Output);
            return this;
        }
        public MockGenericRepository Insert<T>(T input, Task<T> Output) where T : class
        {
            Setup(repo => repo.Insert<T>(It.Is<T>(u => u.Equals(input) || u == input))).Returns(Output);
            return this;
        }
        public MockGenericRepository InsertAny<T>(Task<T> Output) where T : class
        {
            Setup(repo => repo.Insert<T>(It.IsAny<T>())).Returns(Output);
            return this;
        }
        public MockGenericRepository UpsertRangeByCondition<T>(List<T> input) where T : class
        {
            Setup(repo => repo.UpsertRangeByCondition(input));
            return this;
        }
        public MockGenericRepository Update<T>(T input, Task<T> output) where T : class
        {
            Setup(repo => repo.Update(input)).Returns(output);
            return this;
        }
        public MockGenericRepository Delete<T>(T input) where T : class
        {
            Setup(repo => repo.Update(input));
            return this;
        }
        public MockGenericRepository FindByConditionAsync<T>(Task<T> output) where T : class
        {
            Setup(x => x.FindByConditionAsync(It.IsAny<Expression<Func<T, bool>>>())).Returns(output);
            return this;
        }
        public MockGenericRepository FindAllByConditionAsync<T>(Expression<Func<T, bool>> input, Task<List<T>> output) where T : class
        {
            Setup(repo => repo.FindAllByConditionAsync(input)).Returns(output);
            return this;
        }
    }
}
