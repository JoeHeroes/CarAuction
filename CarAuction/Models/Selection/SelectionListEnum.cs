namespace CarAuction.Models.Selection
{
    public static class SelectionListEnum
    {
        public static List<ClassEnum> GetAllYears()
        {
            return new List<ClassEnum>
            {
                new ClassEnum() { Id="0", Name="Select Year"},
                new ClassEnum() { Id="2022", Name="2022"},
                new ClassEnum() { Id="2021", Name="2021"},
                new ClassEnum() { Id="2020", Name="2020"},
                new ClassEnum() { Id="2019", Name="2019"},
                new ClassEnum() { Id="2018", Name="2018"},
                new ClassEnum() { Id="2017", Name="2017"},
                new ClassEnum() { Id="2016", Name="2016"},
                new ClassEnum() { Id="2015", Name="2015"},
                new ClassEnum() { Id="2014", Name="2014"},
                new ClassEnum() { Id="2013", Name="2013"},
                new ClassEnum() { Id="2012", Name="2012"},
                new ClassEnum() { Id="2011", Name="2011"},
                new ClassEnum() { Id="2010", Name="2010"},
                new ClassEnum() { Id="2009", Name="2009"},
                new ClassEnum() { Id="2008", Name="2008"},
                new ClassEnum() { Id="2007", Name="2007"},
                new ClassEnum() { Id="2006", Name="2006"},
                new ClassEnum() { Id="2005", Name="2005"},
                new ClassEnum() { Id="2004", Name="2004"},
                new ClassEnum() { Id="2003", Name="2003"},
                new ClassEnum() { Id="2002", Name="2002"},
                new ClassEnum() { Id="2001", Name="2001"},
                new ClassEnum() { Id="2000", Name="2000"},
            };
        }



        public static List<ClassEnum> GetAllLocations()
        {
            return new List<ClassEnum>
            {
                new ClassEnum() { Id="none", Name="Select Location"},
                new ClassEnum() { Id="Espoo", Name="Espoo"},
                new ClassEnum() { Id="Oulu", Name="Oulu"},
                new ClassEnum() { Id="Pirkkala", Name="Pirkkala"},
                new ClassEnum() { Id="Turku", Name="Turku"},
            };
        }

        public static List<ClassEnum> GetAllBodyTypes()
        {
            return new List<ClassEnum>
            {
                new ClassEnum() { Id="none", Name="Select Body"},
                new ClassEnum() { Id="Micro", Name="Micro"},
                new ClassEnum() { Id="Sedan", Name="Sedan"},
                new ClassEnum() { Id="Liftback", Name="Liftback"},
                new ClassEnum() { Id="Combi", Name="Combi"},
                new ClassEnum() { Id="Coupe", Name="Coupe"},
                new ClassEnum() { Id="Hatchback", Name="Hatchback"},
                new ClassEnum() { Id="Van", Name="Van"},
                new ClassEnum() { Id="SUV", Name="SUV"},
                new ClassEnum() { Id="Pickup", Name="Pickup"},
                new ClassEnum() { Id="Cabrio", Name="Cabrio"},
            };
        }

        public static List<ClassEnum> GetAllDamages()
        {
            return new List<ClassEnum>
            {
                new ClassEnum() { Id="none", Name="Select Damages"},
                new ClassEnum() { Id="AllOver", Name="All Over"},
                new ClassEnum() { Id="Burn", Name="Burn"},
                new ClassEnum() { Id="BurnEngine", Name="Burn Engine"},
                new ClassEnum() { Id="FrontEnd", Name="Front End"},
                new ClassEnum() { Id="RearEnd", Name="Rear End"},
                new ClassEnum() { Id="Hail", Name="Hail"},
                new ClassEnum() { Id="Mechanical", Name="Mechanical"},
                new ClassEnum() { Id="MinorDentsScratch", Name="Minor Dents Scratch"},
                new ClassEnum() { Id="NormalWear", Name="Normal Wear"},
                new ClassEnum() { Id="Rollover", Name="Rollover"},
                new ClassEnum() { Id="Side", Name="Side"},
                new ClassEnum() { Id="TopRoof", Name="Top Roof"},
                new ClassEnum() { Id="Undercarriage", Name="Undercarriage"},
                new ClassEnum() { Id="Vandalism", Name="Vandalism"},
                new ClassEnum() { Id="Unknown", Name="Unknown"},
            };
        }

        public static List<ClassEnum> GetAllDrives()
        {
            return new List<ClassEnum>
            {
                new ClassEnum() { Id="none", Name="Select Drives"},
                new ClassEnum() { Id="AWD", Name="AWD"},
                new ClassEnum() { Id="FWD", Name="FWD"},
                new ClassEnum() { Id="RWD", Name="RWD"},
            };
        }



        public static List<ClassEnum> GetAllTransmissions()
        {
            return new List<ClassEnum>
            {
                new ClassEnum() { Id="none", Name="Select Transmission"},
                new ClassEnum() { Id="Manual", Name="Manual"},
                new ClassEnum() { Id="Automatic", Name="Automatic"},
                new ClassEnum() { Id="DualClutch", Name="DualClutch"},
                new ClassEnum() { Id="CVT", Name="CVT"},
            };
        }

        public static List<ClassEnum> GetAllFuels()
        {
            return new List<ClassEnum>
            {
                new ClassEnum() { Id="none", Name="Select Fuel"},
                new ClassEnum() { Id="Diesel", Name="Diesel"},
                new ClassEnum() { Id="Petrol", Name="Petrol"},
                new ClassEnum() { Id="Gas", Name="Gas"},
                new ClassEnum() { Id="Hybrid", Name="Hybrid"},
                new ClassEnum() { Id="Electric", Name="Electric"},
            };
        }

        public static List<ClassEnum> GetAllProducers()
        {
            return new List<ClassEnum>
            {
                new ClassEnum() { Id="none", Name="Select Producer"},
                new ClassEnum() { Id="Alfa Romeo", Name="Alfa Romeo"},
                new ClassEnum() { Id="Audi", Name="Audi"},
                new ClassEnum() { Id="BMW", Name="BMW"},
                new ClassEnum() { Id="Cupra", Name="Cupra"},
                new ClassEnum() { Id="Chevrolet", Name="Chevrolet"},
                new ClassEnum() { Id="Citroen", Name="Citroen"},
                new ClassEnum() { Id="Dacia", Name="Dacia"},
                new ClassEnum() { Id="Dodge", Name="Dodge"},
                new ClassEnum() { Id="FIAT", Name="FIAT"},
                new ClassEnum() { Id="Ford", Name="Ford"},
                new ClassEnum() { Id="Honda", Name="Honda"},
                new ClassEnum() { Id="Hyundai", Name="Hyundai"},
                new ClassEnum() { Id="Kia", Name="Kia"},
                new ClassEnum() { Id="Land Rover", Name="Land Rover"},
                new ClassEnum() { Id="Jeep", Name="Jeep"},
                new ClassEnum() { Id="Lexus", Name="Lexus"},
                new ClassEnum() { Id="Mazda", Name="Mazda"},
                new ClassEnum() { Id="Mercedes", Name="Mercedes"},
                new ClassEnum() { Id="Mini", Name="Mini"},
                new ClassEnum() { Id="Mitsubishi", Name="Mitsubishi"},
                new ClassEnum() { Id="Nissan", Name="Nissan"},
                new ClassEnum() { Id="Opel", Name="Opel"},
                new ClassEnum() { Id="Peugeot", Name="Peugeot"},
                new ClassEnum() { Id="Renault", Name="Renault"},
                new ClassEnum() { Id="Seat", Name="Seat"},
                new ClassEnum() { Id="Skoda", Name="Skoda"},
                new ClassEnum() { Id="Subaru", Name="Subaru"},
                new ClassEnum() { Id="Tesla", Name="Tesla"},
                new ClassEnum() { Id="Toyota", Name="Toyota"},
                new ClassEnum() { Id="Volkswagen", Name="Volkswagen"},
                new ClassEnum() { Id="Volvo", Name="Volvo"},
            };
        }
    }
}
 