using System;

namespace NinEngine
{
    public enum GenderRequest
    {
        Any,
        Female,
        Male
    };

    public abstract class DateBasedIdNumber : IdNumberBase
    {
        protected static readonly int[] WeightsForCheckDigit1 = { 3, 7, 6, 1, 8, 9, 4, 5, 2 };
        protected static readonly int[] WeightsForCheckDigit2 = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
        public static readonly DateTime FirstPossible = new DateTime(1854, 1, 1);
        public static readonly DateTime LastPossible = new DateTime(2039, 12, 31);

        protected delegate string AdjustNumber(string number);

        protected DateBasedIdNumber(string name, string number) : base(name, number)
        {
        }

        protected void AssertDayAndMonthIsValid(bool isDNumber)
        {
            bool ok = false;
            int mm = Int32.Parse(Number.Substring(2, 2));
            if ((mm >= 1) && (mm <= 12))
            {
                int ddD = Int32.Parse(Number.Substring(0, 2));
                int dd = isDNumber ? ddD - 40 : ddD;
                if (dd >= 1)
                {
                    int yy = Int32.Parse(Number.Substring(4, 2));
                    if (dd <= DateTime.DaysInMonth(1900 + yy, mm) || (dd <= DateTime.DaysInMonth(2000 + yy, mm)))
                    {
                        ok = true;
                    }
                }
            }
            if (!ok)
            {
                string msg = String.Format("{0} '{1}' er ikke en gyldig dato i formen DDMMÅÅ{2}.", Name, Number.Substring(0, 6), isDNumber ? " (første siffer økt med 4)" : String.Empty);
                throw new NinException(Statuscode.BadDate, msg);
            }
        }

        protected int AssertYearAndIndividualNumberCombinationIsValid()
        {
            int yy = Int32.Parse(Number.Substring(4, 2));
            int individual = Int32.Parse(Number.Substring(6, 3));

            if (individual <= 499)
            {
                return 1900 + yy;
            }

            if (individual <= 749 && yy >= 54)
            {
                return 1800 + yy;
            }

            if (individual >= 900 && yy >= 40)
            {
                return 1900 + yy;
            }

            if (yy <= 39)
            {
                return 2000 + yy;
            }

            string msg = String.Format("{0} '{1}' har ikke en gyldig kombinasjon av årstall og individnummer.", Name, Number);
            throw new NinException(Statuscode.BadYearAndIndividualNumberCombination, msg);
        }

        protected void AssertDateIsValid(int year, bool isDNumber)
        {
            int ddD = Int32.Parse(Number.Substring(0, 2));
            int dd = isDNumber ? ddD - 40 : ddD;
            int mm = Int32.Parse(Number.Substring(2, 2));
            if ((mm < 1) || (mm > 12) || (dd < 1) || (dd > DateTime.DaysInMonth(year, mm)))
            {
                string msg = String.Format("{0} '{1}' er ikke gyldig dag og måned for året {2}{3}.", Name, Number.Substring(0, 4), year, isDNumber ? " (første siffer økt med 4)" : String.Empty);
                throw new NinException(Statuscode.BadDate, msg);
            }
        }

        protected static string OneRandomNumber(AdjustNumber adjustMethod = null)
        {
            string date = DateInRange(FirstPossible, LastPossible);
            string number = OneRandomNumber(date, () => Rand.Next(1000), adjustMethod);
            return number;
        }

