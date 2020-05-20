using System;
using System.Collections.Generic;
using System.Linq;

namespace BPtD_Pz_2
{
    public class CustomOnTimePad
    {
        private readonly Dictionary<char, string> _alphabet;
        private readonly Dictionary<string, char> _reversedAlphabet;

        public CustomOnTimePad()
        {
            _reversedAlphabet = GetCustomReversedAlphabet();
            _alphabet = _reversedAlphabet.ToDictionary(x => x.Value, x => x.Key);
        }

        public string Encrypt(string text, string key)
        {
            var result = string.Empty;

            text = text.ToUpper();
            key = key.ToUpper();

            if (!text.All(ch => char.IsDigit(ch) || ch == '#'))
            {
                text = ParseStringToInt(text);
            }

            if (!key.All(ch => char.IsDigit(ch) || ch == '#'))
            {
                key = ParseStringToInt(key);
            }

            key = FormatStringLength(key, text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '#')
                {
                    result += text[i];
                    continue;
                }

                var textNumber = int.Parse(text[i].ToString());
                var keyNumber = int.Parse(key[i].ToString());

                var temp = (textNumber + keyNumber) % 10;

                result += temp;
            }

            return result;
        }

        public string Decrypt(string text, string key)
        {
            var result = string.Empty;

            text = text.ToUpper();
            key = key.ToUpper();

            if (!text.All(ch => char.IsDigit(ch) || ch == '#'))
            {
                text = ParseStringToInt(text);
            }

            if (!key.All(ch => char.IsDigit(ch) || ch == '#'))
            {
                key = ParseStringToInt(key);
            }

            key = FormatStringLength(key, text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '#')
                {
                    result += text[i];
                    continue;
                }

                var textNumber = int.Parse(text[i].ToString());
                var keyNumber = int.Parse(key[i].ToString());
                var initialNumber = textNumber - keyNumber;

                var temp = initialNumber >= 0 ? initialNumber : initialNumber + 10;

                result += temp;
            }

            var convertedResult = ParseIntToString(result);

            return convertedResult;
        }

        private string ParseStringToInt(string text)
        {
            var result = string.Empty;

            foreach (var letter in text)
            {
                if (char.IsDigit(letter) || letter == '#')
                {
                    result += '#';
                }
                else
                {
                    result += _alphabet[letter];
                }
            }

            return result;
        }

        private string ParseIntToString(string str)
        {
            var result = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '#')
                {
                    result += str[i];
                    i++;
                }

                var number = int.Parse(str[i].ToString());

                if (number == 8 || number == 9 || number == 0)
                {
                    var temp = str[i].ToString() + str[i + 1 >= str.Length ? i : i + 1].ToString();
                    result += _reversedAlphabet[temp];
                    i++;
                }
                else
                {
                    result += _reversedAlphabet[number.ToString()].ToString();
                }
            }

            return result;
        }

        private string FormatStringLength(string str, int textLength)
        {
            while (str.Length < textLength)
            {
                str += str;
            }

            return str.Substring(0, textLength);
        }

        private Dictionary<string, char> GetCustomReversedAlphabet() =>
            new Dictionary<string, char>
            {
                {"1", 'А' },
                {"2", 'И'},
                {"3", 'Т'},
                {"4", 'Е'},
                {"5", 'С'},
                {"6", 'Н'},
                {"7", 'О'},
                {"81", 'Б'},
                {"82", 'В'},
                {"83", 'Г'},
                {"84", 'Д'},
                {"85", 'Ж'},
                {"86", 'З'},
                {"87", 'К'},
                {"88", 'Л'},
                {"89", 'М'},
                {"80", 'П'},
                {"91", 'Р'},
                {"92", 'У'},
                {"93", 'Ф'},
                {"94", 'Х'},
                {"95", 'Ц'},
                {"96", 'Ч'},
                {"97", 'Ш'},
                {"98", 'Щ'},
                {"99", 'Ъ'},
                {"90", 'Ы'},
                {"01", 'Ь'},
                {"02", 'Э'},
                {"03", 'Ю'},
                {"04", 'Я'},
                {"05", 'Й'},
                {"00", ' '}
            };
    }
}
