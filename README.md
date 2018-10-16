[![Build Status](https://dev.azure.com/geoftums/Digital%20Signing/_apis/build/status/geoftums.Digital-Signing-Library)](https://dev.azure.com/geoftums/Digital%20Signing/_build/latest?definitionId=9)

# Digital-Signing-Library
a .Net library to sign and verify document/data.

# Digital-Signing
a .Net library to sign and verify document/data.

---This is a package to  Digitally Sign and Verify Data(i.e. documents)-----------
It can be found in the Nuget Manager of Visual Studio

PM->Install-Package DigitalSigning.Utility.Package 

[Usage]
-------
-Use the Nuget Manager to locate and install the package i.e.
 DigitalSigning.Utility.Package

[Overview]
----------
-The main class that implements the Digital Signing and Verification is called DigitalSigning.
 It contains method to Hash,Sign and Verify Documents

[Implementation]
----------------
var digitalSigning=new DigitalSigning.Services.DigitalSigning();//instantiate the class.

[Sign The document.NB:Store the signature for later verification]
------------------------------------------------------------------
var signature=digitalSigning.SignDocument("document/data you want to sign", "a key/name to later identify the original owner");

[Verify signature.Returns true if successful otherwise false]
-------------------------------------------------------------
var verifySignature=digitalSigning.VerifySignature("signature of the original document/data","the document/data to verify", "the key used to sign the document/data");
