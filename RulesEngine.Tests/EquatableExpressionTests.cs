using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Linq.Expressions;

namespace SpearSoft.RulesEngine.Tests
{
    /// <summary>
    /// Summary description for EquatableExpressionTests
    /// </summary>
    [TestFixture]
    public class EquatableExpressionTests
    {
        public interface IClassA
        {
            int A { get; set; }
        }
        public class ClassA : IClassA
        {
            public int A { get; set; }
        }
        public class ClassB : ClassA
        {
            public int B { get; set; }
        }
        public class AnotherClassA
        {
            public int A { get; set; }
        }

        [Test]
        public void ShouldEqual()
        {
            var exp1 = EquatableExpression.Create<ClassA, int>(a => a.A);
            var exp2 = EquatableExpression.Create<ClassA, int>(a1 => a1.A);
            Assert.AreEqual(exp1, exp2);
            Assert.IsTrue(exp1 == exp2);
            Assert.IsFalse(exp1 != exp2);
        }

        [Test]
        public void ShouldNotEqual()
        {
            var exp1 = EquatableExpression.Create<ClassA, int>(a => a.A);
            var exp2 = EquatableExpression.Create<AnotherClassA, int>(a => a.A);
            Assert.AreNotEqual(exp1, exp2);
            Assert.IsTrue(exp1 != exp2);
            Assert.IsFalse(exp1 == exp2);
        }

        [Test]
        public void ShouldApplyTo()
        {
            var exp1 = EquatableExpression.Create<ClassA, int>(a => a.A);
            var exp2 = EquatableExpression.Create<ClassB, int>(a1 => a1.A);
            var exp3 = EquatableExpression.Create<IClassA, int>(x1 => x1.A);

            Assert.IsTrue(exp1.AppliesTo(exp2));
            Assert.IsTrue(exp1.AppliesTo(exp3));
            Assert.IsTrue(exp2.AppliesTo(exp1));
            Assert.IsTrue(exp2.AppliesTo(exp3));
            Assert.IsTrue(exp3.AppliesTo(exp1));
            Assert.IsTrue(exp3.AppliesTo(exp2));
        }

        [Test]
        public void ShouldNotApplyTo()
        {
            var exp1 = EquatableExpression.Create<ClassA, int>(a => a.A);
            var exp2 = EquatableExpression.Create<AnotherClassA, int>(a => a.A);
            var exp3 = EquatableExpression.Create<ClassB, int>(a => a.B);

            Assert.IsFalse(exp1.AppliesTo(exp2));
            Assert.IsFalse(exp1.AppliesTo(exp3));
            Assert.IsFalse(exp2.AppliesTo(exp1));
            Assert.IsFalse(exp2.AppliesTo(exp3));
            Assert.IsFalse(exp3.AppliesTo(exp1));
            Assert.IsFalse(exp3.AppliesTo(exp2));
        }
    }
}
