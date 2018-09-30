using System.Security.Cryptography;
using System.Text;
namespace Digital_Signing_Library
{
    public class DigitalSigning
    {
        public byte[] HashDocument(string document)
        {
            byte[] hashedDocument = null;
            using (SHA256 sha = SHA256Managed.Create())
            {
                hashedDocument = sha.ComputeHash(Encoding.UTF8.GetBytes(document));
            }
            return hashedDocument;
        }

        /// <summary>
        /// This method returns a Signed Document.Save the signature for later 
        /// use for verification.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="key">Any name to help identify the original owner 
        /// </param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signatureOfTheOriginaDocument">The SignedDocument method creates one.</param>
        /// <param name="documentToVerify"></param>
        /// <param name="key">The name used to Sign the document above</param>
        /// <returns></returns>
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
