namespace VedaConnect
{
    public class Address
    {
        public AddressType? Type { get; set; }
        public string Unit { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public StreetType? StreetType { get; set; }
        public string Suburb { get; set; }
        public State? State { get; set; }
        public string Postcode { get; set; }
    }
}