using System;
using System.Collections.Generic;


namespace NinEngine
{
    /// <summary>
    /// http://no.wikipedia.org/wiki/F%C3%B8dselsnummer
    /// dd = day part of birth date
    /// mm = month part of birth date
    /// yy = year part of birth date
    /// iii = individual number
    /// cc = check digits
    /// </summary>
    public static class NorwegianIdentity
    {
        public const string NationalIdentityNumberFormat = "ddmmyyiiicc";
        private static readonly int[] WeightsForCheckDigitInOrganisasjonsnummer = { 3, 2, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] WeightsForCheckDigit1InFoedselsnummer = { 3, 7, 6, 1, 8, 9, 4, 5, 2 };
        private static readonly int[] WeightsForCheckDigit2InFoedselsnummer = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };


        public static bool IsValidNorwegianIdentity(string number)
        {
            return !string.IsNullOrEmpty(number) && (IsValidOrganisasjonsnummer(number) || IsValidFoedselsnummer(number) || IsValidDummyNumber(number) || IsValidSyntheticNumber(number));
        }


        public static bool IsValidOrganisasjonsnummer(string number)
        {
            if (string.IsNullOrEmpty(number) || (number.Length != 9) || (number[0] < '8'))
            {
                return false;
            }

            char checkDigit = Modulo11(WeightsForCheckDigitInOrganisasjonsnummer, number);
            return checkDigit == number[8];
        }


        public static bool IsValidFoedselsnummer(string ddmmyyiiicc)
        {
            return IsValidFoedselsnummer(ddmmyyiiicc, NumberType.Normal);
        }


        public static bool IsValidDummyNumber(string ddmmyyiiicc)
        {
            return IsValidFoedselsnummer(ddmmyyiiicc, NumberType.Dummy);
        }

        public static bool IsValidSyntheticNumber(string ddmmyyiiicc)
        {
            return IsValidFoedselsnummer(ddmmyyiiicc, NumberType.Synthetic);
        }


        private static bool IsValidFoedselsnummer(string ddmmyyiiicc, NumberType numberType)
        {
            if (string.IsNullOrEmpty(ddmmyyiiicc) || ddmmyyiiicc.Length != NationalIdentityNumberFormat.Length)
            {
                return false;
            }

            return IsValidConsideringCheckDigits(ddmmyyiiicc, numberType);
        }


        private static bool IsValidConsideringCheckDigits(string ddmmyyiiicc, NumberType numberType)
        {
            char checkDigit = Modulo11(WeightsForCheckDigit1InFoedselsnummer, ddmmyyiiicc);
            return checkDigit == ddmmyyiiicc[9] && IsValidConsideringCheckDigit2(ddmmyyiiicc, numberType);
        }


        private static bool IsValidConsideringCheckDigit2(string ddmmyyiiicc, NumberType numberType)
        {
            char checkDigit = Modulo11(WeightsForCheckDigit2InFoedselsnummer, ddmmyyiiicc);
            return checkDigit == ddmmyyiiicc[10] && HasValidDate(ddmmyyiiicc, numberType);
        }


        private static bool HasValidDate(string ddmmyyiiicc, NumberType numberType)
        {
            int dd = int.Parse(ddmmyyiiicc.Substring(0, 2));
            switch (numberType)
            {
                case NumberType.Dummy:
                    dd -= 40;
                    break;
                case NumberType.Synthetic:
                    dd -= 80;
                    break;
            }
            return HasValidDate(ddmmyyiiicc, dd);
        }


        private static bool HasValidDate(string ddmmyyiiicc, int dd)
        {
            if (dd < 1 || dd > 31)
            {
                return false;
            }
            
            int mm = int.Parse(ddmmyyiiicc.Substring(2, 2));
            return HasValidDate(ddmmyyiiicc, mm, dd);
        }


        private static bool HasValidDate(string ddmmyyiiicc, int mm, int dd)
        {
            if (mm < 1 || mm > 12)
            {
                return false;
            }

            int yy = int.Parse(ddmmyyiiicc.Substring(4, 2));
            int individual = int.Parse(ddmmyyiiicc.Substring(6, 3));
            int year = GetYear(individual, yy);
            return IsValidDate(year, mm, dd);
        }


        private static int GetYear(int individual, int yy)
        {
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

            return -1;
        }


        private static bool IsValidDate(int year, int mm, int dd)
        {
            if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
            {
                return false;
            }

            return DateTime.DaysInMonth(year, mm) >= dd;
        }


        private static char Modulo11(IList<int> weights, string number)
        {
            int sum = CalculateSum(weights, number);
            return sum >= 0 ? Modulo11(sum) : '-';
        }


        private static char Modulo11(int value)
        {
            int rest = 11 - value % 11;
            if (rest == 11)
            {
                rest = 0;
            }
            return rest < 10 ? (char)('0' + rest) : '-';
        }


        private static int CalculateSum(IList<int> weights, string number)
        {
            int sum = 0;

            for (int position = 0; position < weights.Count; ++position)
            {
                char character = number[position];

                if (char.IsDigit(character))
                {
                    int value = character - '0';
                    sum += value * weights[position];
                }
                else
                {
                    return -1;
                }
            }

            return sum;
        }
    }
}
