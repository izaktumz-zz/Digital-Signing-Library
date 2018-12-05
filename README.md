[![Build Status](https://dev.azure.com/OpenDevProjects/Digital-Signing-Library/_apis/build/status/geoftums.Digital-Signing-Library)](https://dev.azure.com/OpenDevProjects/Digital-Signing-Library/_build/latest?definitionId=2)

# Digital-Signing-Library
a .Net library to sign and verify documents i.e.PDFs or any other data.

# Digital-Signing
a .Net library to sign and verify document/data.

---This is a package to  Digitally Sign and Verify Data(i.e. documents)-----------
It can be found in the Nuget Manager of Visual Studio

PM->Install-Package DigitalSigning.Utility.Package 

[Usage]
-------
-Use the Nuget Manager to locate and install the package i.e.
 DigitalSigning.Utility.Package
 
 -Namespace
 Using DigitalSigning

[Overview]
----------
-The main class that implements the Digital Signing and Verification is called DigitalSigning.
 
It contains method to Hash,Sign and Verify Documents

[NB]:
-----
Store the Signatures for Later Verification.

[Implementation]
----------------
var digitalSigning=new DigitalSigning.Services.DigitalSigning();//instantiate the class.

UnicodeEncoding byteConverter=new UnicodeEncoding();//use this to convert your data to bytes 

i.e. byteConverter.GetBytes('your data')

[Retrieve Certificates]
-----------------------
-The method below retrieves certificates stored in your computer.It returns an array of certificates found.

var certificates=digitalSigning.GetCertificates("StoreName","StoreLocation")

var certificateToUse=certificates[n]..where n represents a number i.e. 1 or 2 or 3..etc.This returns a single certificate

[Sign The document using a Certificate i.e.X509]
-----------------------------------------------
-The SignUsingCertificate takes two parameters.i.e. data you want to sign and the X509 certificate you want to use

var signature=digitalSigning.SignUsingCertificate('data(convert your data to bytes)'  ,  'certificateToUse(From Above explanation) or an X509 Certificate ')

[Verify The document using a Certificate i.e.X509 Returns true if successful otherwise false ]
---------------------------------------------------------------------------------------------
-The VerifyUsingCertificate takes three parameters.

var signature=digitalSigning.VerifyUsingCertificate('data to verify(in bytes)' , 'signature of the original data', 'X509 Certificate used to sign the document or data')


[Sign The document using a Key]
------------------------------------------------------------------
-The document or data can be a path to a document you want to sign.

var signature=digitalSigning.SignDocument("document/data you want to sign", "a key/name to later identify the original owner");

[Verify signature using a Key.Returns true if successful otherwise false]
-------------------------------------------------------------
The document or data can be a path to a document you want to sign.

var verifySignature=digitalSigning.VerifySignature("signature of the original document/data","the document/data to verify", "the key used to sign the document/data");
