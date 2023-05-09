using System.Data;
using TimeLib;
using TimePeriodLib;

namespace TimeUnitTests
{
    [TestClass]
    public class TimeUnitTests
    {

        #region Constructor Tests

        [DataTestMethod]
        [DataRow((byte)12, (byte)23, (byte)34)]
        [DataRow((byte)14, (byte)59, (byte)3)]
        [DataRow((byte)2, (byte)0, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0)]
        public void Constructor_3Arguments_Byte_OK(byte h, byte m, byte s)
        {
            var t = new Time(hour: h,minute: m,second: s);
            Assert.AreEqual(h, t.Hour);
            Assert.AreEqual(m, t.Minute);
            Assert.AreEqual(s, t.Second);
        }

        [DataTestMethod]
        [DataRow((int)13, (int)23, (int)34)]
        [DataRow((int)15, (int)59, (int)3)]
        [DataRow((int)3, (int)0, (int)0)]
        [DataRow((int)0, (int)0, (int)1)]
        public void Constructor_3Arguments_Int_OK(int h, int m, int s)
        {
            var t = new Time(hour: h, minute: m, second: s);
            Assert.AreEqual(h, t.Hour);
            Assert.AreEqual(m, t.Minute);
            Assert.AreEqual(s, t.Second);
        }

        [DataTestMethod]
        [DataRow((byte)12, (byte)23)]
        [DataRow((byte)14, (byte)59)]
        [DataRow((byte)2, (byte)0)]
        [DataRow((byte)0, (byte)0)]
        public void Constructor_2Arguments_Byte_OK(byte h, byte m)
        {
            var t = new Time(hour: h, minute: m);
            Assert.AreEqual(h, t.Hour);
            Assert.AreEqual(m, t.Minute);
            Assert.AreEqual(0, t.Second);
        }

        [DataTestMethod]
        [DataRow((int)13, (int)23)]
        [DataRow((int)15, (int)59)]
        [DataRow((int)3, (int)0)]
        [DataRow((int)0, (int)1)]
        public void Constructor_2Arguments_Int_OK(int h, int m)
        {
            var t = new Time(hour: h, minute: m);
            Assert.AreEqual(h, t.Hour);
            Assert.AreEqual(m, t.Minute);
            Assert.AreEqual(0, t.Second);
        }

        [DataTestMethod]
        [DataRow((byte)12)]
        [DataRow((byte)14)]
        [DataRow((byte)2)]
        [DataRow((byte)0)]
        public void Constructor_1Arguments_Byte_OK(byte h)
        {
            var t = new Time(hour: h);
            Assert.AreEqual(h, t.Hour);
            Assert.AreEqual(0, t.Minute);
            Assert.AreEqual(0, t.Second);
        }

        [DataTestMethod]
        [DataRow((int)13)]
        [DataRow((int)15)]
        [DataRow((int)3)]
        [DataRow((int)0)]
        public void Constructor_1Arguments_Int_OK(int h)
        {
            var t = new Time(hour: h);
            Assert.AreEqual(h, t.Hour);
            Assert.AreEqual(0, t.Minute);
            Assert.AreEqual(0, t.Second);
        }

        [DataTestMethod]
        [DataRow("01:02:03", (byte)1, (byte)2, (byte)3)]
        [DataRow("03:04:05", (byte)3, (byte)4, (byte)5)]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("22:33:44", (byte)22, (byte)33, (byte)44)]
        public void Constructor_StringArgument_OK(string str, byte h, byte m, byte s)
        {
            var t = new Time(str);
            Assert.AreEqual(h, t.Hour);
            Assert.AreEqual(m, t.Minute);
            Assert.AreEqual(s, t.Second);
        }

        [TestMethod]
        public void Constructor_DefaultArguments()
        {
            var t = new Time();
            Assert.AreEqual(0, t.Hour);
            Assert.AreEqual(0, t.Minute);
            Assert.AreEqual(0, t.Second);
        }

