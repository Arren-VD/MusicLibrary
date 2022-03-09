using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services.Helpers
{
    public class PagingWrapper<T> where T : class
    {
        public PagingWrapper(List<T> obj, int pageSize, int page)
        {
            TotalPages = PagingAndFiltering.CalculatePages(obj.Count, pageSize);
            Collection = PagingAndFiltering.ReturnPage(obj, pageSize, page);
        }
        public PagingWrapper()
        {

        }
        public List<T> Collection { get; set; }
        public int TotalPages { get; set; }
    }

}
