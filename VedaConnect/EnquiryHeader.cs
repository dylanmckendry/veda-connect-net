namespace VedaConnect
{
    public class EnquiryHeader
    {
        public string ClientReference { get; set; }
        public string OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string PermissionTypeCode { get; set; }
        public string ProductDataLevelCode { get; set; }
        public string[] RequestedScores { get; set; }
    }
}
