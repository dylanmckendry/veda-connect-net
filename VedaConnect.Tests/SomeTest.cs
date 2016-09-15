using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace VedaConnect.Tests
{
    public class SomeTest
    {
        [Test]
        public async Task Apply()
        {
            var client = new Client(
                "https://cteau.vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "TEST_USER", "TEST_PASSWORD" //TEST
                // "https://vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "PROD_USER", "PROD_PASSWORD"  //PROD
                );
            var enquiry = new Enquiry
            {
                Header = new EnquiryHeader
                {
                    ClientReference = "my-ref-101",
                    OperatorId = "101",
                    OperatorName = "John Smith",
                    PermissionType = PermissionType.ConsumerPlusCommercial,
                    ProductDataLevel = ProductDataLevel.Negative,
                    RequestedScores = new[] {"VSA_2.0_XY_NR"}
                },
                Data = new EnquiryData
                {
                    Individual = new Individual
                    {
                        Title = "Mr",
                        FirstName = "Samuel",
                        OtherNames = new[] {"John"},
                        FamilyName = "Elks",
                        Addresses = new[]
                        {
                            new Address
                            {
                                Type = AddressType.Current,
                                Unit = "9",
                                StreetNumber = "20",
                                StreetName = "Pacific",
                                StreetType = StreetType.ST,
                                Suburb = "Bronte",
                                State = State.NSW,
                                Postcode = "2024"
                            }
                        },
                        DateOfBirth = new DateTime(1965, 4, 5),
                        LicenseNumber = "DD8723633",
                        Gender = Gender.Male
                    },
                    Enquiry = new EnquiryType
                    {
                        AccountTypeCode = "PR",
                        EnquiryAmount = 5000M,
                        CurrencyCode = "AUD",
                        IsCreditReview = false,
                        RelationshipCode = "1"
                    }
                }
            };
            await client.SubmitEnquiryAsync(enquiry);
        }
    }
}