        protected static string OneRandomNumber(DateTime dateFrom, DateTime dateTo, GenderRequest gender, AdjustNumber adjustMethod = null)
        {
            if (dateFrom < FirstPossible)
            {
                string msg = String.Format("Fra-dato ({0}) kan ikke være tidligere enn {1}.", dateFrom, FirstPossible);
                throw new NinException(Statuscode.BadDate, msg);
            }
            if (dateTo > LastPossible)
            {
                string msg = String.Format("Til-dato ({0}) kan ikke være senere enn {1}.", dateTo, LastPossible);
                throw new NinException(Statuscode.BadDate, msg);
            }
            if (dateFrom > dateTo)
            {
                string msg = String.Format("Fra-dato ({0}) kan ikke være senere enn til-dato ({1}).", dateFrom, dateTo);
                throw new NinException(Statuscode.BadDate, msg);
            }
            string date = DateInRange(dateFrom, dateTo);
            GetIndividualNumber getIndividualMethod;
            switch (gender)
            {
                case GenderRequest.Female:
                    getIndividualMethod = () => Rand.Next(500) * 2;
                    break;
                case GenderRequest.Male:
                    getIndividualMethod = () => Rand.Next(500) * 2 + 1;
                    break;
                default:
                    getIndividualMethod = () => Rand.Next(1000);
                    break;
            }
            string number = OneRandomNumber(date, getIndividualMethod, adjustMethod);
            return number;
        }

        protected static string MakeDate(string pattern, AdjustNumber adjustMethod = null)
        {
            string result;
            if (pattern.StartsWith("??????"))
            {
                result = DateInRange(FirstPossible, LastPossible);
            }
            else
            {
                result = String.Empty;
                for (int index = 0; index < 6; ++index)
                {
                    if (Wildcard == pattern[index])
                    {
                        result += (char)('0' + Rand.Next(10));
                    }
                    else
                    {
                        result += pattern[index];
                    }
                }
            }
            if (adjustMethod != null)
            {
                result = adjustMethod(result);
            }
            return result;
        }

        protected static string MakeIndividualNo(string pattern)
        {
            string result = String.Empty;
            for (int index = 6; index < 9; ++index)
            {
                if (Wildcard == pattern[index])
                {
                    result += (char)('0' + Rand.Next(10));
                }
                else
                {
                    result += pattern[index];
                }
            }
            return result;
        }

        protected static char MakeFirstCheckDigit(string number, string pattern)
        {
            return Wildcard == pattern[9] ? Modulo11(WeightsForCheckDigit1, number) : pattern[9];
        }

        protected static char MakeSecondCheckDigit(string number, string pattern)
        {
            return Wildcard == pattern[10] ? Modulo11(WeightsForCheckDigit2, number) : pattern[10];
        }

        private delegate int GetIndividualNumber();

        private static string OneRandomNumber(string date, GetIndividualNumber getIndividualMethod, AdjustNumber adjustMethod)
        {
            string number;
            char checkDigit2;
            do
            {
                char checkDigit1;
                do
                {
                    int individualNo = getIndividualMethod();
                    number = String.Format("{0}{1:000}", date, individualNo);
                    if (adjustMethod != null)
                    {
                        number = adjustMethod(number);
                    }
                    checkDigit1 = Modulo11(WeightsForCheckDigit1, number);
                } while ('-' == checkDigit1);
                number += checkDigit1;
                checkDigit2 = Modulo11(WeightsForCheckDigit2, number);
            } while ('-' == checkDigit2);
            number += checkDigit2;
            return number;
        }

        private static string DateInRange(DateTime from, DateTime to)
        {
            int days = (to - @from).Days + 1;
            DateTime date = @from.AddDays(Rand.Next(days));
            return String.Format("{0:ddMMyy}", date);
        }

        protected struct Range
        {
            public int FromYear, ToYear, FromIndividual, ToIndividual;
        }

        protected static Range[] Ranges =
        {
            new Range {FromYear = 1854, ToYear = 1899, FromIndividual = 500, ToIndividual = 749},
            new Range {FromYear = 1900, ToYear = 1999, FromIndividual = 000, ToIndividual = 499},
            new Range {FromYear = 1940, ToYear = 1999, FromIndividual = 900, ToIndividual = 999},
            new Range {FromYear = 2000, ToYear = 2039, FromIndividual = 500, ToIndividual = 999}
        };
    }
}
