using System;

namespace NinEngine
{
    public class DNumber : DateBasedIdNumber
    {
        public DNumber(string number) : base("D-nummer", number)
        {
            AssertNotNullOrEmpty();
            AssertLength(11);
            AssertDigitsOnly();
            AssertDayAndMonthIsValid(true);
            int year = AssertYearAndIndividualNumberCombinationIsValid();
            AssertDateIsValid(year, true);
            AssertCheckDigit(WeightsForCheckDigit1, Number.Substring(0, 9), Number[9]);
            AssertCheckDigit(WeightsForCheckDigit2, Number.Substring(0, 10), Number[10]);
        }

        public static DNumber Create(string number)
        {
            DNumber result;
            try
            {
                result = new DNumber(number);
            }
            catch (NinException)
            {
                result = null;
            }
            return result;
        }

        private static string Adjust(string number)
        {
            return (char)(number[0] + 4) + number.Substring(1);
        }

        public static DNumber OneRandom(int maxTryCount = DefaultRetryCount)
        {
            DNumber result = null;
            for (int tryCounter = 0; tryCounter < maxTryCount; ++tryCounter)
            {
                string number = OneRandomNumber(Adjust);
                result = Create(number);
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        public static DNumber OneRandom(string pattern, int maxTryCount = DefaultRetryCount)
        {
            ValidatePattern(pattern, 11);
            DNumber result = null;
            for (int tryCounter = 0; tryCounter < maxTryCount; ++tryCounter)
            {
                string number = MakeDate(pattern, Adjust) + MakeIndividualNo(pattern);
                char checkDigit1 = MakeFirstCheckDigit(number, pattern);
                if (checkDigit1 != '-')
                {
                    number += checkDigit1;
                    char checkDigit2 = MakeSecondCheckDigit(number, pattern);
                    if (checkDigit2 != '-')
                    {
                        number += checkDigit2;
                        result = Create(number);
                        if (result != null)
                        {
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public static DNumber OneRandom(DateTime dateFrom, DateTime dateTo, GenderRequest gender, int maxTryCount = DefaultRetryCount)
        {
            DNumber result = null;
            for (int tryCounter = 0; tryCounter < maxTryCount; ++tryCounter)
            {
                string number = OneRandomNumber(dateFrom, dateTo, gender, Adjust);
                result = Create(number);
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }
    }
}
