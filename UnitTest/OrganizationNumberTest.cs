using System.Collections.Generic;
using System.Linq;
using NinEngine;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class OrganizationNumberTest
    {
        #region Test data

        private const string NumberNull = null;
        private const string NumberEmpty = "";
        private const string NumberShort = "87654321";
        private const string NumberLong = "9876543210";
        private const string NumberNonDigit1 = "9A7654321";
        private const string NumberNonDigit2 = "98b654321";
        private const string NumberNonDigit3 = "987Ø54321";
        private const string NumberNonDigit4 = "9876å4321";
        private const string NumberNonDigit5 = "98765!321";
        private const string NumberNonDigit6 = "987654~21";
        private const string NumberBadFirstDigit1 = "023456789";
        private const string NumberBadFirstDigit2 = "123456789";
        private const string NumberBadFirstDigit3 = "223456789";
        private const string NumberBadFirstDigit4 = "323456789";
        private const string NumberBadFirstDigit5 = "423456789";
        private const string NumberBadFirstDigit6 = "523456789";
        private const string NumberBadFirstDigit7 = "623456789";
        private const string NumberBadFirstDigit8 = "723456789";
        private const string NumberBadCheckDigit1 = "987654320";
        private const string NumberBadCheckDigit2 = "987654321";
        private const string NumberBadCheckDigit3 = "987654322";
        private const string NumberBadCheckDigit4 = "987654323";
        private const string NumberBadCheckDigit5 = "987654324";
        private const string NumberBadCheckDigit6 = "987654326";
        private const string NumberBadCheckDigit7 = "987654327";
        private const string NumberBadCheckDigit8 = "987654328";
        private const string NumberBadCheckDigit9 = "987654329";
        private const string NumberLegal1 = "801234569";
        private const string NumberLegal2 = "987654325";

        #endregion Test data

        #region Fast tests

        [Category(TestCategory.Fast)]
        [Test]
        public void Construct_Null_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => new OrganizationNumber(NumberNull));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.IsNullOrEmpty, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [Test]
        public void Construct_Empty_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => new OrganizationNumber(NumberEmpty));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.IsNullOrEmpty, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [Test]
        public void Construct_TooShort_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => new OrganizationNumber(NumberShort));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadLength, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [Test]
        public void Construct_TooLong_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => new OrganizationNumber(NumberLong));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadLength, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [TestCase(NumberNonDigit1)]
        [TestCase(NumberNonDigit2)]
        [TestCase(NumberNonDigit3)]
        [TestCase(NumberNonDigit4)]
        [TestCase(NumberNonDigit5)]
        [TestCase(NumberNonDigit6)]
        public void Construct_NonDigits_ThrowsException(string number)
        {
            var ex = Assert.Throws(typeof(NinException), () => new OrganizationNumber(number));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadCharacters, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [TestCase(NumberBadFirstDigit1)]
        [TestCase(NumberBadFirstDigit2)]
        [TestCase(NumberBadFirstDigit3)]
        [TestCase(NumberBadFirstDigit4)]
        [TestCase(NumberBadFirstDigit5)]
        [TestCase(NumberBadFirstDigit6)]
        [TestCase(NumberBadFirstDigit7)]
        [TestCase(NumberBadFirstDigit8)]
        public void Construct_BadFirstDigit_ThrowsException(string number)
        {
            var ex = Assert.Throws(typeof(NinException), () => new OrganizationNumber(number));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadFirstDigit, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [TestCase(NumberBadCheckDigit1)]
        [TestCase(NumberBadCheckDigit2)]
        [TestCase(NumberBadCheckDigit3)]
        [TestCase(NumberBadCheckDigit4)]
        [TestCase(NumberBadCheckDigit5)]
        [TestCase(NumberBadCheckDigit6)]
        [TestCase(NumberBadCheckDigit7)]
        [TestCase(NumberBadCheckDigit8)]
        [TestCase(NumberBadCheckDigit9)]
        public void Construct_BadCheckDigit_ThrowsException(string number)
        {
            var ex = Assert.Throws(typeof(NinException), () => new OrganizationNumber(number));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadCheckDigit, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [TestCase(NumberLegal1)]
        [TestCase(NumberLegal2)]
        public void Construct_Legal_PropertiesSet(string number)
        {
            OrganizationNumber on = new OrganizationNumber(number);
            Assert.AreEqual("Organisasjonsnummer", on.Name);
            Assert.AreEqual(number, on.Number);
        }

        [Category(TestCategory.Fast)]
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
        [TestCase(NumberBadFirstDigit1)]
        [TestCase(NumberBadFirstDigit2)]
        [TestCase(NumberBadFirstDigit3)]
        [TestCase(NumberBadFirstDigit4)]
        [TestCase(NumberBadFirstDigit5)]
        [TestCase(NumberBadFirstDigit6)]
        [TestCase(NumberBadFirstDigit7)]
        [TestCase(NumberBadFirstDigit8)]
        [TestCase(NumberBadCheckDigit1)]
        [TestCase(NumberBadCheckDigit2)]
        [TestCase(NumberBadCheckDigit3)]
        [TestCase(NumberBadCheckDigit4)]
        [TestCase(NumberBadCheckDigit5)]
        [TestCase(NumberBadCheckDigit6)]
        [TestCase(NumberBadCheckDigit7)]
        [TestCase(NumberBadCheckDigit8)]
        [TestCase(NumberBadCheckDigit9)]
        public void Create_Illegal_ReturnsNull(string number)
        {
            OrganizationNumber on = OrganizationNumber.Create(number);
            Assert.IsNull(on);
        }

        [Category(TestCategory.Fast)]
        [TestCase(NumberLegal1)]
        [TestCase(NumberLegal2)]
        public void Create_Legal_ReturnsObjectWithPropertiesSet(string number)
        {
            OrganizationNumber on = OrganizationNumber.Create(number);
            Assert.IsNotNull(on);
            Assert.AreEqual("Organisasjonsnummer", on.Name);
            Assert.AreEqual(number, on.Number);
        }

        [Category(TestCategory.Fast)]
        [Test]
        public void OneRandom_ReturnsValidNumber()
        {
            Assert.DoesNotThrow(() => OrganizationNumber.OneRandom());
        }

        [Category(TestCategory.Fast)]
        [Test]
        public void OneRandom_TwoCallsReturnsDifferentNumbers()
        {
            OrganizationNumber orgNo1 = OrganizationNumber.OneRandom();
            OrganizationNumber orgNo2 = OrganizationNumber.OneRandom();
            Assert.IsNotNull(orgNo1);
            Assert.IsNotNull(orgNo2);
            Assert.AreNotEqual(orgNo1.Number, orgNo2.Number);
        }

        [Category(TestCategory.Fast)]
        [Test]
        public void OneRandom_NullPattern_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => OrganizationNumber.OneRandom(null));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.PatternIsNullOrEmpty, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [Test]
        public void OneRandom_EmptyPattern_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => OrganizationNumber.OneRandom(string.Empty));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.PatternIsNullOrEmpty, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [TestCase("????????")]
        [TestCase("??????????")]
        public void OneRandom_BadLengthPattern_ThrowsException(string pattern)
        {
            var ex = Assert.Throws(typeof(NinException), () => OrganizationNumber.OneRandom(pattern));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadPatternLength, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [TestCase("C????????")]
        [TestCase("?d???????")]
        [TestCase("??Æ??????")]
        [TestCase("???ø?????")]
        [TestCase("????#????")]
        [TestCase("?????|???")]
        public void OneRandom_InvalidCharacterInPattern_ThrowsException(string pattern)
        {
            var ex = Assert.Throws(typeof(NinException), () => OrganizationNumber.OneRandom(pattern));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadPattern, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [Test]
        public void OneRandom_NoWildcardInPattern_ThrowsException()
        {
            var ex = Assert.Throws(typeof(NinException), () => OrganizationNumber.OneRandom("987654321"));
            NinException nex = ex as NinException;
            Assert.IsNotNull(nex);
            Assert.AreEqual(Statuscode.BadPattern, nex.Code);
        }

        [Category(TestCategory.Fast)]
        [TestCase("?????????")]
        [TestCase("8????????")]
        [TestCase("9????????")]
        [TestCase("?1234567?")]
        [TestCase("????????0")]
        public void OneRandom_GoodPattern_ReturnsValidNumber(string pattern)
        {
            Assert.DoesNotThrow(() => OrganizationNumber.OneRandom(pattern));
        }

        [Category(TestCategory.Fast)]
        [TestCase(123)]
        [TestCase(987)]
        public void QuickManyRandom_ReturnsRequestedNumber(int count)
        {
            List<OrganizationNumber> many = OrganizationNumber.QuickManyRandom(count).ToList();
            Assert.IsNotNull(many);
            Assert.AreEqual(count, many.Count);
        }

        #endregion Fast tests

        #region Slow tests

        [Category(TestCategory.Slow)]
        [TestCase(123)]
        [TestCase(987)]
        public void ManyRandom_ReturnsRequestedNumber(int count)
        {
            List<OrganizationNumber> many = OrganizationNumber.ManyRandom(count).ToList();
            Assert.IsNotNull(many);
            Assert.AreEqual(count, many.Count);
        }

        [Category(TestCategory.Slow)]
        [Test]
        public void ManyRandom_TooManyRequested_ReturnsMaximumNumber()
        {
            List<OrganizationNumber> many = OrganizationNumber.ManyRandom(OrganizationNumber.PossibleLegalVariations + 123).ToList();
            Assert.IsNotNull(many);
            Assert.AreEqual(OrganizationNumber.PossibleLegalVariations, many.Count);
        }

        [Category(TestCategory.Slow)]
        [Test]
        public void AllPossible_ReturnsAllPossibleVariations()
        {
            List<OrganizationNumber> allPossible = OrganizationNumber.AllPossible().ToList();
            Assert.IsNotNull(allPossible);
            Assert.AreEqual(OrganizationNumber.PossibleLegalVariations, allPossible.Count);
            Assert.AreEqual("800000009", allPossible[0].Number);
            Assert.AreEqual("999999999", allPossible[OrganizationNumber.PossibleLegalVariations - 1].Number);
        }

        #endregion Slow tests
    }
}
