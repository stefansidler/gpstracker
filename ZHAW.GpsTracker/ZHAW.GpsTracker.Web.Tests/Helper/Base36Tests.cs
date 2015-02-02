using System;
using NUnit.Framework;
using ZHAW.GpsTracker.Web.Helper;

namespace ZHAW.GpsTracker.Web.Tests.Helper
{
    [TestFixture]
    public class Base36Tests
    {
        [Test]
        public void Encode_0_ReturnsZero()
        {
            string expected = "0";

            string actual = Base36.Encode(0);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Encode_1_Returns1()
        {
            string expected = "1";

            string actual = Base36.Encode(1);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Encode_10_ReturnsA()
        {
            string expected = "a";

            string actual = Base36.Encode(10);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Encode_35_ReturnsZ()
        {
            string expected = "z";

            string actual = Base36.Encode(35);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Encode_10000_Returns7ps()
        {
            string expected = "7ps";

            string actual = Base36.Encode(10000);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Decode_0_Returns0()
        {
            long expected = 0;

            long actual = Base36.Decode("0");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Decode_1_Returns1()
        {
            long expected = 1;

            long actual = Base36.Decode("1");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Decode_A_Returns10()
        {
            long expected = 10;

            long actual = Base36.Decode("a");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Decode_Z_Returns35()
        {
            long expected = 35;

            long actual = Base36.Decode("z");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Decode_7ps_Returns10000()
        {
            long expected = 10000;

            long actual = Base36.Decode("7ps");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Reverse_Null_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Base36.Reverse(null));
        }

        [Test]
        public void Reverse_EmptyString_ReturnsEmptyString()
        {
            string expected = string.Empty;

            string actual = Base36.Reverse(string.Empty);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Reverse_12345_Returns54321()
        {
            string expected = "54321";

            string actual = Base36.Reverse("12345");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Reverse_54321_Returns12345()
        {
            string expected = "12345";

            string actual = Base36.Reverse("54321");

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
