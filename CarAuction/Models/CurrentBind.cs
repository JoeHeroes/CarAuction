﻿namespace CarAuction.Models
{
    public class CurrentBind
    {
        public int Id { get; set; }


        public int UserId { get; set; }
        public int VehicleId { get; set; }

        public User UserMany { get; set; }
        public Vehicle VehicleMany { get; set; }
    }
}
