namespace CarAuction.Models
{
    public class InfoBid
    {
        public int Id { get; set; }
        public bool BidStatus { get; set;}
        public int CurrentBid { get; set; }
        public bool SaleStatus { get; set; }
        public bool Watch { get; set; }
    }
}
