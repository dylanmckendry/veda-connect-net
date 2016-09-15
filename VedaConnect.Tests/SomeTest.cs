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
            await client.Apply();
        }
    }
}
