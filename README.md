# veda-connect-net

VedaConnect .NET SDK is a C# binding for the VedaScore Apply API.

## Getting Started

1. Reference the VedaConnect project
2. Initialise the client and call -

   ```code
    var client = new Client("https://cteau.vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "TEST_USER", "TEST_PASSWORD");
    await client.SubmitEnquiryAsync(new Enquiry { /* Enquiry data */ });
    ```

