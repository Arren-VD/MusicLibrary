using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public static class PagingAndFiltering
    {
        public static int CalculatePages(int totalCount, int pageSize)
        {
            return (int)Math.Ceiling((double)totalCount /(double) pageSize);
        }
        public static List<T> ReturnPage<T>(List<T> obj,int pageSize, int page )
        {
            return obj.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
    }
}
