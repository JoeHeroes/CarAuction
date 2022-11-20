using CarAuction.Models.Enum;

namespace CarAuction.Models
{
    public class AuctionQuery
    {
        public Producer Producer { get; set; } = Producer.none;
        public string LocationName { get; set; } = null;
        public int RegistrationYear { get; set; }
        public int SinceYear { get; set; } = 2000;
        public int ToYear { get; set; } = 2020;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string SortBy { get; set; } = null!;
        public SortDirection SortDirection { get; set; }





    }
}
