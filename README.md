# veda-connect-net

VedaConnect .NET SDK is a C# binding for the VedaScore Apply API.

## Getting Started

1. Reference the VedaConnect project
2. Initialise the client and make an enquiry by calling -

   ```code
    var applyClient = new VedaScoreApplyClient("https://cteau.vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "TEST_USER", "TEST_PASSWORD");
    var enquiryResult = await applyClient.SubmitEnquiryAsync(new Enquiry { /* Enquiry data */ });
    ```

3. Get the PDF report for the enquiry by calling -

   ```code
    var reportClient = new PreviousEnquiryClient("https://cteau.vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "TEST_USER", "TEST_PASSWORD");
    var reportResult = await reportClient.GetPreviousEnquiryAsync(enquiryResult.Headers.Id, PreviousEnquiryContentType.Pdf);
    // Save reportResult.Bytes to a PDF file...
    ```
