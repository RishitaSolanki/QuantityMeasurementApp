
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests;
    [TestClass]
    public class QuantityLengthTest
    {
        [TestMethod]
        public void TestEquality_FeetToFeet_SameValue()
        {
            Quantity q1 = new Quantity(1.0, Unit.Feet);
            Quantity q2 = new Quantity(1.0, Unit.Feet);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_InchToInch_SameValue()
        {
            Quantity q1 = new Quantity(1.0, Unit.Inch);
            Quantity q2 = new Quantity(1.0, Unit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_FeetToInch_EquivalentValue()
        {
            Quantity q1 = new Quantity(1.0, Unit.Feet);
            Quantity q2 = new Quantity(12.0, Unit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            Quantity q1 = new Quantity(1.0, Unit.Feet);
            Quantity q2 = new Quantity(2.0, Unit.Feet);
            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_NullComparison()
        {
            Quantity q1 = new Quantity(1.0, Unit.Feet);
            Assert.IsFalse(q1.Equals(null));
        }

        [TestMethod]
        public void TestEquality_SameReference()
        {
            Quantity q1 = new Quantity(1.0, Unit.Feet);
            Assert.IsTrue(q1.Equals(q1));
        }
    }
