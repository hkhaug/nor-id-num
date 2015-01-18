using System;

namespace NinEngine
{
    public enum Statuscode
    {
        Ok,
        IsNullOrEmpty,
        BadLength,
        BadCharacters,
        BadFirstDigit,
        BadDate,
        BadYearAndIndividualNumberCombination,
        BadCheckDigit,
        PatternIsNullOrEmpty,
        BadPatternLength,
        BadPattern,
        NoMatchFound
    }

    public class NinException : Exception
    {
        public Statuscode Code { get; private set; }

        public NinException(Statuscode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
