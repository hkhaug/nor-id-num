using System;

namespace NinEngine
{
    public class BirthNumber : DateBasedIdNumber
    {
        public BirthNumber(string number) : base("Fødselsnummer", number)
        {
            AssertNotNullOrEmpty();
            AssertLength(11);
            AssertDigitsOnly();
            AssertDayAndMonthIsValid(false);
            int year = AssertYearAndIndividualNumberCombinationIsValid();
            AssertDateIsValid(year, false);
            AssertCheckDigit(WeightsForCheckDigit1, Number.Substring(0, 9), Number[9]);
            AssertCheckDigit(WeightsForCheckDigit2, Number.Substring(0, 10), Number[10]);
        }

        public static BirthNumber Create(string number)
        {
            BirthNumber result;
            try
            {
                result = new BirthNumber(number);
            }
            catch (NinException)
            {
                result = null;
            }
            return result;
        }

        public static BirthNumber OneRandom(int maxTryCount = DefaultRetryCount)
        {
            BirthNumber result = null;
            for (int tryCounter = 0; tryCounter < maxTryCount; ++tryCounter)
            {
                string number = OneRandomNumber();
                result = Create(number);
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        public static BirthNumber OneRandom(string pattern, int maxTryCount = DefaultRetryCount)
        {
            ValidatePattern(pattern, 11);
            BirthNumber result = null;
            for (int tryCounter = 0; tryCounter < maxTryCount; ++tryCounter)
            {
                string number = MakeDate(pattern) + MakeIndividualNo(pattern);
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

        public static BirthNumber OneRandom(DateTime dateFrom, DateTime dateTo, GenderRequest gender, int maxTryCount = DefaultRetryCount)
        {
            BirthNumber result = null;
            for (int tryCounter = 0; tryCounter < maxTryCount; ++tryCounter)
            {
                string number = OneRandomNumber(dateFrom, dateTo, gender);
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
