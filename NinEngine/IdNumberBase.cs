using System;
using System.Collections.Generic;

namespace NinEngine
{
    public abstract class IdNumberBase
    {
        protected const int DefaultRetryCount = 1000;

        protected static Random Rand { get; private set; }

        static IdNumberBase()
        {
            Rand = new Random();
        }

        public const char Wildcard = '?';
        public string Name { get; private set; }
        public string Number { get; private set; }

        protected IdNumberBase(string name, string number)
        {
            Name = name;
            Number = number;
        }

        protected void AssertNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(Number))
            {
                string msg = string.Format("{0} mangler fullstendig.", Name);
                throw new NinException(Statuscode.IsNullOrEmpty, msg);
            }
        }

        protected void AssertLength(int length)
        {
            if (Number.Length != length)
            {
                string msg = string.Format("{0} '{1}' består ikke av akkurat {2} tegn.", Name, Number, length);
                throw new NinException(Statuscode.BadLength, msg);
            }
        }

        protected void AssertDigitsOnly()
        {
            foreach (char ch in Number)
            {
                if (!char.IsDigit(ch))
                {
                    string msg = string.Format("{0} '{1}' består ikke av bare siffre.", Name, Number);
                    throw new NinException(Statuscode.BadCharacters, msg);
                }
            }
        }

        protected void AssertCheckDigit(IList<int> weights, string number, char checkDigit)
        {
            char ch = Modulo11(weights, number);
            if(ch != checkDigit)
            {
                string msg = string.Format("{0} '{1}' har ugyldig sjekksiffer '{2}'.", Name, number, checkDigit);
                throw new NinException(Statuscode.BadCheckDigit, msg);
            }
        }

        protected static char Modulo11(IList<int> weights, string number)
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

        protected static void ValidatePattern(string pattern, int length)
        {
            if(string.IsNullOrEmpty(pattern))
            {
                throw new NinException(Statuscode.PatternIsNullOrEmpty, "Mønster mangler fullstendig.");
            }
            if (pattern.Length != length)
            {
                string msg = string.Format("Mønster '{0}' må bestå av akkurat {1} tegn.", pattern, length);
                throw new NinException(Statuscode.BadPatternLength, msg);
            }
            foreach (char ch in pattern)
            {
                if ((Wildcard == ch) || char.IsDigit(ch))
                {
                    continue;
                }
                string msg = string.Format("Mønster '{0}' kan bare inneholde siffre 0-9 og jokere ({1}).", pattern, Wildcard);
                throw new NinException(Statuscode.BadPattern, msg);
            }
            if (pattern.IndexOf(Wildcard) < 0)
            {
                string msg = string.Format("Mønster '{0}' inneholder ingen jokere ({1}).", pattern, Wildcard);
                throw new NinException(Statuscode.BadPattern, msg);
            }
        }
    }
}
