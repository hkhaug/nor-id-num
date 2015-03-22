using System.Collections.Generic;

namespace NinEngine
{
    public enum GenderRequest
    {
        Any,
        Female,
        Male
    };

    public static class IndividualNumberProvider
    {
        enum YearRanges
        {
            From1854Through1899,
            From1900Through1939,
            From1940Through1999,
            From2000Through2039
        };

        public struct RangeInfo
        {
            public int FromYear, ToYear, FromIndividual, ToIndividual;
        }

        public static readonly RangeInfo[] RangeInfos =
        {
            new RangeInfo {FromYear = 1854, ToYear = 1899, FromIndividual = 500, ToIndividual = 749},
            new RangeInfo {FromYear = 1900, ToYear = 1999, FromIndividual = 000, ToIndividual = 499},
            new RangeInfo {FromYear = 1940, ToYear = 1999, FromIndividual = 900, ToIndividual = 999},
            new RangeInfo {FromYear = 2000, ToYear = 2039, FromIndividual = 500, ToIndividual = 999}
        };

        private static readonly Dictionary<YearRanges, Dictionary<GenderRequest, IEnumerable<int>>> IndividualNumbers = new Dictionary<YearRanges, Dictionary<GenderRequest, IEnumerable<int>>>
        {
            {YearRanges.From1854Through1899, MakeRange(1854, 1899)},
            {YearRanges.From1900Through1939, MakeRange(1900, 1939)},
            {YearRanges.From1940Through1999, MakeRange(1940, 1999)},
            {YearRanges.From2000Through2039, MakeRange(2000, 2039)}
        };

        private static Dictionary<GenderRequest, IEnumerable<int>> MakeRange(int fromYear, int toYear)
        {
            List<int> all = new List<int>();
            List<int> female = new List<int>();
            List<int> male = new List<int>();
            foreach (RangeInfo rangeInfo in RangeInfos)
            {
                if ((fromYear >= rangeInfo.FromYear) && (toYear <= rangeInfo.ToYear))
                {
                    for (int individualNumber = rangeInfo.FromIndividual; individualNumber <= rangeInfo.ToIndividual; ++individualNumber)
                    {
                        all.Add(individualNumber);
                        if ((individualNumber & 1) == 0)
                        {
                            female.Add(individualNumber);
                        }
                        else
                        {
                            male.Add(individualNumber);
                        }
                    }
                }
            }
            Dictionary<GenderRequest, IEnumerable<int>> result = new Dictionary<GenderRequest, IEnumerable<int>>();
            result.Add(GenderRequest.Any, all);
            result.Add(GenderRequest.Female, female);
            result.Add(GenderRequest.Male, male);
            return result;
        }

        public static IEnumerable<int> GetLegalNumbers(int year, GenderRequest gender)
        {
            YearRanges yearRange = YearToRange(year);
            Dictionary<GenderRequest, IEnumerable<int>> numbersForRange = IndividualNumbers[yearRange];
            return numbersForRange[gender];
        }

        private static YearRanges YearToRange(int year)
        {
            if ((year < 1854) || (year > 2039))
            {
                string msg = string.Format("{0} er ikke et lovlig årstall i området 1854 til 2039.", year);
                throw new NinException(Statuscode.BadYear, msg);
            }
            if (year <= 1899) return YearRanges.From1854Through1899;
            if (year <= 1939) return YearRanges.From1900Through1939;
            if (year <= 1999) return YearRanges.From1940Through1999;
            return YearRanges.From2000Through2039;
        }

        public static int GetYearWithCentury(int twoDigitYear, int individualNumber)
        {
            if (individualNumber <= 499)
            {
                return 1900 + twoDigitYear;
            }

            if (individualNumber <= 749 && twoDigitYear >= 54)
            {
                return 1800 + twoDigitYear;
            }

            if (individualNumber >= 900 && twoDigitYear >= 40)
            {
                return 1900 + twoDigitYear;
            }

            if (twoDigitYear <= 39)
            {
                return 2000 + twoDigitYear;
            }

            return -1;
        }
    }
}
