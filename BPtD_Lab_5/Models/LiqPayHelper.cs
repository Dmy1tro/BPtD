using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace BPtD_Lab_5.Models
{
    public class LiqPayHelper
    {
        private const string PrivateKey = "";
        private const string PublicKey = "";

        public static (string, string) GetLiqPayProcessedData(decimal amount, string orderId, string resultUrl)
        {
            var signature = new LiqPayData
            {
                public_key = PublicKey,
                version = 3,
                action = "pay",
                amount = amount,
                currency = "UAH",
                description = "Purchasing labs",
                order_id = orderId,
                sandbox = 1,

                result_url = resultUrl,
                info = "Some info about purchasing....",
                product_category = "Science",
                product_description = "Super labs",
                product_name = "Universe staff"
            };

            var json = JsonConvert.SerializeObject(signature);
            var dataHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var signatureHash = GetLiqPaySignature(dataHash);

            return (dataHash, signatureHash);
        }

        public static string GetLiqPaySignature(string data)
        {
            var key = PrivateKey + data + PrivateKey;
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var hash = SHA1.Create().ComputeHash(keyBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
