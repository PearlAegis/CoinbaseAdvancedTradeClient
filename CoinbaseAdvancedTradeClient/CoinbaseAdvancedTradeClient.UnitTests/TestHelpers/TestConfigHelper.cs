using CoinbaseAdvancedTradeClient.Models.Config;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Asn1.Sec;
using System.Text;

namespace CoinbaseAdvancedTradeClient.UnitTests.TestHelpers
{
    public static class TestConfigHelper
    {
        // Generate a valid EC P-256 private key for testing purposes
        public static string GenerateTestKeySecret()
        {
            // Generate P-256 (secp256r1) key pair
            var keyGen = new ECKeyPairGenerator();
            var curveParams = SecNamedCurves.GetByName("secp256r1");
            var domainParams = new ECDomainParameters(curveParams.Curve, curveParams.G, curveParams.N, curveParams.H);
            keyGen.Init(new ECKeyGenerationParameters(domainParams, new SecureRandom()));
            
            var keyPair = keyGen.GenerateKeyPair();
            var privateKey = (ECPrivateKeyParameters)keyPair.Private;
            
            // Convert to PEM format
            using var stringWriter = new StringWriter();
            var pemWriter = new PemWriter(stringWriter);
            pemWriter.WriteObject(privateKey);
            pemWriter.Writer.Flush();
            
            return stringWriter.ToString();
        }

        public static SecretApiKeyConfig CreateTestApiConfig()
        {
            return new SecretApiKeyConfig()
            {
                KeyName = "test-key",
                KeySecret = GenerateTestKeySecret()
            };
        }

        public static SecretApiKeyWebSocketConfig CreateTestWebSocketConfig()
        {
            return new SecretApiKeyWebSocketConfig()
            {
                KeyName = "test-key",
                KeySecret = GenerateTestKeySecret()
            };
        }
    }
}