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
            if (totalCount == 0)
                return 0;
            pageSize = (pageSize == 0) ? totalCount : pageSize;
            return (int)Math.Ceiling((double)totalCount /(double) pageSize);
        }
        public static List<T> ReturnPage<T>(List<T> obj,int pageSize, int page )
        {
            int maxPagesize = CalculatePages(obj.Count, pageSize);
            maxPagesize = (maxPagesize < page) ? maxPagesize : page;
            return obj.Skip(pageSize * (Math.Min(page, maxPagesize) - 1)).Take(pageSize).ToList();
        }
    }
}
