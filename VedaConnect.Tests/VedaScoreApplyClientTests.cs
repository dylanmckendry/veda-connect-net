using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace VedaConnect.Tests
{
    public class VedaScoreApplyClientTests
    {
        [Test]
        public async Task ApplyTwice()
        {
            var client = new VedaScoreApplyClient(
                "https://cteau.vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "TEST_USER", "TEST_PASSWORD" //TEST
                // "https://vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "PROD_USER", "PROD_PASSWORD"  //PROD
                );
            var task1 = GetResult(client);
            var task2 = GetResult(client);

            await Task.WhenAll(task1, task2);

            var result1 = task1.Result;
            var result2 = task2.Result;

            Assert(result1);
            Assert(result2);
        }

        [Test]
        public Task Appy()
        {
            return SubmitEnquiryAsync();
        }

        public async Task<SubmitEnquiryResult> SubmitEnquiryAsync()
        {
            var client = new VedaScoreApplyClient(
                "https://cteau.vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "TEST_USER", "TEST_PASSWORD" //TEST
                // "https://vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "PROD_USER", "PROD_PASSWORD"  //PROD
                );
            var result = await GetResult(client);

            Assert(result);

            return result;
        }

        private static void Assert(SubmitEnquiryResult result)
        {
            result.Should().NotBeNull();
            result.Content.Should().NotBeNullOrEmpty();
            result.Headers.Should().NotBeNull();
            result.Response.Should().NotBeNull();
            result.Headers.Id.Should().Be(result.Response.productheader.enquiryid);
        }

        private static async Task<SubmitEnquiryResult> GetResult(VedaScoreApplyClient vedaScoreApplyClient)
        {
            var enquiry = new Enquiry
            {
                Header = new EnquiryHeader
                {
                    ClientReference = "my-ref-101",
                    OperatorId = "101",
                    OperatorName = "John Smith",
                    PermissionType = PermissionType.ConsumerPlusCommercial,
                    ProductDataLevel = ProductDataLevel.Negative,
                    RequestedScores = new[] { "VSA_2.0_XY_NR" }
                },
                Data = new EnquiryData
                {
                    Individual = new Individual
                    {
                        Title = "Mr",
                        FirstName = "Samuel",
                        OtherNames = new[] { "John" },
                        FamilyName = "Elks",
                        Addresses = new[]
                        {
                            new Address
                            {
                                Type = CurrentOrPrevious.Current,
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
            var result = await vedaScoreApplyClient.SubmitEnquiryAsync(enquiry);
            return result;
        }
    }
}
