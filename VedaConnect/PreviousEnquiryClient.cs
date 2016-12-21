using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VedaConnect.PreviousEnquiry;

namespace VedaConnect
{
    public class PreviousEnquiryClient
    {
        private previousEnquiryPortTypeClient _client;

        public PreviousEnquiryClient(string url, string username, string password)
        {
            _client = ClientFactory.Create<previousEnquiryPortTypeClient, previousEnquiryPortType>(url, username, password);
        }

        public async Task<PreviousEnquiryResult> GetPreviousEnquiryAsync(string enquiryId, PreviousEnquiryContentType type)
        {
            var contentType = GetContentType(type);

            var messages = new List<Message>();
            EventHandler<MessageReceivedEventArgs> messageReceived = (o, args) => messages.Add(args.Message);

            MessageInspector.MessageReceived += messageReceived;
            var result = await _client.previousEnquiryOperationAsync(new previousEnquiryRequestType { enquiryId = enquiryId, contentType = new[] { contentType } });
            await Task.Delay(3000);
            MessageInspector.MessageReceived -= messageReceived;

            var binaryData = result.response.binaryData[0];

            return new PreviousEnquiryResult
            {
                Bytes = binaryData.Value
            };
        }

        private string GetContentType(PreviousEnquiryContentType type)
        {
            switch (type)
            {
                case PreviousEnquiryContentType.Pdf:
                    return "application/pdf";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
