namespace VedaConnect
{
    public class EnquiryType
    {
        public string AccountTypeCode { get; set; }
        public decimal EnquiryAmount { get; set; }
        public string CurrencyCode { get; set; }
        public bool? IsCreditReview { get; set; }
        public string RelationshipCode { get; set; }
    }
}