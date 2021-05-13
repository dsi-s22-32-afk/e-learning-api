using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Models.Responses
{
    public class PaginatedListResponse<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int PagesTotal { get; set; }

        public T[] Data { get; set; }
    }
}
