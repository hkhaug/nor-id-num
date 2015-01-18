using NinEngine;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class BirthNumberTest
    {
        private const string NumberNull = null;
        private const string NumberEmpty = "";
        private const string NumberShort = "1234567890";
        private const string NumberLong = "123456789012";
        private const string NumberNonDigit1 = "A2345678901";
        private const string NumberNonDigit2 = "1b345678901";
        private const string NumberNonDigit3 = "12Ø45678901";
        private const string NumberNonDigit4 = "123å5678901";
        private const string NumberNonDigit5 = "1234!678901";
        private const string NumberNonDigit6 = "12345~78901";
        private const string NumberBadDate1 = "00019912345";
        private const string NumberBadDate2 = "32019912345";
        private const string NumberBadDate3 = "31049912345";
        private const string NumberBadDate4 = "29029912345";
        private const string NumberBadDate5 = "01009912345";
        private const string NumberBadDate6 = "01139912345";
        private const string NumberBadDate7 = "01139912345";
        private const string NumberBadYearAndIndividualNumberCombination = "01014567845";
        private const string NumberBadFirstCheckDigit1 = "01020398709";
        private const string NumberBadFirstCheckDigit2 = "01020398719";
        private const string NumberBadFirstCheckDigit3 = "01020398729";
        private const string NumberBadFirstCheckDigit4 = "01020398739";
        private const string NumberBadFirstCheckDigit5 = "01020398749";
        private const string NumberBadFirstCheckDigit6 = "01020398759";
        private const string NumberBadFirstCheckDigit7 = "01020398779";
        private const string NumberBadFirstCheckDigit8 = "01020398789";
        private const string NumberBadFirstCheckDigit9 = "01020398799";
        private const string NumberBadSecondCheckDigit1 = "01020398760";
        private const string NumberBadSecondCheckDigit2 = "01020398761";
        private const string NumberBadSecondCheckDigit3 = "01020398762";
        private const string NumberBadSecondCheckDigit4 = "01020398763";
        private const string NumberBadSecondCheckDigit5 = "01020398764";
        private const string NumberBadSecondCheckDigit6 = "01020398765";
        private const string NumberBadSecondCheckDigit7 = "01020398766";
        private const string NumberBadSecondCheckDigit8 = "01020398768";
        private const string NumberBadSecondCheckDigit9 = "01020398769";
        private const string NumberLegal = "01020398767";

        [Test]
        public void Construct_Null_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(NumberNull));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.IsNullOrEmpty, nex.Code);
        }

        [Test]
        public void Construct_Empty_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(NumberEmpty));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.IsNullOrEmpty, nex.Code);
        }

        [Test]
        public void Construct_TooShort_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(NumberShort));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadLength, nex.Code);
        }

        [Test]
        public void Construct_TooLong_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(NumberLong));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadLength, nex.Code);
        }

        [TestCase(NumberNonDigit1)]
        [TestCase(NumberNonDigit2)]
        [TestCase(NumberNonDigit3)]
        [TestCase(NumberNonDigit4)]
        [TestCase(NumberNonDigit5)]
        [TestCase(NumberNonDigit6)]
        public void Construct_NonDigits_ThrowsException(string number)
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(number));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadCharacters, nex.Code);
        }

        [TestCase(NumberBadDate1)]
        [TestCase(NumberBadDate2)]
        [TestCase(NumberBadDate3)]
        [TestCase(NumberBadDate4)]
        [TestCase(NumberBadDate5)]
        [TestCase(NumberBadDate6)]
        [TestCase(NumberBadDate7)]
        public void Construct_BadDate_ThrowsException(string number)
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(number));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadDate, nex.Code);
        }

        [TestCase(NumberBadYearAndIndividualNumberCombination)]
        public void Construct_BadYearAndIndividualNumberCombination_ThrowsException(string number)
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(number));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadYearAndIndividualNumberCombination, nex.Code);
        }

        [TestCase(NumberBadFirstCheckDigit1)]
        [TestCase(NumberBadFirstCheckDigit2)]
        [TestCase(NumberBadFirstCheckDigit3)]
        [TestCase(NumberBadFirstCheckDigit4)]
        [TestCase(NumberBadFirstCheckDigit5)]
        [TestCase(NumberBadFirstCheckDigit6)]
        [TestCase(NumberBadFirstCheckDigit7)]
        [TestCase(NumberBadFirstCheckDigit8)]
        [TestCase(NumberBadFirstCheckDigit9)]
        public void Construct_BadFirstCheckDigit_ThrowsException(string number)
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(number));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadCheckDigit, nex.Code);
        }

        [TestCase(NumberBadSecondCheckDigit1)]
        [TestCase(NumberBadSecondCheckDigit2)]
        [TestCase(NumberBadSecondCheckDigit3)]
        [TestCase(NumberBadSecondCheckDigit4)]
        [TestCase(NumberBadSecondCheckDigit5)]
        [TestCase(NumberBadSecondCheckDigit6)]
        [TestCase(NumberBadSecondCheckDigit7)]
        [TestCase(NumberBadSecondCheckDigit8)]
        [TestCase(NumberBadSecondCheckDigit9)]
        public void Construct_BadSecondCheckDigit_ThrowsException(string number)
        {
            var ex = Assert.Throws(typeof(NinException), () => new BirthNumber(number));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadCheckDigit, nex.Code);
        }

        [TestCase(NumberLegal)]
        public void Construct_Legal_PropertiesSet(string number)
        {
            BirthNumber bn = new BirthNumber(number);
            Assert.AreEqual("Fødselsnummer", bn.Name);
            Assert.AreEqual(number, bn.Number);
        }

        [TestCase(NumberNull)]
        [TestCase(NumberEmpty)]
        [TestCase(NumberShort)]
        [TestCase(NumberLong)]
        [TestCase(NumberNonDigit1)]
        [TestCase(NumberNonDigit2)]
        [TestCase(NumberNonDigit3)]
        [TestCase(NumberNonDigit4)]
        [TestCase(NumberNonDigit5)]
        [TestCase(NumberNonDigit6)]
        [TestCase(NumberBadDate1)]
        [TestCase(NumberBadDate2)]
        [TestCase(NumberBadDate3)]
        [TestCase(NumberBadDate4)]
        [TestCase(NumberBadDate5)]
        [TestCase(NumberBadDate6)]
        [TestCase(NumberBadDate7)]
        [TestCase(NumberBadYearAndIndividualNumberCombination)]
        [TestCase(NumberBadFirstCheckDigit1)]
        [TestCase(NumberBadFirstCheckDigit2)]
        [TestCase(NumberBadFirstCheckDigit3)]
        [TestCase(NumberBadFirstCheckDigit4)]
        [TestCase(NumberBadFirstCheckDigit5)]
        [TestCase(NumberBadFirstCheckDigit6)]
        [TestCase(NumberBadFirstCheckDigit7)]
        [TestCase(NumberBadFirstCheckDigit8)]
        [TestCase(NumberBadFirstCheckDigit9)]
        [TestCase(NumberBadSecondCheckDigit1)]
        [TestCase(NumberBadSecondCheckDigit2)]
        [TestCase(NumberBadSecondCheckDigit3)]
        [TestCase(NumberBadSecondCheckDigit4)]
        [TestCase(NumberBadSecondCheckDigit5)]
        [TestCase(NumberBadSecondCheckDigit6)]
        [TestCase(NumberBadSecondCheckDigit7)]
        [TestCase(NumberBadSecondCheckDigit8)]
        [TestCase(NumberBadSecondCheckDigit9)]
        public void Create_Illegal_ReturnsNull(string number)
        {
            BirthNumber bn = BirthNumber.Create(number);
            Assert.IsNull(bn);
        }

        [TestCase(NumberLegal)]
        public void Create_Legal_ReturnsObjectWithPropertiesSet(string number)
        {
            BirthNumber bn = BirthNumber.Create(number);
            Assert.IsNotNull(bn);
            Assert.AreEqual("Fødselsnummer", bn.Name);
            Assert.AreEqual(number, bn.Number);
        }

        [Test]
        public void OneRandom_ReturnsValidNumber()
        {
            Assert.DoesNotThrow(() => BirthNumber.OneRandom());
        }

        [Test]
        public void OneRandom_TwoCallsReturnsDifferentNumbers()
        {
            BirthNumber birthNo1 = BirthNumber.OneRandom();
            BirthNumber birthNo2 = BirthNumber.OneRandom();
            Assert.IsNotNull(birthNo1);
            Assert.IsNotNull(birthNo2);
            Assert.AreNotEqual(birthNo1.Number, birthNo2.Number);
        }

        [Test]
        public void OneRandom_NullPattern_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => BirthNumber.OneRandom(null));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.PatternIsNullOrEmpty, nex.Code);
        }

        [Test]
        public void OneRandom_EmptyPattern_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => BirthNumber.OneRandom(string.Empty));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.PatternIsNullOrEmpty, nex.Code);
        }

        [TestCase("??????????")]
        [TestCase("????????????")]
        public void OneRandom_BadLengthPattern_ThrowsException(string pattern)
        {
            var ex = Assert.Throws(typeof(NinException), () => BirthNumber.OneRandom(pattern));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadPatternLength, nex.Code);
        }

        [TestCase("C??????????")]
        [TestCase("?d?????????")]
        [TestCase("??Æ????????")]
        [TestCase("???ø???????")]
        [TestCase("????#??????")]
        [TestCase("?????|?????")]
        public void OneRandom_InvalidCharacterInPattern_ThrowsException(string pattern)
        {
            var ex = Assert.Throws(typeof(NinException), () => BirthNumber.OneRandom(pattern));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadPattern, nex.Code);
        }

        [Test]
        public void OneRandom_NoWildcardInPattern_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => BirthNumber.OneRandom("01234567890"));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadPattern, nex.Code);
        }

        [TestCase("???????????")]
        [TestCase("31?????????")]
        [TestCase("??12???????")]
        [TestCase("????99?????")]
        [TestCase("??????999??")]
        [TestCase("?????????0?")]
        [TestCase("??????????0")]
        [TestCase("?????????99")]
        public void OneRandom_GoodPattern_ReturnsValidNumber(string pattern)
        {
            Assert.DoesNotThrow(() => BirthNumber.OneRandom(pattern));
        }

        [Test]
        public void OneRandom_BadFromDate_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => BirthNumber.OneRandom(DateBasedIdNumber.FirstPossible.AddDays(-1), DateBasedIdNumber.LastPossible, GenderRequest.Any));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadDate, nex.Code);
        }

        [Test]
        public void OneRandom_BadToDate_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => BirthNumber.OneRandom(DateBasedIdNumber.FirstPossible, DateBasedIdNumber.LastPossible.AddDays(1), GenderRequest.Any));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadDate, nex.Code);
        }

        [Test]
        public void OneRandom_FromDateLaterThanToDate_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => BirthNumber.OneRandom(DateBasedIdNumber.LastPossible, DateBasedIdNumber.FirstPossible, GenderRequest.Any));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadDate, nex.Code);
        }

        [TestCase(GenderRequest.Female, 0)]
        [TestCase(GenderRequest.Male, 1)]
        public void OneRandom_SpecificGender_ReturnsOnlySpecifiedGender(GenderRequest gender, int expectedOddOrEven)
        {
            for (int index = 0; index < 10; ++index)
            {
                BirthNumber birthNo = BirthNumber.OneRandom(DateBasedIdNumber.FirstPossible, DateBasedIdNumber.LastPossible, gender);
                Assert.IsNotNull(birthNo);
                int oddOrEven = birthNo.Number[8] & 1;
                Assert.AreEqual(expectedOddOrEven, oddOrEven);
            }
        }
    }
}
