using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAuction.Models.Enum;

namespace CarAuction.Models
{
    public class UniversityQuery
    {
        public string SearchPhrase { get; set; } = null!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; } = null!;
        public SortDirection SortDirection { get; set; }
    }
}
