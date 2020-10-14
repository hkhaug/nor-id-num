namespace NinEngine
{
    public static class StringExtensions
    {
        public static bool IsValidNorwegianIdentity(this string number) => NorwegianIdentity.IsValidNorwegianIdentity(number);
        public static bool IsValidOrganisasjonsnummer(this string number) => NorwegianIdentity.IsValidOrganisasjonsnummer(number);
        public static bool IsValidFoedselsnummer(this string ddmmyyiiicc) => NorwegianIdentity.IsValidFoedselsnummer(ddmmyyiiicc);
        public static bool IsValidDummyNumber(this string ddmmyyiiicc) => NorwegianIdentity.IsValidDummyNumber(ddmmyyiiicc);
    }
}
