using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace VedaConnect.Tests
{
    public class PreviousEnquiryClientTests
    {
        [Test]
        public async Task GetPdf()
        {
            var enquiry = await new VedaScoreApplyClientTests().SubmitEnquiryAsync();

            var client = new PreviousEnquiryClient(
               "https://cteau.vedaxml.com/sys2/previous-enquiry-v1", "TEST_USER", "TEST_PASSWORD" //TEST
               // "https://vedaxml.com/sys2/previous-enquiry-v1", "PROD_USER", "PROD_PASSWORD"  //PROD
               );

            var result = await client.GetPreviousEnquiryAsync(enquiry.Headers.Id, PreviousEnquiryContentType.Pdf);

            result.Should().NotBeNull();
            result.Bytes.Should().NotBeNullOrEmpty();
        }
    }
}
