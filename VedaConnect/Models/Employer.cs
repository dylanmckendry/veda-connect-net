namespace VedaConnect
{
    public class Employer
    {
        public string Name { get; set; }
        public CurrentOrPrevious? Type { get; set; }
        public string Occupation { get; set; }
        public bool? IsSelfEmployed { get; set; }
        public string Abn { get; set; }
        public string OrganisationNumber { get; set; }
        public string CountryCode { get; set; }
    }
}
