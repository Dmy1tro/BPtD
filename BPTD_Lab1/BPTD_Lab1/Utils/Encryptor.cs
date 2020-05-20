using BPTD_Lab1.Models;
using System;

namespace BPTD_Lab1.Utils
{
    public static class Encryptor
    {
        public const int SizeOfBlock = 128;
        public const int SizeOfChar = 16;
        public const int LengthOfBlock = SizeOfBlock / SizeOfChar;
        public const int ShiftKey = 2;
        public const int QuantityOfRounds = 16;

        private static string[] _blocks;
        private static long _totalTries = 0;
        private static long _oneBitCount = 0;
        private static double _averageEntropy = 0;

        public static EncryptedData EncriptDES(string text, string key)
        {
            text = TextToCorrectLength(text);

            _blocks = CutStringIntoBlocks(text);

            key = KeyToCorrectLength(key, LengthOfBlock / 2);

            var keyInBinaryFormat = StringToBinaryFormat(key);

            for (int j = 0; j < QuantityOfRounds; j++)
            {
                for (int i = 0; i < _blocks.Length; i++)
                {
                    _blocks[i] = EncodeDES_One_Round(_blocks[i], keyInBinaryFormat);
                }

                keyInBinaryFormat = KeyToNextRound(keyInBinaryFormat);
            }

            keyInBinaryFormat = KeyToPrevRound(keyInBinaryFormat);

            var encryptedBinaryText = string.Empty;

            for (int i = 0; i < _blocks.Length; i++)
            {
                encryptedBinaryText += _blocks[i];
            }

            _blocks = null;

            var encryptedKey = string.Empty;
            var encryptedText = string.Empty;
            var averageEntropy = 0;

            var result = new EncryptedData
            {
                Key = key,
                Text = text,
                EncryptedKey = StringFromBinaryToNormalFormat(keyInBinaryFormat),
                EncryptedText = StringFromBinaryToNormalFormat(encryptedBinaryText),
                AverageEntropy = averageEntropy
            };

            return result;
        }

        public static EncryptedData DecryptDES(string encryptedText, string encryptedKey)
        {
            encryptedText = TextToCorrectLength(encryptedText);

            var keyInBinaryFormat = StringToBinaryFormat(encryptedKey);

            _blocks = CutStringIntoBlocks(encryptedText);

            for (int j = 0; j < QuantityOfRounds; j++)
            {
                for (int i = 0; i < _blocks.Length; i++)
                    _blocks[i] = DecodeDES_One_Round(_blocks[i], keyInBinaryFormat);

                keyInBinaryFormat = KeyToPrevRound(keyInBinaryFormat);
            }

            keyInBinaryFormat = KeyToNextRound(keyInBinaryFormat);

            string textInBinaryFormat = string.Empty;

            for (int i = 0; i < _blocks.Length; i++)
                textInBinaryFormat += _blocks[i];

            _blocks = null;

            var result = new EncryptedData
            {
                Key = StringFromBinaryToNormalFormat(keyInBinaryFormat),
                Text = StringFromBinaryToNormalFormat(textInBinaryFormat),
                EncryptedKey = encryptedKey,
                EncryptedText = encryptedText
            };

            return result;
        }

        public static bool IsKeyWeak(string text, string key)
        {
            text = TextToCorrectLength(text);
            var blocks = CutStringIntoBlocks(text);

            var encryptedInfo = EncriptDES(blocks[0], key);

            var encryptedText = encryptedInfo.EncryptedText;

            var repeatedResult = EncriptDES(encryptedText, key);

            return repeatedResult.EncryptedText == blocks[0];
        }

        private static string TextToCorrectLength(string input)
        {
            while (input.Length % LengthOfBlock != 0)
                input += ".";

            return input;
        }

        private static string KeyToCorrectLength(string input, int lengthKey)
        {
            while (input.Length < lengthKey)
                input = "&" + input;

            return input;
        }

        private static string[] CutStringIntoBlocks(string input)
        {
            var blocks = new string[(input.Length / LengthOfBlock)];

            for (int i = 0; i < blocks.Length; i++)
            {
                var temp = input.Substring(i * LengthOfBlock, LengthOfBlock);

                blocks[i] = StringToBinaryFormat(temp);
            }

            return blocks;
        }

        private static string StringToBinaryFormat(string input)
        {
            string output = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                string char_binary = Convert.ToString(input[i], 2);

                while (char_binary.Length < SizeOfChar)
                    char_binary = "0" + char_binary;

                output += char_binary;
            }

            return output;
        }

        private static string StringFromBinaryToNormalFormat(string input)
        {
            string output = string.Empty;

            for (int i = 0; i < input.Length; i += SizeOfChar)
            {
                var char_binary = input.Substring(i, SizeOfChar);

                var char_normal = Convert.ToInt32(char_binary, 2);

                output += (char)char_normal;
            }

            return output;
        }

        private static string EncodeDES_One_Round(string input, string key)
        {
            string L = input.Substring(0, input.Length / 2);
            string R = input.Substring(input.Length / 2);

            return (R + XOR(L, F(R, key)));
        }

        private static string DecodeDES_One_Round(string input, string key)
        {
            string L = input.Substring(0, input.Length / 2);
            string R = input.Substring(input.Length / 2);

            return (XOR(F(L, key), R) + L);
        }

        private static string XOR(string s1, string s2)
        {
            string result = string.Empty;

            _totalTries = 0;
            _oneBitCount = 0;

            for (int i = 0; i < s1.Length; i++)
            {
                bool a = Convert.ToBoolean(Convert.ToInt32(s1[i].ToString()));
                bool b = Convert.ToBoolean(Convert.ToInt32(s2[i].ToString()));

                result += a ^ b ? "1" : "0";

                _totalTries++;
                _oneBitCount += a ^ b ? 1 : 0;
            }

            _averageEntropy = (double)_oneBitCount / _totalTries;

            return result;
        }

        private static string F(string text, string key)
        {
            var result = XOR(text, key);

            if (key.Length > text.Length)
            {
                result = XOR(result, key.Remove(0, key.Length - text.Length));
            }

            return result;
        }

        private static string KeyToNextRound(string key)
        {
            for (int i = 0; i < ShiftKey; i++)
            {
                key = key[key.Length - 1] + key;
                key = key.Remove(key.Length - 1);
            }

            return key;
        }

        private static string KeyToPrevRound(string key)
        {
            for (int i = 0; i < ShiftKey; i++)
            {
                key += key[0];
                key = key.Remove(0, 1);
            }

            return key;
        }
    }
}
