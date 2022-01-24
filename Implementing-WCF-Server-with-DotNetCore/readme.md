# Implementing WCF Service in DotNetCore

## WHY ?

Microsoft SOAP supported 
- ASMX at least since NetFramework 2.0
    Around 2003
- WCF from NetFramework 3.5 to 4.9
    Around 2021 ...
- DotNetCore WCF Client support of 
    [Supported WCF Client Feature in dotnet 2.1](https://github.com/dotnet/wcf/blob/main/release-notes/SupportedFeatures-v2.1.0.md)
- No MS official support to DotNetCore WCF Server
    Alternatives
        - [CoreWCF](https://github.com/CoreWCF/CoreWCF)


## Implementation

1. Explore a DotNet 4.8 WCF Server
    - Self Hosting
    - Binding ( Basic, WsHttpBinding, NetTCP )
    - MetadataExchange WSDL
2. Explore a DotNetCore WCF Client
    - Proxy Generation
    - Service Consumption ( Basic, WsHttpBinding, NetTCP )
3. Implement a DotNetCore WCF Server
    - CoreWCF Contract / Service Migration
    - CoreWCF Host configuration
    - Service Consumption ( Basic, WsHttpBinding, NetTCP )
