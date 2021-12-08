using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Helpers
{
    public class Pagination
    {
        public int PageIndex { get; set; }
        public int FirstPageIndex { get; set; }
        public int LastPageIndex { get; set; }
        public int PreviousPageIndex { get; set; }
        public int NextPageIndex { get; set; }
        public int FirstObjectIndex { get; set; }
        public List<int> PageIndexes { get; set; }

        public Pagination(IQueryable<object> query, int? pageIndex, int itemsPerPage = 6)
        {
            int numberOfObjects = query.Count();
            if (pageIndex == null)
            {
                pageIndex = 0;
            }
            PageIndex = (int)pageIndex;
            int totalPages = (int)Math.Ceiling(1.0 * numberOfObjects / itemsPerPage);
            FirstPageIndex = 0;
            LastPageIndex = totalPages - 1;
            PreviousPageIndex = PageIndex - 1;
            if (PreviousPageIndex < 0)
            {
                PreviousPageIndex = 0;
            }
            NextPageIndex = PageIndex + 1;
            if (NextPageIndex > LastPageIndex)
            {
                NextPageIndex = LastPageIndex;
            }
            PageIndexes = new List<int>();
            for (int p = 0; p <= LastPageIndex; p++)
            {
                PageIndexes.Add(p);
            }
            FirstObjectIndex = PageIndex * itemsPerPage;
        }

    }

}
