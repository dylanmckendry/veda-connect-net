using System;
using System.Linq;
using System.Threading.Tasks;
using VedaConnect.VedaScoreApply;

namespace VedaConnect
{
    public class Client : IDisposable
    {
        private readonly VedaScoreApplyPortTypeClient _client;

        public Client(string url, string username, string password)
        {
            _client = new VedaScoreApplyPortTypeClient("VedaConnectEndpoint", url);

            if (_client.ClientCredentials == null)
            {
                throw new Exception("Client credentials not available because the web service endpoint hasn't been configured properly.");
            }

            _client.ClientCredentials.UserName.UserName = username;
            _client.ClientCredentials.UserName.Password = password;
        }

        public async Task Apply()
        {
            var request = new submitEnquiryRequest(new requestType
            {
                enquiryheader = new requestTypeEnquiryheader
                {
                    clientreference = "my-ref-101",
                    operatorid = "101",
                    operatorname = "John Smith",
                    permissiontypecode = "XY",
                    productdatalevelcode = "N",
                    requestedscores = new[] { "VSA_2.0_XY_NR" }
                },
                enquirydata = new requestTypeEnquirydata
                {
                    Item = new individualinputType
                    {
                        currentname = new individualnameinputType
                        {
                            title = "Mr",
                            firstgivenname = "Samuel",
                            othergivenname = new[] { "John" },
                            familyname = "Elks"
                        },
                        addresses = new[]
                        {
                            new addressinputType
                            {
                                type = currentPreviousType.C,
                                Items = new object[]
                                {
                                    "9",
                                    "20",
                                    "Pacific",
                                    "ST",
                                    "Bronte",
                                    new stateType {Value = AustralianStateType.NSW},
                                    "2024"
                                },
                                ItemsElementName = new[]
                                {
                                    ItemsChoiceType.unitnumber,
                                    ItemsChoiceType.streetnumber,
                                    ItemsChoiceType.streetname,
                                    ItemsChoiceType.streettype,
                                    ItemsChoiceType.suburb,
                                    ItemsChoiceType.state,
                                    ItemsChoiceType.postcode,
                                },
                                typeSpecified = true
                            }
                        },
                        dateofbirth = new DateTime(1965, 4, 5),
                        dateofbirthSpecified = true,
                        driverslicence = new photoidcardType
                        {
                            number = "DD8723633"
                        },
                        gendercode = gendercodeType.M
                    },
                    enquiry = new enquiryType
                    {
                        accounttypecode = "PR",
                        enquiryamount = new MoneyType { currencycode = "AUD", Value = "5000" },
                        iscreditreview = false,
                        iscreditreviewSpecified = true,
                        relationshipcode = "1"
                    }
                }
            });

            var sumbitEnquiryResponse = await _client.submitEnquiryAsync(request);

            var response = sumbitEnquiryResponse.response;
            if (response.responsetype == responseResponsetype.error)
            {
                throw new VedaConnectException("Boom!", response.errors);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _client.Close();
        }
    }

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