        [DataTestMethod]
        [DataRow((byte)24, (byte)30, (byte)0)]
        [DataRow((byte)12, (byte)70, (byte)0)]
        [DataRow((byte)12, (byte)30, (byte)110)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ArguemntOutOfRangeException(byte h, byte m, byte s)
        {
            var t = new Time(hour: h,minute: m,second: s);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ArguemntNullException()
        {
            var t = new Time(hour: null, minute: null, second: null);
        }

        #endregion

        #region ToString Tests

        [DataTestMethod]
        [DataRow("12:23:34", 12, 23, 34)]
        [DataRow("11:28:02", 11, 28, 2)]
        [DataRow("10:25:00", 10, 25, 0)]
        public void ToString(string str, int h, int m, int s)
        {
            var t = new Time(h, m, s);
            Assert.AreEqual(str, t.ToString());
        }

        #endregion

        #region IEquatable Tests

        [DataTestMethod]
        [DataRow(true, 1, 2, 3, 1, 2, 3)]
        [DataRow(false, 1, 2, 30, 1, 22, 3)]
        [DataRow(false, 1, 2, 3, 1, 2, 23)]
        [DataRow(true, 10, 20, 30, 10, 20, 30)]

        public void IEquatable(bool result, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.AreEqual(result, t1.Equals(t2));
        }

        #endregion

        #region IComparable Tests

        [DataTestMethod]
        [DataRow('=', 1, 2, 3, 1, 2, 3)]
        [DataRow('>', 10, 2, 3, 1, 2, 30)]
        [DataRow('<', 1, 2, 3, 1, 20, 3)]
        public void IComparable(char result, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            switch (result)
            {
                case '=':
                    Assert.AreEqual(true, t1.CompareTo(t2)==0);
                    break;
                case '<':
                    Assert.AreEqual(true, t1.CompareTo(t2) < 0);
                    break;
                case '>':
                    Assert.AreEqual(true, t1.CompareTo(t2) > 0);
                    break;
            }
        }

        #endregion

        #region Operators Tests

        [DataTestMethod]
        [DataRow(true, 10, 22, 35, 10, 22, 35)]
        [DataRow(false, 1, 22, 35, 10, 22, 35)]
        [DataRow(false, 10, 22, 35, 10, 2, 35)]
        public void Operator_Equals(bool result, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.AreEqual(result, t1==t2);
        }

        [DataTestMethod]
        [DataRow(false, 10, 22, 35, 10, 22, 35)]
        [DataRow(true, 1, 22, 35, 10, 22, 35)]
        [DataRow(true, 10, 22, 35, 10, 2, 35)]
        public void Operator_NotEquals(bool result, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.AreEqual(result, t1 != t2);
        }

        [DataTestMethod]
        [DataRow(false, 10, 22, 35, 10, 22, 35)]
        [DataRow(true, 1, 22, 35, 10, 22, 35)]
        [DataRow(false, 10, 22, 35, 10, 2, 35)]
        public void Operator_Lesser(bool result, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.AreEqual(result, t1 < t2);
        }

        [DataTestMethod]
        [DataRow(false, 10, 22, 35, 10, 22, 35)]
        [DataRow(false, 1, 22, 35, 10, 22, 35)]
        [DataRow(true, 10, 22, 35, 10, 2, 35)]
        public void Operator_Greater(bool result, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.AreEqual(result, t1 > t2);
        }

        [DataTestMethod]
        [DataRow(true, 10, 22, 35, 10, 22, 35)]
        [DataRow(true, 1, 22, 35, 10, 22, 35)]
        [DataRow(false, 10, 22, 35, 10, 2, 35)]
        public void Operator_LesserOrEqual(bool result, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.AreEqual(result, t1 <= t2);
        }

        [DataTestMethod]
        [DataRow(true, 10, 22, 35, 10, 22, 35)]
        [DataRow(false, 1, 22, 35, 10, 22, 35)]
        [DataRow(true, 10, 22, 35, 10, 2, 35)]
        public void Operator_GreaterOrEqual(bool result, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.AreEqual(result, t1 >= t2);
        }

        [DataTestMethod]
        [DataRow(10, 10, 10, 2, 2, 2, 12, 12, 12)]
        [DataRow(23, 59, 59, 0, 0, 1, 0, 0, 0)]
        [DataRow(0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [DataRow(12, 30, 30, 0, 30, 30, 13, 1, 0)]
        public void Operator_Add(int h1, int m1, int s1, int h2, int m2, int s2, int h3, int m3, int s3)
        {
            var t1 = new Time(h1, m1, s1);
            var tp = new TimePeriod(h2, m2, s2);
            var t2 = new Time(h3, m3, s3);
            Assert.AreEqual(t1+tp, t2);
        }

        [DataTestMethod]
        [DataRow(10, 10, 10, 2, 2, 2, 12, 12, 12)]
        [DataRow(23, 59, 59, 0, 0, 1, 0, 0, 0)]
        [DataRow(0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [DataRow(12, 30, 30, 0, 30, 30, 13, 1, 0)]
        public void Operator_Subtract(int h3, int m3, int s3, int h2, int m2, int s2, int h1, int m1, int s1)
        {
            var t1 = new Time(h1, m1, s1);
            var tp = new TimePeriod(h2, m2, s2);
            var t2 = new Time(h3, m3, s3);
            Assert.AreEqual(t1 - tp, t2);
        }

        #endregion
    }
}