using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SpearSoft.RulesEngine;
using SpearSoft.RulesEngine.Fluent;
using System.Collections.ObjectModel;

namespace SpearSoft.RulesEngine.Tests
{
    [TestFixture]
    public class CompositionTests
    {
        class Foo
        {
            public Foo Foo1 { get; set; }
            public Foo Foo2 { get; set; }
            public int Value { get; set; }
            public Foo(int value)
            {
                Value = value;
            }
        }

        [Test]
        public void ShouldValidateComposition_Explicit()
        {
            //TODO: Redo this test!!! CallValidate() = implicit call. Build an entirely new engine for the explicit test.
            var builder = new Fluent.FluentBuilder();
            builder.For<Foo>()
                    .Setup(f => f.Value)
                        .MustEqual(1)
                    .EndSetup()
                    .If(f => f.Foo1 != null)
                        .Setup(f => f.Foo1)
                            .CallValidate()
                    .EndIf()
                    .If(f => f.Foo2 != null)
                        .Setup(f => f.Foo2)
                            .CallValidate();

            var engine = builder.Build();
            var foo = new Foo(1) { Foo1 = new Foo(1), Foo2 = new Foo(1) };
            Assert.IsTrue(engine.Validate(foo));
            foo.Foo2.Value = 3;
            Assert.IsFalse(engine.Validate(foo));
        }

        [Test]
        public void ShouldValidateComposition_ImplicitEngine()
        {
            var builder = new Fluent.FluentBuilder();
            builder.For<Foo>()
                    .Setup(f => f.Value)
                        .MustEqual(1)
                    .EndSetup()
                    .If(f => f.Foo1 != null)
                        .Setup(f => f.Foo1)
                            .CallValidate()
                    .EndIf()
                    .If(f => f.Foo2 != null)
                        .Setup(f => f.Foo2)
                            .CallValidate();

            var engine = builder.Build();
            var foo = new Foo(1) { Foo1 = new Foo(1), Foo2 = new Foo(1) };
            Assert.IsTrue(engine.Validate(foo));
            foo.Foo2.Value = 3;
            Assert.IsFalse(engine.Validate(foo));
        }

        [Test]
        public void ShouldValidateComposition_MultiNested_ImplicitEngine()
        {
            var builder = new Fluent.FluentBuilder();
            builder.For<Foo>()
                    .Setup(f => f.Value)
                        .MustEqual(1)
                    .EndSetup()
                    .If(f => f.Foo1 != null)
                        .If(f => f.Foo1 != null)
                            .If(f => f.Foo1 != null)
                                .Setup(f => f.Foo1)
                                    .CallValidate();
            builder.For<Foo>()
                    .If(f => f.Foo2 != null)
                        .If(f => f.Foo2 != null)
                            .If(f => f.Foo2 != null)
                                .Setup(f => f.Foo2)
                                    .CallValidate();

            var engine = builder.Build();
            var foo = new Foo(1) { Foo1 = new Foo(1), Foo2 = new Foo(1) };
            Assert.IsTrue(engine.Validate(foo));
            foo.Foo2.Value = 3;
            Assert.IsFalse(engine.Validate(foo));
        }

        [Test]
        public void ShouldCulpabiliseComposition()
        {
            var builder = new Fluent.FluentBuilder();
            builder.For<Foo>()
                    .Setup(f => f.Foo1.Value)
                        .MustEqual(1);

            var engine = builder.Build();
            var foo = new Foo(1) { Foo1 = new Foo(0) };
            var report = new ValidationReport(engine);
            
            Assert.IsFalse(report.Validate(foo));
            ValidationError[] errors;
            Assert.IsTrue(report.HasError(foo.Foo1, f => f.Value, out errors));

        }
    }
}
