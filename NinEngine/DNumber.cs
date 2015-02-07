using System;
using System.Collections.Generic;
using System.Linq;

namespace NinEngine
{
    public class DNumber : DateBasedIdNumber
    {
        public const int PossibleLegalVariations = 26412204;

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

        public static IEnumerable<DNumber> ManyRandom(int count)
        {
            List<DNumber> candidates = AllPossible().ToList();
            if (count >= candidates.Count)
            {
                return candidates;
            }
            List<DNumber> found = new List<DNumber>();
            for (int itemNo = 0; itemNo < count; ++itemNo)
            {
                int itemIndex = Rand.Next(candidates.Count);
                found.Add(candidates[itemNo]);
                candidates.RemoveAt(itemIndex);
            }
            return found;
        }

        public static IEnumerable<DNumber> AllPossible()
        {
            List<DNumber> result = new List<DNumber>();
            foreach (Range range in Ranges)
            {
                AddAllPossibleForRange(result, range);
            }
            return result;
        }

        private static void AddAllPossibleForRange(ICollection<DNumber> birthNumbers, Range range)
        {
            for (int year = range.FromYear; year <= range.ToYear; ++year)
            {
                AddAllPossibleForYear(birthNumbers, year, range.FromIndividual, range.ToIndividual);
            }
        }

        private static void AddAllPossibleForYear(ICollection<DNumber> birthNumbers, int year, int fromIndividual, int toIndividual)
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

        private static void AddOneIfLegal(ICollection<DNumber> birthNumbers, DateTime dt, int individual)
        {
            string number = Adjust(string.Format("{0:ddMMyy}{1:000}", dt, individual));
            char checkDigit1 = Modulo11(WeightsForCheckDigit1, number);
            if ('-' != checkDigit1)
            {
                number += checkDigit1;
                char checkDigit2 = Modulo11(WeightsForCheckDigit2, number);
                if ('-' != checkDigit2)
                {
                    number += checkDigit2;
                    DNumber birthNumber = new DNumber(number);
                    birthNumbers.Add(birthNumber);
                }
            }
        }
    }
}
