
namespace BPTD_Lab1.Models
{
    public class EncryptedData
    {
        public string Key { get; set; }

        public string Text { get; set; }

        public string EncryptedKey { get; set; }

        public string EncryptedText { get; set; }

        public double AverageEntropy { get; set; }
    }
}
