﻿using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Repositories
{
    public interface ITrueGenericRepository
    {
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<T> GetById<T>(object id) where T : class;
        Task<T> Insert<T>(T obj) where T : class;
        Task Update<T>(T obj) where T : class;
        Task Delete<T>(object id) where T : class;
        Task Save();
        Task<T> FindByConditionAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}
