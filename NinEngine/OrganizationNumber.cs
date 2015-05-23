using System.Collections.Generic;
using System.Linq;

namespace NinEngine
{
    public class OrganizationNumber : IdNumberBase
    {
        public const int PossibleLegalVariations = 18181818;

        private static readonly int[] WeightsForCheckDigit = { 3, 2, 7, 6, 5, 4, 3, 2 };

        public OrganizationNumber(string number) : base("Organisasjonsnummer", number)
        {
            AssertNotNullOrEmpty();
            AssertLength(9);
            AssertDigitsOnly();
            AssertFirstDigit8Or9();
            AssertCheckDigit(WeightsForCheckDigit, Number.Substring(0, 8), Number[8]);
        }

        public static OrganizationNumber Create(string number)
        {
            OrganizationNumber result;
            try
            {
                result = new OrganizationNumber(number);
            }
            catch (NinException)
            {
                result = null;
            }
            return result;
        }

        private void AssertFirstDigit8Or9()
        {
            if ((Number[0] != '8') && (Number[0] != '9'))
            {
                string msg = string.Format("{0} '{1}' har ikke 8 eller 9 som første siffer.", Name, Number);
                throw new NinException(Statuscode.BadFirstDigit, msg);
            }
        }

        public static OrganizationNumber OneRandom()
        {
            string number;
            char checkDigit;
            do
            {
                number = Rand.Next(80000000, 100000000).ToString();
                checkDigit = Modulo11(WeightsForCheckDigit, number);
            } while ('-' == checkDigit);
            return new OrganizationNumber(number + checkDigit);
        }

        public static OrganizationNumber OneRandom(string pattern, int maxTryCount = DefaultRetryCount)
        {
            ValidatePattern(pattern, 9);
            char[] resultChars = new char[9];
            OrganizationNumber result = null;
            for(int tryCounter = 0; tryCounter < maxTryCount; ++tryCounter)
            {
                SetFirstDigit(resultChars, pattern);
                SetMiddleDigits(resultChars, pattern);
                SetCheckDigit(resultChars, pattern);
                result = Create(new string(resultChars));
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        private static void SetFirstDigit(IList<char> resultChars, string pattern)
        {
            switch (pattern[0])
            {
                case Wildcard:
                    resultChars[0] = (char)('8' + Rand.Next(2));
                    break;
                case '8':
                case '9':
                    resultChars[0] = pattern[0];
                    break;
                default:
                {
                    string msg = string.Format("Mønster '{0}' kan ikke gi noen lovlige resultater fordi første siffer er ugyldig (må være 8 eller 9).", pattern);
                    throw new NinException(Statuscode.BadPattern, msg);
                }
            }
        }

        private static void SetMiddleDigits(IList<char> resultChars, string pattern)
        {
            for (int index = 1; index < 8; ++index)
            {
                if (Wildcard == pattern[index])
                {
                    resultChars[index] = (char) ('0' + Rand.Next(10));
                }
                else
                {
                    resultChars[index] = pattern[index];
                }
            }
        }

        private static void SetCheckDigit(char[] resultChars, string pattern)
        {
            if (Wildcard == pattern[8])
            {
                string numberWithoutCheckdigit = new string(resultChars, 0, 8);
                resultChars[8] = Modulo11(WeightsForCheckDigit, numberWithoutCheckdigit);
            }
            else
            {
                resultChars[8] = pattern[8];
            }
        }

        public static IEnumerable<OrganizationNumber> ManyRandom(int count)
        {
            List<OrganizationNumber> candidates = AllPossible().ToList();
            if (count >= candidates.Count)
            {
                return candidates;
            }
            List<OrganizationNumber> found = new List<OrganizationNumber>();
            for (int itemNo = 0; itemNo < count; ++itemNo)
            {
                int itemIndex = Rand.Next(candidates.Count);
                found.Add(candidates[itemIndex]);
                candidates.RemoveAt(itemIndex);
            }
            return found;
        }

        public static IEnumerable<OrganizationNumber> AllPossible()
        {
            List<OrganizationNumber> result = new List<OrganizationNumber>();
            for (int number = 80000000; number < 100000000; ++number)
            {
                string numberStr = number.ToString();
                char checkDigit = Modulo11(WeightsForCheckDigit, numberStr);
                if ('-' != checkDigit)
                {
                    result.Add(new OrganizationNumber(numberStr + checkDigit));
                }
            }
            return result;
        }

        public static IEnumerable<OrganizationNumber> QuickManyRandom(int count)
        {
            List<OrganizationNumber> result = new List<OrganizationNumber>();
            for (int itemNo = 0; itemNo < count; ++itemNo)
            {
                result.Add(OneRandom());
            }
            return result;
        }
    }
}
