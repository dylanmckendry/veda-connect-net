using System;

namespace VedaConnect
{
    public class MessageHeaders
    {
        public MessageHeaders() { }

        internal MessageHeaders(System.ServiceModel.Channels.MessageHeaders headers)
        {
            Action = headers.Action;
            Count = headers.Count;
            FaultTo = headers.FaultTo?.Uri;
            From = headers.From?.Uri;
            Id = headers.MessageId?.ToString();
            RelatesTo = headers.RelatesTo?.ToString();
            ReplyTo = headers.ReplyTo?.Uri;
            To = headers.To;
        }

        public Uri To { get; set; }

        public Uri FaultTo { get; set; }

        public Uri ReplyTo { get; set; }

        public string RelatesTo { get; set; }

        public string Id { get; set; }

        public Uri From { get; set; }

        public int Count { get; set; }

        public string Action { get; set; }
    }
}