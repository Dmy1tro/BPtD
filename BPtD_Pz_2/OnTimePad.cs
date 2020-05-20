using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPtD_Pz_2
{
    class OnTimePad
    {
        private readonly Dictionary<char, int> _alphabet;
        private readonly Dictionary<int, char> _reversedAlphabet;

        public OnTimePad()
        {
            _alphabet = new Dictionary<char, int>();
            _reversedAlphabet = new Dictionary<int, char>();
                
            InitializeAlphabet(new char[] { });
        }

        public OnTimePad(IEnumerable<char> alphabet)
        {
            _alphabet = new Dictionary<char, int>();
            _reversedAlphabet = new Dictionary<int, char>();

            InitializeAlphabet(alphabet);
        }

        public string Crypt(string text, string key, bool encrypt)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                var textIndex = _alphabet[text[i]];
                var keyIndex = _alphabet[key[i % key.Length]];

                var encryptedIndex = _alphabet.Count + textIndex;
                encryptedIndex += (encrypt ? 1 : -1) * keyIndex;

                sb.Append(_reversedAlphabet[encryptedIndex % _alphabet.Count]);
            }

            return sb.ToString();
        }

        public string CryptByXor(string text, string key)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                var textIndex = _alphabet[text[i]];
                var keyIndex = _alphabet[key[i % key.Length]];

                var encryptedIndex = (textIndex ^ keyIndex) % _alphabet.Count;
                var encryptedLetter = _reversedAlphabet[encryptedIndex];

                sb.Append(encryptedLetter);
            }

            return sb.ToString();
        }

        private void InitializeAlphabet(IEnumerable<char> alphabet)
        {
            if (!alphabet.Any())
            {
                alphabet = GetDefaultAlphabet();
            }

            var i = 0;

            foreach (var c in alphabet)
            {
                _alphabet.Add(c, i);
                _reversedAlphabet.Add(i++, c);
            }
        }

        private string GetDefaultAlphabet()
        {
            return "abcdefghijklmnopqrstuvwxyz" +
                   "abcdefghijklmnopqrstuvwxyz".ToUpper() +
                   " ,.;!?';:" + 
                   "1234567890";
        }

    }
}
