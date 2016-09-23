namespace VedaConnect
{
    public class Address
    {
        public string Property { get; set; }
        public CurrentOrPrevious? Type { get; set; }
        public string Unit { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public StreetType? StreetType { get; set; }
        public string Suburb { get; set; }
        public State? State { get; set; }
        public string Postcode { get; set; }
        public string CountryCode { get; set; }
    }
}