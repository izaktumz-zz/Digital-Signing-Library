using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace DigitalSigning.Services
{
    public class DigitalSigning
    {
        public string HashAlgorithm { get; set; }
        public DigitalSigning()
        {
            HashAlgorithm = "SHA1";
        }
        public X509Certificate2Collection GetCertificates(string storeName, StoreLocation storeLocation)
        {
            var store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            return store.Certificates;
        }

        public byte[] HashData(byte[] data) => new SHA1Managed().ComputeHash(data);

        public byte[] SignUsingCertificate(byte[] data, X509Certificate2 certificateForSigning)
        {
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificateForSigning.PrivateKey;
            var hashedData = HashData(data);
            return csp.SignHash(hashedData, GetHashAlgorithm(HashAlgorithm));
        }
        public bool VerifyUsingCertificate(byte[] dataToVerify, byte[] signatureOfOriginalData, X509Certificate2 certificateUsedToSign)
        {
            var hashedData = HashData(dataToVerify);
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificateUsedToSign.PublicKey.Key;
            return csp.VerifyHash(hashedData, GetHashAlgorithm(HashAlgorithm), signatureOfOriginalData);
        }
        public string GetHashAlgorithm(string HashAlgorithm) => CryptoConfig.MapNameToOID(HashAlgorithm);

        public byte[] HashDocument(string document)
        {
            byte[] hashedDocument = null;
            using (SHA256 sha = SHA256Managed.Create())
            {
                hashedDocument = sha.ComputeHash(Encoding.UTF8.GetBytes(document));
            }
            return hashedDocument;
        }
        public byte[] SignDocument(string document, string key)
        {
            byte[] hashedDocument = HashDocument(document);
            byte[] signedDocument = null;
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = key;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
            {
                var formatter = new RSAPKCS1SignatureFormatter(rsa);
                formatter.SetHashAlgorithm("SHA256");
                signedDocument = formatter.CreateSignature(hashedDocument);
            }
            return signedDocument;
        }
        public bool VerifySignature(byte[] signatureOfTheOriginaDocument, string documentToVerify, string key)
        {
            byte[] hashed_DocumentToVerify = null;
            bool isVerify = false;
            #region Document to verify
            using (SHA256 sha = SHA256Managed.Create())
            {
                hashed_DocumentToVerify = sha.ComputeHash(Encoding.UTF8.GetBytes(documentToVerify));
                CspParameters csp = new CspParameters();
                csp.KeyContainerName = key;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
                {
                    var formatter = new RSAPKCS1SignatureDeformatter(rsa);
                    formatter.SetHashAlgorithm("SHA256");
                    var result = formatter.VerifySignature(hashed_DocumentToVerify, signatureOfTheOriginaDocument);
                    if (result == true)
                    {
                        isVerify = true;
                    }
                }
            }
            #endregion
            return isVerify;
        }
    }
}
