using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;
using System;

namespace QuantityMeasurementAppTests
{
    [TestClass]
    public class UC4EqualityTests
    {
        [TestMethod]
        public void testEquality_YardToYard_SameValue()
        {
            var q1 = new Quantity(1.0, Unit.Yards);
            var q2 = new Quantity(1.0, Unit.Yards);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_YardToYard_DifferentValue()
        {
            var q1 = new Quantity(1.0, Unit.Yards);
            var q2 = new Quantity(2.0, Unit.Yards);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            var yard = new Quantity(1.0, Unit.Yards);
            var feet = new Quantity(3.0, Unit.Feet);

            Assert.IsTrue(yard.Equals(feet));
        }

        [TestMethod]
        public void testEquality_FeetToYard_EquivalentValue()
        {
            var feet = new Quantity(3.0, Unit.Feet);
            var yard = new Quantity(1.0, Unit.Yards);

            Assert.IsTrue(feet.Equals(yard));
        }

        
        [TestMethod]
        public void testEquality_YardToInches_EquivalentValue()
        {
            var yard = new Quantity(1.0, Unit.Yards);
            var inches = new Quantity(36.0, Unit.Inches);

            Assert.IsTrue(yard.Equals(inches));
        }

        [TestMethod]
        public void testEquality_InchesToYard_EquivalentValue()
        {
            var inches = new Quantity(36.0, Unit.Inches);
            var yard = new Quantity(1.0, Unit.Yards);

            Assert.IsTrue(inches.Equals(yard));
        }

        
        [TestMethod]
        public void testEquality_YardToFeet_NonEquivalentValue()
        {
            var yard = new Quantity(1.0, Unit.Yards);
            var feet = new Quantity(2.0, Unit.Feet);

            Assert.IsFalse(yard.Equals(feet));
        }

        
        [TestMethod]
        public void testEquality_centimetersToInches_EquivalentValue()
        {
            var cm = new Quantity(1.0, Unit.Centimeters);
            var inches = new Quantity(0.393701, Unit.Inches);

            Assert.IsTrue(cm.Equals(inches));
        }

        [TestMethod]
        public void testEquality_centimetersToFeet_NonEquivalentValue()
        {
            var cm = new Quantity(1.0, Unit.Centimeters);
            var feet = new Quantity(1.0, Unit.Feet);

            Assert.IsFalse(cm.Equals(feet));
        }

        [TestMethod]
        public void testEquality_MultiUnit_TransitiveProperty()
        {
            var yard = new Quantity(1.0, Unit.Yards);
            var feet = new Quantity(3.0, Unit.Feet);
            var inches = new Quantity(36.0, Unit.Inches);

            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(yard.Equals(inches));
        }

        [TestMethod]
       public void testEquality_YardWithNullUnit()
{
    try
    {
        var invalid = new Quantity(1.0, (Unit)999);
        Assert.Fail("Expected ArgumentException was not thrown.");
    }
    catch (ArgumentException)
    {
        Assert.IsTrue(true);
    }
}

        [TestMethod]
        public void testEquality_YardSameReference()
        {
            var yard = new Quantity(1.0, Unit.Yards);

            Assert.IsTrue(yard.Equals(yard));
        }

        [TestMethod]
        public void testEquality_YardNullComparison()
        {
            var yard = new Quantity(1.0, Unit.Yards);

            Assert.IsFalse(yard.Equals(null));
        }

        [TestMethod]
        public void testEquality_CentimetersWithNullUnit()
{
    try
    {
        var invalid = new Quantity(1.0, (Unit)(-1));
        Assert.Fail("Expected ArgumentException was not thrown.");
    }
    catch (ArgumentException)
    {
        Assert.IsTrue(true);
    }
}

        [TestMethod]
        public void testEquality_CentimetersSameReference()
        {
            var cm = new Quantity(1.0, Unit.Centimeters);

            Assert.IsTrue(cm.Equals(cm));
        }

        [TestMethod]
        public void testEquality_CentimetersNullComparison()
        {
            var cm = new Quantity(1.0, Unit.Centimeters);

            Assert.IsFalse(cm.Equals(null));
        }

        [TestMethod]
        public void testEquality_AllUnits_ComplexScenario()
        {
            var yard = new Quantity(2.0, Unit.Yards);
            var feet = new Quantity(6.0, Unit.Feet);
            var inches = new Quantity(72.0, Unit.Inches);

            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(yard.Equals(inches));
        }
    }
}