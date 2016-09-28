using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using VedaConnect.VedaScoreApply;

namespace VedaConnect
{
    public class Client : IDisposable
    {
        private readonly VedaScoreApplyPortTypeClient _client;

        public Client(string url, string username, string password)
        {
            var encoding = new TextMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11WSAddressing10 };
            var transport = new HttpsTransportBindingElement { AuthenticationScheme = AuthenticationSchemes.Basic, KeepAliveEnabled = true, MaxReceivedMessageSize = int.MaxValue };
            var security = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
            security.IncludeTimestamp = false;

            var binding = new CustomBinding();
            binding.Elements.Add(encoding);
            binding.Elements.Add(security);
            binding.Elements.Add(transport);

            _client = new VedaScoreApplyPortTypeClient(binding, new EndpointAddress(url));

            if (_client.ClientCredentials == null)
            {
                throw new Exception("Client credentials not available because the web service endpoint hasn't been configured properly.");
            }

            _client.ClientCredentials.UserName.UserName = username;
            _client.ClientCredentials.UserName.Password = password;
        }

        public async Task<response> SubmitEnquiryAsync(Enquiry enquiry)
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
                        gendercode = individual.Gender.ToCode()
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

            return sumbitEnquiryResponse.response;
        }

        private static addressinputType[] Addresses(Individual individual)
        {
            return individual.Addresses.Select(a =>
            {
                var items = new Dictionary<ItemsChoiceType, object>
                {
                    {ItemsChoiceType.property, a.Property},
                    {ItemsChoiceType.unitnumber, a.Unit},
                    {ItemsChoiceType.streetnumber, a.StreetNumber},
                    {ItemsChoiceType.streetname, a.StreetName},
                    {ItemsChoiceType.streettype, a.StreetType.ToString()},
                    {ItemsChoiceType.suburb, a.Suburb},
                    {ItemsChoiceType.state,a.State == null ? null : new stateType {Value = a.State.Value.ToAustralianStateType()}},
                    {ItemsChoiceType.postcode, a.Postcode},
                    {ItemsChoiceType.countrycode, a.CountryCode}
                }
                    .Where(k => k.Value != null)
                    .ToDictionary(k => k.Key, k => k.Value);
                var result = new addressinputType
                {
                    type = a.Type?.ToCurrentPreviousType() ?? currentPreviousType.C,
                    typeSpecified = a.Type != null,
                    Items = items.Values.ToArray(),
                    ItemsElementName = items.Keys.ToArray()
                };
                return result;
            }).ToArray();
        }

        private static requestTypeEnquiryheader Enquiryheader(EnquiryHeader header)
        {
            return new requestTypeEnquiryheader
            {
                clientreference = header.ClientReference,
                operatorid = header.OperatorId,
                operatorname = header.OperatorName,
                permissiontypecode = header.PermissionType.ToCode(),
                productdatalevelcode = header.ProductDataLevel.ToCode(),
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

