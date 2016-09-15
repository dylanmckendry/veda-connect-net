using System;
using System.Globalization;
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

        public async Task SubmitEnquiryAsync(Enquiry enquiry)
        {
            var header = enquiry.Header;
            var individual = enquiry.Data.Individual;
            var productEnquiry = enquiry.Data.Enquiry;

            var addresses = Addresses(individual);
            var request = new submitEnquiryRequest(new requestType
            {
                enquiryheader = Enquiryheader(header),
                enquirydata = new requestTypeEnquirydata
                {
                    Item = new individualinputType
                    {
                        currentname = new individualnameinputType
                        {
                            title = individual.Title,
                            firstgivenname = individual.FirstName,
                            othergivenname = individual.OtherNames,
                            familyname = individual.FamilyName
                        },
                        addresses = addresses,
                        dateofbirth = individual.DateOfBirth ?? DateTime.MinValue,
                        dateofbirthSpecified = individual.DateOfBirth.HasValue,
                        driverslicence = new photoidcardType
                        {
                            number = individual.LicenseNumber
                        },
                        gendercode = (gendercodeType)Enum.Parse(typeof(gendercodeType), individual.Gender.ToString()[0].ToString())
                    },
                    enquiry = new enquiryType
                    {
                        accounttypecode = productEnquiry.AccountTypeCode,
                        enquiryamount = new MoneyType
                        {
                            currencycode = productEnquiry.CurrencyCode,
                            Value = productEnquiry.EnquiryAmount.ToString(CultureInfo.InvariantCulture)
                        },
                        iscreditreview = productEnquiry.IsCreditReview ?? false,
                        iscreditreviewSpecified = productEnquiry.IsCreditReview.HasValue,
                        relationshipcode = productEnquiry.RelationshipCode
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

        private static addressinputType[] Addresses(Individual individual)
        {
            return individual.Addresses.Select(a =>
                new addressinputType
                {
                    type =
                        (currentPreviousType)Enum.Parse(typeof(currentPreviousType), a.Type.ToString()[0].ToString()),
                    Items = new object[]
                    {
                        a.Unit,
                        a.StreetNumber,
                        a.StreetName,
                        a.StreetType.ToString(),
                        a.Suburb,
                        new stateType
                        {
                            Value = (AustralianStateType) Enum.Parse(typeof (AustralianStateType), a.State.ToString())
                        },
                        a.Postcode
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
                    typeSpecified = a.Type != null
                })
                .ToArray();
        }

        private static requestTypeEnquiryheader Enquiryheader(EnquiryHeader header)
        {
            return new requestTypeEnquiryheader
            {
                clientreference = header.ClientReference,
                operatorid = header.OperatorId,
                operatorname = header.OperatorName,
                permissiontypecode = header.PermissionTypeCode,
                productdatalevelcode = header.ProductDataLevelCode,
                requestedscores = header.RequestedScores
            };
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
}

