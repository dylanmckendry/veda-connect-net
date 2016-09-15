using System;
using System.Linq;
using VedaConnect.VedaScoreApply;

namespace VedaConnect
{
    public class VedaConnectException : Exception
    {
        public VedaError[] Errors { get; private set; }

        public VedaConnectException(string message)
            : base(message)
        { }

        public VedaConnectException(string message, Exception innerException)
            : base(message, innerException)
        { }

        internal VedaConnectException(string message, errorType[] errors)
            : base(message)
        {
            Errors = errors
                .Select(e => new VedaError
                {
                    FaultCode = e.faultcode,
                    FaultActor = e.faultactor,
                    FaultString = e.faultstring,
                    Detail = e.detail
                })
                .ToArray();
        }

        public class VedaError
        {
            public string FaultCode { get; set; }
            public string FaultActor { get; set; }
            public string FaultString { get; set; }
            public string Detail { get; set; }
        }
    }
}