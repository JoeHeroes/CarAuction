namespace CarAuction.Models.View
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool AllDay { get; set; }
        public string BackgroundColor { get; set; }
    }
}