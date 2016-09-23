namespace VedaConnect
{
    public class EnquiryHeader
    {
        public string ClientReference { get; set; }
        public string OperatorId { get; set; }
        public string OperatorName { get; set; }
        public PermissionType PermissionType { get; set; }
        public ProductDataLevel ProductDataLevel { get; set; }
        public string[] RequestedScores { get; set; }
    }
}
