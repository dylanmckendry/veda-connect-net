# veda-connect-net

VedaConnect .NET SDK is a C# binding for the VedaScore Apply API.

## Getting Started

1. Reference the VedaConnect project
2. Add the following binding config to your project:

    <system.serviceModel>
        <bindings>
            <customBinding>
            <binding name="VedaConnectBining">
                <textMessageEncoding messageVersion="Soap11WSAddressing10" />
                <security authenticationMode="UserNameOverTransport" includeTimestamp="false" />
                <httpsTransport authenticationScheme="Basic" keepAliveEnabled="false" />
                </binding>
                </customBinding>
        </bindings>
        <client>
            <endpoint address="https://cteau.vedaxml.com/sys2/soap11/vedascore-apply-v2-0"
                binding="customBinding" bindingConfiguration="VedaConnectBining"
                contract="VedaScoreApply.VedaScoreApplyPortType" name="VedaConnectEndpoint" />
        </client>
    </system.serviceModel>

3. Initialise the client and call -

    var client = new Client("https://cteau.vedaxml.com/sys2/soap11/vedascore-apply-v2-0", "TEST_USER", "TEST_PASSWORD");
	await client.SubmitEnquiryAsync(new Enquiry { /* Enquiry data */ });

