using System;
using System.Collections.Generic;
using System.Linq;

namespace NinEngine
{
    public class BirthNumber : DateBasedIdNumber
    {
        public const int PossibleLegalVariations = 26412179;

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

        public static BirthNumber OneRandom(DateTime dateFrom, DateTime dateTo, GenderRequest gender = GenderRequest.Any, int maxTryCount = DefaultRetryCount)
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

        public static BirthNumber OneRandom(GenderRequest gender, int maxTryCount = DefaultRetryCount)
        {
            return OneRandom(FirstPossible, LastPossible, gender, maxTryCount);
        }

        public static IEnumerable<BirthNumber> ManyRandom(int count)
        {
            List<BirthNumber> candidates = AllPossible().ToList();
            if (count >= candidates.Count)
            {
                return candidates;
            }
            List<BirthNumber> found = new List<BirthNumber>();
            for (int itemNo = 0; itemNo < count; ++itemNo)
            {
                int itemIndex = Rand.Next(candidates.Count);
                found.Add(candidates[itemIndex]);
                candidates.RemoveAt(itemIndex);
            }
            return found;
        }

        public static IEnumerable<BirthNumber> AllPossible()
        {
            List<BirthNumber> result = new List<BirthNumber>();
            foreach (IndividualNumberProvider.RangeInfo rangeInfo in IndividualNumberProvider.RangeInfos)
            {
                AddAllPossibleForRange(result, rangeInfo);
            }
            return result;
        }

        private static void AddAllPossibleForRange(ICollection<BirthNumber> birthNumbers, IndividualNumberProvider.RangeInfo rangeInfo)
        {
            for (int year = rangeInfo.FromYear; year <= rangeInfo.ToYear; ++year)
            {
                AddAllPossibleForYear(birthNumbers, year, rangeInfo.FromIndividual, rangeInfo.ToIndividual);
            }
        }

        private static void AddAllPossibleForYear(ICollection<BirthNumber> birthNumbers, int year, int fromIndividual, int toIndividual)
        {
            DateTime dt = new DateTime(year, 1, 1);
            while (dt.Year == year)
            {
                for (int individual = fromIndividual; individual <= toIndividual; ++individual)
                {
                    AddOneIfLegal(birthNumbers, dt, individual);
                }
                dt = dt.AddDays(1d);
            }
        }

        private static void AddOneIfLegal(ICollection<BirthNumber> birthNumbers, DateTime dt, int individual)
        {
            string number = string.Format("{0:ddMMyy}{1:000}", dt, individual);
            char checkDigit1 = Modulo11(WeightsForCheckDigit1, number);
            if ('-' != checkDigit1)
            {
                number += checkDigit1;
                char checkDigit2 = Modulo11(WeightsForCheckDigit2, number);
                if ('-' != checkDigit2)
                {
                    number += checkDigit2;
                    BirthNumber birthNumber = new BirthNumber(number);
                    birthNumbers.Add(birthNumber);
                }
            }
        }
    }
}
