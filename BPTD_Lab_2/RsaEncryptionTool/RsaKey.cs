namespace RsaEncryptionTool
{
    public class RsaKey
    {
        public RsaKey(string publicKey, string privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        public string PublicKey { get; }

        public string PrivateKey { get; }
    }
}
